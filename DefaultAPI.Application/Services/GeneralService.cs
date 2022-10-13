using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Exceptions;
using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.CrossCutting;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using ServiceTria.Framework.DTO;
using ServiceTria.Framework.Helpers.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Services
{
    public sealed class GeneralService : BaseService, IGeneralService
    {
        private readonly IHttpContextAccessor _accessor;
        private List<RefreshTokens> _refreshTokens = new List<RefreshTokens>();

        TokenConfiguration _tokenConfiguration { get; }
        EmailSettings _emailSettings { get; }

        private readonly IHostingEnvironment _hostingEnvironment;

        public GeneralService(IHttpContextAccessor accessor, TokenConfiguration tokenConfiguration, EmailSettings emailSettings, IHostingEnvironment hostingEnvironment, INotificationMessageService notificationMessageService) : base(notificationMessageService)
        {
            _accessor = accessor;
            _tokenConfiguration = tokenConfiguration;
            _emailSettings = emailSettings;
            _hostingEnvironment = hostingEnvironment;
        }

        public long GetCurrentUserId()
        {
            string userId = _accessor.HttpContext.User.FindFirst(x => x.Type == "Id")?.Value;
            return long.TryParse(userId, out _) ? long.Parse(userId) : 0;
        }

        public string GetCurrentUserName()
        {
            return _accessor.HttpContext.User.Identity.Name ?? string.Empty;
            //return _accessor.HttpContext.User.FindFirst(x => x.Type == "Login")?.Value ?? string.Empty;
        }

        public string CreateJwtToken(Credentials credentials)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Id",credentials.Id.ToString()),
                        new Claim(ClaimTypes.Name, credentials.Login.ToString()),
                        new Claim(ClaimTypes.Role, string.Join(",",credentials.Roles)) // são as permissões do usuario, onde podemos restringir os endpoints a partir da tag >>  No Authorize(Roles = "ROLE_AUDIT") por exemplo
                }),
                Expires = DateTime.UtcNow.AddSeconds(_tokenConfiguration.Seconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<FileDownloadDTO> Export2Excel<T>(List<T> result, string fileName)
        {
            try
            {
                var file = new FileDownloadDTO
                {
                    FileName = fileName
                };

                var excel = new ServiceTria.Framework.Excel.ExcelBO(new StyleSheetSettings());

                file = await excel.ExportExcel(result, fileName);

                return file;
            }
            catch (Exception ex)
            {
                throw new ExceptionExcel();
            }
        }

        public async Task<RequestData> RequestDataToExternalAPI(string url)
        {
            RequestData requestDataDto = new RequestData();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        requestDataDto.Data = await response.Content.ReadAsStringAsync();
                        requestDataDto.IsSuccess = true;
                        return requestDataDto;
                    }
                }
            }
            catch (Exception)
            {
                requestDataDto.Data = string.Format(Constants.ExceptionRequestAPI, url);
                requestDataDto.IsSuccess = false;
            }
            return requestDataDto;
        }

        public async Task<RequestData> RequestLogin(string url, string key = "")
        {
            RequestData requestDataDto = new RequestData();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(1);
                    var stringContent = new StringContent(key, Encoding.UTF8, "application/xml");
                    HttpResponseMessage response = await client.PostAsync(url, stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        requestDataDto.Data = await response.Content.ReadAsStringAsync();
                        requestDataDto.IsSuccess = true;
                    }
                    else
                    {
                        requestDataDto.Data = string.Format(Constants.ExceptionRequestAPI, url);
                        requestDataDto.IsSuccess = false;
                    }
                    return requestDataDto;
                }
            }
            catch (Exception)
            {
                requestDataDto.Data = string.Format(Constants.ExceptionRequestAPI, url);
                requestDataDto.IsSuccess = false;
            }
            return requestDataDto;
        }

        public async Task SendEmail(EmailConfig emailConfig)
        {
            EmailSettings emailSettings = _emailSettings;
            emailSettings = new EmailSettings(emailSettings);
            emailConfig = new EmailConfig(emailSettings, emailConfig);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailConfig.emailFrom.Address);
            emailConfig.emailTo.Split(';').ToList().ForEach(p => email.To.Add(MailboxAddress.Parse(p.Trim())));
            email.Subject = GetEmailSubjectTemplate(emailConfig.enumEmailDisplay, emailConfig.enumEmailTemplate, emailConfig.emailSubject);
            email.Priority = MessagePriority.Urgent;
            email.Body = BuildMessage(emailConfig).Result;
            await ExecuteMailWithMailKit(emailConfig, email);
        }

        public async Task<bool> RunSqlProcedure(string procName, string paramName, string paramValue)
        {
            SqlConnection sqlConnObj = GetSqlConnection();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(procName, sqlConnObj);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue(paramName, paramValue);
                sqlConnObj.Open();
                await sqlCmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Notify(string.Format(Constants.ErrorInProcedure, procName));
                return false;
            }
            finally
            {
                sqlConnObj.Close();
            }

            return true;
        }

        public async Task<bool> RunSqlBackup(string directory)
        {
            string dir = string.IsNullOrWhiteSpace(directory) ? Directory.GetCurrentDirectory() : directory;
            SqlConnection sqlConnObj = GetSqlConnection();
            try
            {
                string nomeArquivo = string.Format("{0}_{1}.{2}", "DefaultAPI", DateTime.Now.ToString("ddMMyyyy"), "bak");
                if (File.Exists(Path.Combine(dir, nomeArquivo)))
                {
                    File.Delete(Path.Combine(dir, nomeArquivo));
                }
                string query = string.Format("Backup database {0} to disk='{1}\\{2}'", sqlConnObj.Database, dir, nomeArquivo);
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnObj);
                sqlConnObj.Open();
                await sqlCmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInBackup);
                return false;
            }
            finally
            {
                sqlConnObj.Close();
            }

            return true;
        }

        public async Task<MemoryStream> Export2Zip(string directory, int typeFile = 2)
        {
            ExtensionMethods extensionMethods = new ExtensionMethods();
            List<string> listaArquivos = new List<string>();
            int count = 0;
            foreach (string arquivo in Directory.GetFiles(directory, $"*.{extensionMethods.GetMemoryStreamExtension(typeFile)}"))
            {
                listaArquivos.Add(arquivo);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (string arquivo in listaArquivos)
                    {
                        ZipArchiveEntry zipArchiveEntry = archive.CreateEntry($"file{count}.pdf", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(System.IO.File.ReadAllBytes(arquivo), 0, System.IO.File.ReadAllBytes(arquivo).Length);
                        count++;
                    }
                }
                await Task.CompletedTask;
                return ms;
            }
        }

        public async Task<bool> SendPushNotification(PushNotification notification, string tokenUser)
        {
            // Reference: http://codepickup.in/csharp/fcm-push-notification-in-csharp/
            // Se nao gerar a chave web api, so ir na autentication

            var postData = JsonConvert.SerializeObject(new
            {
                to = tokenUser,
                notification,
                // data = Algum dado em especial
            });

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Constants.UrlToGetFireBase);
                request.Method = "post";
                request.KeepAlive = false;
                request.ContentType = "application/json";
                request.Headers.Add(string.Format("Authorization: key={0}", Constants.ServerApiKey));
                request.Headers.Add(string.Format("Sender: id={0}", Constants.SenderId));
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                HttpStatusCode responseCode = ((HttpWebResponse)response).StatusCode;

                if (!responseCode.Equals(HttpStatusCode.OK))
                    return false;

                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseLine = reader.ReadToEnd();
                reader.Close();

                await Task.CompletedTask;
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task ExecuteMailWithMailKit(EmailConfig emailConfig, MimeMessage email)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(emailConfig.emailSettings.PrimaryDomain, emailConfig.emailSettings.PrimaryPort, SecureSocketOptions.Auto);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.emailSettings.UsernameEmail, emailConfig.emailSettings.UserPassword);
                    await client.SendAsync(email);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private string GetEmailSubjectTemplate(EnumEmailDisplay enumEmailDisplay, EnumEmailTemplate enumEmailTemplate, string subject)
        {
            Dictionary<EnumEmailDisplay, string> dictionary = new Dictionary<EnumEmailDisplay, string>();
            dictionary.Add(EnumEmailDisplay.Padrao, $"Bem vindo ao sistema {enumEmailTemplate.GetDisplayName()}");
            dictionary.Add(EnumEmailDisplay.BoasVindas, $"Bem vindo ao sistema {enumEmailTemplate.GetDisplayName()}");
            dictionary.Add(EnumEmailDisplay.EsqueciSenha, $"{enumEmailTemplate.GetDisplayName()} - Esqueci a senha");
            dictionary.Add(EnumEmailDisplay.TrocaSenha, $"{enumEmailTemplate.GetDisplayName()} - Solicitação de troca de senha");
            dictionary.Add(EnumEmailDisplay.ConfirmacaoSenha, $"{enumEmailTemplate.GetDisplayName()} - Confirmação de senha");
            return dictionary[enumEmailDisplay];
        }

        private string GetEmailBodyTemplate(EnumEmailDisplay enumEmailDisplay, EnumEmailTemplate enumEmailTemplate, string body, string userName)
        {
            switch (enumEmailDisplay)
            {
                case EnumEmailDisplay.Padrao:
                    break;
                case EnumEmailDisplay.BoasVindas:
                    body = $"Olá, {userName}" + "<br>";
                    body += $"Seja bem vindo ao <b>{enumEmailTemplate.GetDisplayName()}</b>" + "<br> ";
                    body += $"Utilize a senha <b>{1234}</b> para acessar o sistema e usufrua de todas as ferramentas disponíveis." + "<br>";
                    break;
                case EnumEmailDisplay.EsqueciSenha:
                    body = $"<center>Olá, {userName}</center>";
                    body += $"<center>Conforme sua solicitação enviamos este email para que você possa concluir sua solicitação de esqueci a senha. Clique no botão abaixo.</center>" + "<br> ";
                    break;
                case EnumEmailDisplay.TrocaSenha:
                    body = $"<center>Olá, {userName}</center>";
                    body += $"<center>Conforme sua solicitação enviamos este email para que você possa concluir sua solicitação de troca de senha. Clique no botão abaixo.</center>" + "<br> ";
                    break;
                case EnumEmailDisplay.ConfirmacaoSenha:
                    body = $"<center>Olá, {userName}</center>";
                    body += $"<center>Quero reporta-lo que a sua confirmação de senha foi realizada com sucesso no periodo das {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}</center>" + "<br> ";
                    break;
            }
            return body;
        }

        private async Task<MimeEntity> BuildMessage(EmailConfig emailConfig)
        {
            if (emailConfig.enumEmailDisplay == EnumEmailDisplay.Relatorio)
            {
                var builder = new BodyBuilder();
                builder.HtmlBody = GetEmailBodyTemplate(emailConfig.enumEmailDisplay, emailConfig.enumEmailTemplate, emailConfig.emailBody, emailConfig.userName);
                builder.Attachments.Add(Path.Combine(_hostingEnvironment.WebRootPath, "Arquivos", "arquivo.pdf"));
                await Task.CompletedTask;
                return builder.ToMessageBody();
            }

            await Task.CompletedTask;
            return new TextPart(TextFormat.Html)
            {
                Text = GetEmailBodyTemplate(emailConfig.enumEmailDisplay, emailConfig.enumEmailTemplate, emailConfig.emailBody, emailConfig.userName),
            };
        }

        private SqlConnection GetSqlConnection()
        {
            return null;
            //return new SqlConnection(_configuration["ConnectionString:DefaultConnection"]);
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public long GetCurrentUserProfileId()
        {
            string profileId = _accessor.HttpContext.User.FindFirst(x => x.Type == "ProfileId")?.Value;
            return long.TryParse(profileId, out _) ? long.Parse(profileId) : 0;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Key);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken))
                throw new SecurityTokenException(Constants.ErrorInRefreshToken);
            else if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(Constants.ErrorInRefreshToken);

            return principal;
        }

        public void SaveRefreshToken(string username, string refreshToken)
        {
            _refreshTokens.Add(new RefreshTokens(username, refreshToken));
        }

        public string GetRefreshToken(string username)
        {
            return _refreshTokens.FirstOrDefault(x => x.Username == username).RefreshToken;
        }

        public void DeleteRefreshToken(string username, string refreshToken)
        {
            var item = _refreshTokens.FirstOrDefault(x => x.Username == username && x.RefreshToken == refreshToken);
            _refreshTokens.Remove(item);
        }

        public string ExtractObjectInformationsByReflection(object obj)
        {
            //obtem o tipo do objeto
            //esse tipo não tem relação com a instância de obj
            var tipo = obj.GetType();

            StringBuilder builder = new StringBuilder();
            //obtem o nome do tipo
            builder.AppendLine("Log do " + tipo.Name);
            builder.AppendLine("Data: " + DateTime.Now);

            //Vamos obter agora todas as propriedades do tipo
            //Usamos o método GetProperties para obter
            //o nome das propriedades do tipo
            foreach (var prop in tipo.GetProperties())
            {
                //usa a propriedade Name para obter o nome da propriedade
                //e o método GetValue() para obter o valor da instância desse tipo
                builder.AppendLine(prop.Name + ": " + prop.GetValue(obj));
            }

            return builder.ToString();
        }
    }
}
