using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultAPI.Configuration.Middleware
{
    /// <summary>
    /// Net Core 3.0
    /// Link de referência: https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
    /// </summary>
    public sealed class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            using (var originalBodyStream = context.Response.Body)
            {
                try
                {
                    using (var responseBody = _recyclableMemoryStreamManager.GetStream())
                    {
                        context.Response.Body = responseBody;

                        await _next(context);

                        context.Response.Body.Seek(0, SeekOrigin.Begin);
                        var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                        context.Response.Body.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);

                        _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                       $"Schema:{context.Request.Scheme} " +
                       $"Host: {context.Request.Host} " +
                       $"Path: {context.Request.Path} " +
                       $"QueryString: {context.Request.QueryString} " +
                       $"Response Body: {text}");
                    }
                }
                finally
                {
                    context.Response.Body = originalBodyStream;
                }
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            string requestBody;
            stream.Position = 0;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                requestBody = streamReader.ReadToEnd();
            }
            return requestBody;
        }

        #region Metodos originais do link de referencia

        private async Task LogResponseOriginal(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string ReadStreamInChunksOptional(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        #endregion
    }
}
