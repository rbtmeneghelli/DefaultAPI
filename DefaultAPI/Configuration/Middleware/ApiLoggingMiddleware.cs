using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Configuration.Middleware
{
    /// <summary>
    /// Net Core 2.2
    /// Link de refêrencia: https://salslab.com/a/safely-logging-api-requests-and-responses-in-asp-net-core/
    /// Link de referência: https://elanderson.net/2017/02/log-requests-and-responses-in-asp-net-core/
    /// </summary>
    public sealed class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // A injeção de dependencia faz direto aqui

                var request = httpContext.Request;
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {
                    var stopWatch = Stopwatch.StartNew();
                    var requestTime = DateTime.UtcNow;
                    var requestBodyContent = await ReadRequestBody(request);
                    var originalBodyStream = httpContext.Response.Body;
                    using (var responseBody = new MemoryStream())
                    {
                        var response = httpContext.Response;
                        response.Body = responseBody;
                        await _next(httpContext);
                        stopWatch.Stop();

                        string responseBodyContent = null;
                        responseBodyContent = await ReadResponseBody(response);
                        await responseBody.CopyToAsync(originalBodyStream);

                        //Faz a chamada de um serviço do banco de dados para gravar o log
                        await SafeLog(createApiLogItem(requestTime, stopWatch.ElapsedMilliseconds, response.StatusCode, request.Method, request.Path, request.QueryString.ToString(), requestBodyContent, responseBodyContent));
                    }
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                await _next(httpContext);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            //request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return bodyAsText;
        }

        private async Task SafeLog(ApiLogItem apiLogItem)
        {
            if (apiLogItem.Path.ToLower().StartsWith("/api/login"))
            {
                apiLogItem.RequestBody = "(Request logging disabled for /api/login)";
                apiLogItem.ResponseBody = "(Response logging disabled for /api/login)";
            }

            if (apiLogItem.RequestBody.Length > 100)
            {
                apiLogItem.RequestBody = $"(Truncated to 100 chars) {apiLogItem.RequestBody.Substring(0, 100)}";
            }

            if (apiLogItem.ResponseBody.Length > 100)
            {
                apiLogItem.ResponseBody = $"(Truncated to 100 chars) {apiLogItem.ResponseBody.Substring(0, 100)}";
            }

            if (apiLogItem.QueryString.Length > 100)
            {
                apiLogItem.QueryString = $"(Truncated to 100 chars) {apiLogItem.QueryString.Substring(0, 100)}";
            }

            // Chama o serviço que vai gravar no banco por aqui!
        }

        private ApiLogItem createApiLogItem(DateTime requestTime, long responseMillis, int statusCode, string method, string path, string queryString, string requestBody, string responseBody)
        {
            return new ApiLogItem(requestTime, responseMillis, statusCode, method, path, queryString, requestBody, responseBody);
        }
    }
}
