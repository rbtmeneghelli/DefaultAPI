using DefaultAPI.Domain;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DefaultAPI.Infra.CrossCutting
{
    public sealed class ExtensionMethods
    {
        public string RemoveMimeType(string base64)
        {
            var keyValue = "base64,";
            if (!base64.Contains(keyValue))
                return base64;

            var start = base64.LastIndexOf(keyValue);
            return base64.Substring(start).Replace(keyValue, "");
        }

        public string BuildPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public string ConvertObjectParaJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }

        public T ConvertJSonParaObject<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
            catch
            {
                throw;
            }
        }

        public string ConvertDateToString(string value)
        {
            return DateTime.TryParse(value, out var date) ? date.ToString("yyyy-MM-dd") : value;
        }

        public string GetOnlyNumbers(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var numbers = text.Where(char.IsDigit).ToArray();
            if (numbers == null || numbers.Length == 0)
                return string.Empty;

            return new string(numbers);
        }

        public string RemoveSpecialCharacters(string text)
        {
            string[] specialCharacters = { "=", ":", "%", "/" };
            string result = string.Empty;
            string partText = string.Empty;
            if (!string.IsNullOrWhiteSpace(text))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    partText = text.Substring(i, 1);
                    if (!specialCharacters.Contains(partText))
                        result += partText;
                }
                return result;
            }
            return text;
        }

        public string RemoveHtmlTags(string value)
        {
            var tagList = Regex.Matches(value, @"(?<=</?)([^ >/]+)")
                               .Select(p => p.ToString())
                               .Distinct()
                               .ToArray();

            var newText = value.Trim();

            foreach (var tag in tagList)
            {
                var start = newText.IndexOf($"<{tag}");
                var end = newText.IndexOf(">");

                if (start >= 0 && end >= 0 && end < newText.Length)
                {
                    var lenght = end - start + 1;
                    if (lenght > 0 && (start + lenght) < newText.Length)
                    {
                        var removed = newText.Substring(start, lenght);
                        newText = newText.Replace(removed, "");
                    }
                }

                newText = newText.Replace($"</{tag}>", "");
            }

            return newText;
        }

        public string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public string RemoveQuotationMarks(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.EndsWith("\'") || value.EndsWith("\""))
                return value.Replace("\'", "").Replace("\"", "").Trim();
            return value;
        }

        public bool ValidEmail(string email)
        {
            bool isEmailValid = false;

            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                isEmailValid = Regex.IsMatch(email, emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                isEmailValid = false;
            }

            return isEmailValid;
        }

        public bool ValidCpf(string vrCPF)
        {
            bool equal = true;
            string value = vrCPF.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
                return false;

            for (int i = 1; i < 11 && equal; i++)

                if (value[i] != value[0])

                    equal = false;

            if (equal || value == "12345678909")
                return false;

            int[] numbers = new int[11];

            for (int i = 0; i < 11; i++)

                numbers[i] = int.Parse(value[i].ToString());

            int sum = 0;

            for (int i = 0; i < 9; i++)

                sum += (10 - i) * numbers[i];

            int result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }

            else if (numbers[9] != 11 - result)
                return false;

            sum = 0;

            for (int i = 0; i < 10; i++)

                sum += (11 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }

            else
                if (numbers[10] != 11 - result)
                return false;

            return true;

        }

        public bool ValidCnpj(string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", "").Replace("/", "").Replace("-", "");

            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt;

            bool[] CNPJOk;

            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;

            try
            {

                for (nrDig = 0; nrDig < 14; nrDig++)
                {

                    digitos[nrDig] = int.Parse(

                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)

                        soma[0] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig, 1)));

                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {

                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (

                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == 0);

                    else

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }

            catch
            {
                return false;
            }

        }

        public string BuildPassword()
        {
            Random numbers = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVYWXZ";
            return string.Format("{0}{1}{2}{3}{4}", letters.Substring(numbers.Next(0, 25), 1), numbers.Next(0, 9), letters.Substring(numbers.Next(0, 25), 1), numbers.Next(0, 9), letters.Substring(numbers.Next(0, 25), 1));
        }

        public void ValidItensList()
        {
            List<string> arrNames = new List<string>() { "Macoratti", "Visual C#", null, ".NET", "2017" };
            string str = arrNames.TrueForAll(delegate (string val)
            {
                if (val == null)
                    return false;
                else
                    return true;
            }).ToString();
            Console.WriteLine("Todos os valores da lista são diferentes de null ? Resposta = {0}", str);
        }

        public void ValidItensArray()
        {
            var list = new List<int>() { 4, 6, 7, 8, 34, 33, 11 };
            //verifica se existe um valor menor ou igual a zero
            var isTrue = list.TrueForAll(n => n > 0);
            Console.WriteLine("Todos os valores da lista são diferentes de zero ? Resposta = {0}", isTrue);
        }

        public bool ValidTelAndCel(string pStNumeros)
        {
            string[] stDigitoMovel = new string[] { "6", "7", "8", "9" };
            string[] stDigitosFixo = new string[] { "1", "2", "3", "4", "5" };

            if (!string.IsNullOrWhiteSpace(pStNumeros) && pStNumeros.Length > 5)
            {
                if (pStNumeros.Length < 13 && stDigitoMovel.ToString().Contains(pStNumeros.Substring(5, 1)))
                    return true;

                else if (pStNumeros.Length < 14 && stDigitosFixo.ToString().Contains(pStNumeros.Substring(5, 1)))
                    return true;
            }

            return false;
        }

        public string ConvertImageToBase64(string pImage)
        {
            var imagePath = Path.GetFileName(pImage);
            var imageArray = File.ReadAllBytes(imagePath);

            return string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(imageArray));
        }

        public string GetColorHex(string[] arrColorUsed)
        {
            string color = string.Empty;
            while (true)
            {
                color = GetColorHex();
                if (arrColorUsed.Any(x => x != color))
                    break;
            }

            return color;
        }

        public string GetColorHex()
        {
            Random random = new Random();
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);
            int a = random.Next(0, 255);
            string cor = string.Format("#{0}", Color.FromArgb(a, r, g, b).Name.ToUpper().Substring(0, 6));
            return cor;
        }

        public string GetColorRgb(string[] arrColorUsed)
        {
            string color = string.Empty;
            while (true)
            {
                color = GetColorRgb().ToString();
                if (arrColorUsed.Any(x => x != color))
                    break;
            }

            return color;
        }

        public Color GetColorRgb()
        {
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return randomColor;
        }

        public DateTime LastDayPreviousMonth(string currentMonth, string currentYear)
        {
            DateTime firstMonthDay = DateTime.Parse(string.Format("{0}/{1}/{2}", "01", currentMonth, currentYear));
            DateTime lastMonthDay = firstMonthDay.AddDays(-1);
            return lastMonthDay;
        }

        public DateTime LastDayCurrentMonth(string currentMonth, string currentYear)
        {
            DateTime firstMonthDay = DateTime.Parse(string.Format("{0}/{1}/{2}", "01", currentMonth, currentYear));
            DateTime firstMonthNextDay = firstMonthDay.AddMonths(1);
            DateTime lastMonthDay = firstMonthNextDay.AddDays(-1);
            return lastMonthDay;
        }

        public DataTable ConvertDynamicListToDataTable(List<dynamic> list)
        {
            var json = JsonConvert.SerializeObject(list);
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            return dt;
        }

        public void CleanFiles(string rootFolder, string user)
        {
            string[] arrFiles = new string[] { "Arquivo1", "Arquivo2" };
            foreach (var item in arrFiles)
            {
                string fileName = string.Format("{0}_{1}.xlsx", item, user);
                FileInfo fullPath = new FileInfo(Path.Combine(rootFolder, fileName));
                if (fullPath.Exists)
                    fullPath.Delete();
            }
        }

        public void ShowAllDrivers()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Array.ForEach(drives, drive =>
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Drive {drive.Name} esta pronto.");
                    Console.WriteLine($"Espaço disponível livre: {drive.AvailableFreeSpace} " +
                     $"bytes ou { FormatBytes(drive.AvailableFreeSpace)}");
                    Console.WriteLine($"Formato : {drive.DriveFormat}");
                    Console.WriteLine($"Tipo: {drive.DriveType}");
                    Console.WriteLine($"Nome: {drive.Name}");
                    Console.WriteLine("Nome Completo da Raiz : " + $"{drive.RootDirectory.FullName}");
                    Console.WriteLine($"Espaço total Livre : {drive.TotalFreeSpace} bytes ou { FormatBytes(drive.TotalFreeSpace)}");
                    Console.WriteLine($"Espaço Total : {drive.TotalSize} bytes ou {FormatBytes(drive.TotalSize)}");
                    Console.WriteLine($"Volume Label: {drive.VolumeLabel}");
                }
                else
                {
                    Console.WriteLine($"Drive {drive.Name} não esta pronto.");
                }
                Console.WriteLine();
            });
        }

        public void ShowAllAlternativeDrivers()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Nome do Drive: {drive.Name}");
                    Console.WriteLine($"Formato: {drive.DriveFormat}");
                    Console.WriteLine($"Tipo: {drive.DriveType}");
                    Console.WriteLine($"Diretório Raiz : {drive.RootDirectory}");
                    Console.WriteLine($"Volume label: {drive.VolumeLabel}");
                    Console.WriteLine($"Espaço Livre: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Espaço disponível: {drive.AvailableFreeSpace}");
                    Console.WriteLine($"Tamanho Total: {drive.TotalSize}");
                    Console.WriteLine();
                    Console.ReadLine();
                }
            }
        }

        private string FormatBytes(long bytes)
        {
            if (bytes >= 0x1000000000000000) { return ((double)(bytes >> 50) / 1024).ToString("0.### EB"); }
            if (bytes >= 0x4000000000000) { return ((double)(bytes >> 40) / 1024).ToString("0.### PB"); }
            if (bytes >= 0x10000000000) { return ((double)(bytes >> 30) / 1024).ToString("0.### TB"); }
            if (bytes >= 0x40000000) { return ((double)(bytes >> 20) / 1024).ToString("0.### GB"); }
            if (bytes >= 0x100000) { return ((double)(bytes >> 10) / 1024).ToString("0.### MB"); }
            if (bytes >= 0x400) { return ((double)(bytes) / 1024).ToString("0.###") + " KB"; }
            return bytes.ToString("0 Bytes");
        }

        public string GetUrlDetails(string url)
        {
            Uri uri = new Uri(url);
            StringBuilder result = new StringBuilder();
            result.Append($"AbsolutePath = {uri.AbsolutePath } || ");
            result.Append($"AbsoluteUri = {uri.AbsoluteUri} || ");
            result.Append($"Authority = {uri.Authority} || ");
            result.Append($"DnsSafeHost = {uri.DnsSafeHost} || ");
            result.Append($"Fragment = {uri.Fragment} || ");
            result.Append($"Host = {uri.Host} || ");
            result.Append($"HostNameType = {uri.HostNameType} || ");
            result.Append($"IsAbsoluteUri = {uri.IsAbsoluteUri} || ");
            result.Append($"IsDefaultPort = {uri.IsDefaultPort} || ");
            result.Append($"IsFile = {uri.IsFile} || ");
            result.Append($"IsLoopback = {uri.IsLoopback} || ");
            result.Append($"IsUnc = {uri.IsUnc} || ");
            result.Append($"LocalPath = {uri.LocalPath} || ");
            result.Append($"OriginalString = {uri.OriginalString} || ");
            result.Append($"PathAndQuery = {uri.PathAndQuery} || ");
            result.Append($"Port = {uri.Port} || ");
            result.Append($"Query = {uri.Query} || ");
            result.Append($"Scheme = {uri.Scheme} || ");
            result.Append($"UserEscaped = {uri.UserEscaped} || ");
            result.Append($"UserInfo = {uri.UserInfo} || ");
            result.Append(new string('-', 10));
            return result.ToString();
        }

        private bool CheckUrlIsValid(string baseUrl, string url)
        {
            // Vale a pena destacar o método IsBaseOf que determina se a instância Uri atual é uma base da instância da Uri especificada, ou seja, ele permite determinar se uma Uri esta contida no início da segunda Uri.
            Uri baseUri = new Uri(baseUrl);
            Uri uriAddress = new Uri(url);
            if (baseUri.IsBaseOf(uriAddress))
                return true;
            return false;
        }

        public string BuildUrlWithUriBuilder(int option)
        {
            UriBuilder uriBuilder = new UriBuilder();
            string url = string.Empty;

            switch (option)
            {
                case 1:
                    uriBuilder.Scheme = "http";
                    uriBuilder.Host = "site.net";
                    uriBuilder.Path = "sistema";
                    Uri uri = uriBuilder.Uri;
                    url = uri.ToString();
                    break;
                case 2:
                    uriBuilder.Scheme = "http";
                    uriBuilder.Host = "site.net";
                    uriBuilder.Path = "sistema";
                    uriBuilder.Port = 8089;
                    uriBuilder.UserName = "site";
                    uriBuilder.Password = "numsey";
                    uriBuilder.Query = "search=br";
                    Uri uri2 = uriBuilder.Uri;
                    url = uri2.ToString();
                    break;
            }

            return url;
        }

        public bool CheckIpAddressIsValid(string ipAddress)
        {
            IPAddress IP;
            bool flag = false;
            try
            {
                flag = IPAddress.TryParse(ipAddress, out IP);
                if (flag)
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro : " + ex.Message);
            }
            return false;
        }

        public IPAddress GetIPAddress(string hostName)
        {
            // A partir de um hostname, o metodo ira retornar o endereço de IP
            Ping ping = new Ping();
            try
            {
                var replay = ping.Send(hostName);
                if (replay.Status == IPStatus.Success)
                {
                    return replay.Address;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }

        public List<string> CloneList(List<string> list)
        {
            return list.GetRange(0, list.Count);
        }

        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex)
                        { throw new Exception(ex.Message, ex.InnerException); }
                    }
                }
                return objT;
            }).ToList();
        }

        public string ConvertTimeSpanToString(TimeSpan hour)
        {
            if (hour != null)
            {
                if (hour.Hours < 10 && hour.Minutes < 10)
                    return $"0{hour.Hours}:0{hour.Minutes}";
                else if (hour.Hours >= 10 && hour.Minutes < 10)
                    return $"{hour.Hours}:0{hour.Minutes}";
                else if (hour.Hours < 10 && hour.Minutes >= 10)
                    return $"0{hour.Hours}:0{hour.Minutes}";
                else
                    return $"{hour.Hours}:{hour.Minutes}";
            }

            return string.Empty;
        }

        public string AddZeroToLeftOrRight(string text, int qty = 8, bool isLeft = true)
        {
            if (!string.IsNullOrWhiteSpace(text))
                return isLeft ? text.PadLeft(qty, '0') : text.PadRight(qty, '0');
            return text;
        }

        public List<DropDownList> ConvertEnumToList<T>() where T : Enum
        {
            List<DropDownList> list = Enum.GetValues(typeof(EnumArquivo))
              .Cast<EnumArquivo>()
              .Select(x => new DropDownList
              {
                  Id = ((long)x),
                  Description = x.ToString()
              }).ToList();

            return list;
        }

        public List<DropDownList> GetListDrivers()
        {
            int count = 0;
            List<DropDownList> list = new List<DropDownList>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (((drive.TotalFreeSpace / drive.TotalSize) * 100) < 90)
                {
                    list.Add(new DropDownList() { Id = count, Description = drive.Name });
                    count++;
                }
            }
            return list;
        }

        public string RemoveFinalQuotationMarks(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && (value.EndsWith("\'") || value.EndsWith("\"")))
                return value.Replace("\'", "").Replace("\"", "").Trim();
            return value;
        }

        public bool CheckIfStringIsEqual(string value, string word)
        {
            return value.Equals(word, StringComparison.OrdinalIgnoreCase) ? true : false;
        }

        public int getIndexPositionFromWord(string value, string valueToFind)
        {
            if (value.IndexOf(valueToFind) != -1)
                return value.IndexOf(valueToFind);

            else if (value.IndexOf(valueToFind) != -1)
                return value.IndexOf(valueToFind, 10); // O 10 é o valor inicial do indice que vai começar a procurar a string

            return -1;
        }

        public string[] getLocalDriversFromMachine()
        {
            return Environment.GetLogicalDrives();
        }

        public void BrokePassword(string password, int sizePassword, string keys)
        {
            // Exemplo: (123Abc, 8, "")

            char firstChar = 'a';
            char lastChar = 'z';
            long retries = 0;
            bool finish = false;

            if (keys == password)
            {
                finish = true;
            }

            if (keys.Length == sizePassword || finish == true)
            {
                return;
            }

            for (char c = firstChar; c <= lastChar; c++)
            {
                retries++;
                BrokePassword(password, sizePassword, keys + c);
            }
        }

        public bool CheckPassword(string password)
        {
            if (password.Length < 6 || password.Length > 15)
                return false;
            else if (password.Contains(" "))
                return false;
            else if (!password.Any(char.IsUpper))
                return false;
            else if (!password.Any(char.IsLower))
                return false;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1])
                    return false;
            }

            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            foreach (char c in specialCharactersArray)
            {
                if (password.Contains(c))
                    return true;
            }

            return false;
        }

        public string ConvertXmlToJson(string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                doc = removeXmlDeclaration(doc);
                var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);
                return json.ToString();
            }

            return null;
        }

        private XmlDocument removeXmlDeclaration(XmlDocument doc)
        {
            var declarations = doc.ChildNodes.OfType<XmlNode>().Where(x => x.NodeType == XmlNodeType.XmlDeclaration).ToList();
            declarations.ForEach(x => doc.RemoveChild(x));
            return doc;
        }

        public string CreateCustomCpf()
        {
            int sum = 0, rest = 0;
            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string result = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                sum += int.Parse(result[i].ToString()) * mult1[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            result = result + rest;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(result[i].ToString()) * mult2[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            result = result + rest;
            return result;
        }

        public List<DropDownList> ReadCsvFile(string path)
        {
            return File.ReadAllLines(path)
            .Skip(1)
            .Where(row => row.Length > 0)
            .Select(row => ParseRow(row)).ToList();
        }

        private DropDownList ParseRow(string row)
        {
            var columns = row.Split(',');
            return new DropDownList()
            {
                Id = int.Parse(columns[0]),
                Description = columns[1]
            };
        }

        public string GetMemoryStreamType(int key)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            dictionary.Add(2, "application/pdf");
            dictionary.Add(3, "application/octet-stream");
            dictionary.Add(4, "application/zip");
            return dictionary[key];
        }

        public string GetMemoryStreamExtension(int key)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "xlsx");
            dictionary.Add(2, "pdf");
            dictionary.Add(3, "docx");
            dictionary.Add(4, "zip");
            return dictionary[key];
        }

        public string GetCron(EnumJobTypeExecution typeExecution)
        {
            Dictionary<EnumJobTypeExecution, string> dictionary = new Dictionary<EnumJobTypeExecution, string>();
            dictionary.Add(EnumJobTypeExecution.Never, "0 0 31 2 *");
            dictionary.Add(EnumJobTypeExecution.Daily, "0 0 * * *");
            dictionary.Add(EnumJobTypeExecution.Weekly, "0 0 * * 1");
            dictionary.Add(EnumJobTypeExecution.Monthly, "0 0 1 * *");
            dictionary.Add(EnumJobTypeExecution.Yearly, "0 0 1 1 *");
            return dictionary[typeExecution];
        }

        public string StartDateToJob(EnumJobTypeExecution key)
        {
            DateTime currentDate = DateTime.Now;
            Dictionary<EnumJobTypeExecution, string> dictionary = new Dictionary<EnumJobTypeExecution, string>();
            dictionary.Add(EnumJobTypeExecution.Daily, currentDate.ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Weekly, GetFirstDayOfWeek(currentDate).ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Monthly, new DateTime(currentDate.Year, currentDate.Month, 1).ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Yearly, new DateTime(currentDate.Year, 1, 1).ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Never, "");
            return dictionary[key];
        }

        public string EndDateToJob(EnumJobTypeExecution key)
        {
            DateTime currentDate = DateTime.Now;
            Dictionary<EnumJobTypeExecution, string> dictionary = new Dictionary<EnumJobTypeExecution, string>();
            dictionary.Add(EnumJobTypeExecution.Daily, currentDate.ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Weekly, GetLasttDayOfWeek(currentDate).ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Monthly, new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Yearly, new DateTime(currentDate.Year, 12, 31).ToString("yyyy-MM-dd"));
            dictionary.Add(EnumJobTypeExecution.Never, "");
            return dictionary[key];
        }

        public DateTime GetFirstDayOfWeek(DateTime date)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return date.AddDays(-diff).Date;
        }

        public DateTime GetLasttDayOfWeek(DateTime date)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            DateTime start = date.AddDays(-diff).Date;
            return start.AddDays(6).Date;
        }

        public string CreateStrongPassword(int numCharacters = 10, bool includeSpecialChars = true, bool onlyUpperCase = false)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string special = "!@#$%ˆ&*(){}[];";
            var chars = includeSpecialChars ? (valid + special) : valid;
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < numCharacters--)
                res.Append(chars[rnd.Next(chars.Length)]);
            return onlyUpperCase ? res.ToString().ToUpper() : res.ToString();
        }

        public DropDownList ConvertObjectToClass(object obj)
        {
            DropDownList model = obj as DropDownList;
            return model;
        }

        public List<int> GetListPermissions(string from, string to)
        {
            Dictionary<(string, string), List<int>> dictionary = new Dictionary<(string, string), List<int>>();
            dictionary.Add(("Sistema", "Administrador"), new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            dictionary.Add(("Sistema", "Supervisor"), new List<int>() { 1, 3, 5, 7, 9 });
            dictionary.Add(("Sistema", "Colaborador"), new List<int>() { 2, 4, 6, 8 });
            dictionary.Add(("Sistema", "Usuario"), new List<int>() { 1, 2 });
            return dictionary[(from, to)];
        }

        public string RemoveSpaceFromWords(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return Regex.Replace(text.ToUpper().Trim(), @"\s+", "");

            return string.Empty;
        }

        public static DateTime FirstDayCurrentMonth()
        {
            return DateTime.Parse(string.Format("{0}/{1}/{2}", "01", DateTime.Now.Month, DateTime.Now.Year));
        }

        public string[] RepeatValues(int times, string word)
        {
            times = times > 0 ? times : 1;

            if (!string.IsNullOrEmpty(word))
                return Enumerable.Range(1, times).Select(x => word).ToArray();

            return Enumerable.Range(1, times).Select(x => "gray").ToArray();
        }

        public string RemoveAccent(string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        public string GetEnvironmentVariableValue(string path)
        {
            try
            {
                return Environment.GetEnvironmentVariable(path, EnvironmentVariableTarget.Process);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void SetEnvironmentVariableValue(string path, string value)
        {
            try
            {
                Environment.SetEnvironmentVariable(path, value, EnvironmentVariableTarget.Process);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<byte[]> SetFileToByteArray(IFormFile formfile)
        {
            MemoryStream ms = new MemoryStream(new byte[formfile.Length]);
            await formfile.CopyToAsync(ms);
            return ms.ToArray();
        }

        public void CreateJsonFile<T>(T model, string fileName = "arquivo.json")
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(model));
        }

        public void CreateXmlFile<T>(T model, string fileName = "arquivo.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(new FileStream(fileName, FileMode.OpenOrCreate), model);
        }

        public T ConvertXmlToObject<T>(string xmlFile)
        {
            StreamReader xmlStream = new StreamReader(xmlFile);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var result = (T)serializer.Deserialize(xmlStream);
            return result;
        }

        public Match EncontrarOcorrenciaDe(string fonte, string criterio, int ocorrencia)
        {
            Regex RE = new Regex(criterio, RegexOptions.Multiline);

            if (ocorrencia < 1)
                throw (new ArgumentException("Não pode ser menor que 1", nameof(ocorrencia)));

            MatchCollection correspondencias = RE.Matches(fonte);

            if (ocorrencia >= correspondencias.Count)
                return (null);

            return (correspondencias[ocorrencia]);
        }

        public List<Match> EncontrarCadaOcorrenciaDe(string fonte, string criterio, int ocorrencia)
        {
            List<Match> ocorrencias = new List<Match>();
            Regex RE = new Regex(criterio, RegexOptions.Multiline);

            if (ocorrencia < 1)
                throw (new ArgumentException("Não pode ser menor que 1", nameof(ocorrencia)));

            MatchCollection correspondencias = RE.Matches(fonte);

            for (int index = (ocorrencia - 1); index < correspondencias.Count; index += ocorrencia)
            {
                ocorrencias.Add(correspondencias[index]);
            }

            return (ocorrencias);
        }

        /// <summary>
        /// Nesse metodo nós iremos receber um base64 em string
        /// </summary>
        /// <param name="file"></param>
        /// <param name="imgFile">Esse parametro tem que ser formado por um guid + "_" + nomearquivo</param>
        /// <returns></returns>
        public async Task<(bool, string)> UploadFile(string file, string imgFile)
        {

            if (string.IsNullOrEmpty(file))
            {
                return (false, "Forneça uma imagem!");
            }

            var imageDataByteArray = Convert.FromBase64String(file);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imgFile);

            if (File.Exists(filePath))
            {
                return (false, "Já existe um arquivo com este nome!");
            }

            await File.WriteAllBytesAsync(filePath, imageDataByteArray);

            return (true, "Upload efetuado com sucesso");
        }

        /// <summary>
        /// Nesse metodo enviamos a imagem por stream sem necessidade de base 64
        /// </summary>
        /// <param name="file"></param>
        /// <param name="imgPrefix">Esse parametro tem que ser formado por um guid + "_"</param>
        /// <returns></returns>
        private async Task<(bool, string)> UploadArquivoAlternativo(IFormFile arquivo, string imgPrefix)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return (false, "Forneça uma imagem!");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imgPrefix + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                return (false, "Já existe um arquivo com este nome!");
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return (true, "Upload efetuado com sucesso");
        }

        public bool VerificaSenhaForte(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            if (senha.Length < 6 || senha.Length > 12)
                return false;

            if (!senha.Any(c => char.IsDigit(c)))
                return false;

            if (!senha.Any(c => char.IsUpper(c)))
                return false;

            if (!senha.Any(c => char.IsLower(c)))
                return false;

            if (!senha.Any(c => char.IsSymbol(c)))
                return false;

            var contadorRepetido = 0;
            var ultimoCaracter = '\0';

            foreach (var c in senha)
            {
                if (c == ultimoCaracter)
                    contadorRepetido++;
                else
                    contadorRepetido = 0;

                if (contadorRepetido == 2)
                    return false;

                ultimoCaracter = c;
            }

            return true;
        }

        public Dictionary<long, string> ConvertListToDictionary(List<DropDownList> list)
        {
            return list.ToDictionary(item => item.Id, item => item.Description);
        }

        public int MethodWithTwoResults(int value, out bool IsOk)
        {
            // Vai retornar o valor armazenado na variavel value, e podemos pegar o valor da variavel IsOk e
            // manipular como se fosse uma variavel local e não so um parametro
            IsOk = value > 0 ? true : false;
            if (value > 0 && value % 2 == 0) value = value * 2;
            else if (value > 0 && value % 3 == 0) value = value * 3;
            else value = 0;
            return value;
        }

        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public string FormatCnpj(string texto)
        {
            return Convert.ToUInt64(texto).ToString(@"00\.000\.000\/0000\-00");
        }

        public string FormatCpf(string texto)
        {
            return Convert.ToUInt64(texto).ToString(@"000\.000\.000\-00");
        }

        public string GenerateTokenNumbers()
        {
            Random random = new Random();
            int count = 0;
            string token = string.Empty;

            while (count < 8)
            {
                int number = random.Next(0, 9);
                token = string.Concat(token, number.ToString());
            }

            return token.Trim();
        }

        public static DateTime GetNextUtilDay(DateTime dateTime)
        {
            try
            {
                while (true)
                {
                    if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                        dateTime = dateTime.AddDays(2);
                    else if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                        dateTime = dateTime.AddDays(1);

                    // Caso tenha feriado nacional ou internacional, fazer uma consulta no BD pra isso...depois um IF para validar e somar 1 dia...

                    return dateTime;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static DateTime GetCurrentUtilDay()
        {
            return GetNextUtilDay(FirstDayCurrentMonth().AddDays(5));
        }

        public static double MilesToKm(double miles) => Math.Round(miles * 1.609, 3);

        public static double KmToMiles(double km) => Math.Round(km / 1.609, 3);

        public static string FormatStringBase64ToString(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }

        public static string ConvertModelObjectToXml<T>(T modelObject)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, modelObject);
                return textWriter.ToString();
            }
        }

        public static string ConvertXmlToJson<T>(string xmlData)
        {
            T modelObject;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader textReader = new StringReader(xmlData))
            {
                modelObject = (T)xmlSerializer.Deserialize(textReader);
            }
            return JsonConvert.SerializeObject(modelObject);
        }

        public static string ConvertJsonToXml<T>(string jsonData)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            T modelObject = System.Text.Json.JsonSerializer.Deserialize<T>(jsonData, options);

            return ConvertModelObjectToXml<T>(modelObject);
        }

        #region "Metodos auxiliares para diminuir o tamanho de arquivos muito extensos, para arquivos compactos"

        public static byte[] CompressStream(byte[] originalSource)
        {
            using (var outStream = new MemoryStream())
            {
                using (var gzip = new GZipStream(outStream, CompressionMode.Compress))
                {
                    gzip.Write(originalSource, 0, originalSource.Length);
                }
                return outStream.ToArray();
            }
        }

        public static byte[] DecompressStream(byte[] originalSource)
        {
            using (var sourceStream = new MemoryStream(originalSource))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        gzip.CopyTo(outStream);
                    }
                    return outStream.ToArray();
                }
            }
        }

        #endregion

        public static string RetornarTextoComAspas(string textStart, string word, string textEnd)
        {
            return string.Format("{0}{1}{2}{3}{4}", textStart, Constants.quote, word, Constants.quote, textEnd);
        }

        #region "Metodo para pegar o base64 do front (btoa ou atob) e faz o processo de conversão"

        public static string EncodingString(string toEncode)
        {
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(toEncode);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodingString(string toDecode)
        {
            byte[] bytes = Convert.FromBase64String(toDecode);
            return ASCIIEncoding.ASCII.GetString(bytes);
        }

        #endregion
    }
}