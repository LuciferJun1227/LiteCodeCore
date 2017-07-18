using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;

namespace LuckyCode.Core.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;
        public LoggerMiddleware(RequestDelegate next,ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                return;
            }
            using (MemoryStream requestBodyStream = new MemoryStream())
            {
                using (MemoryStream responseBodyStream = new MemoryStream())
                {
                    Stream originalRequestBody = context.Request.Body;
                    context.Request.EnableRewind();
                    Stream originalResponseBody = context.Response.Body;

                    try
                    {
                        await context.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

                        requestBodyStream.Seek(0, SeekOrigin.Begin);
                        context.Request.Body = requestBodyStream;

                        string responseBody = "";


                        context.Response.Body = responseBodyStream;

                        Stopwatch watch = Stopwatch.StartNew();
                        await _next(context);
                        watch.Stop();

                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        responseBody = new StreamReader(responseBodyStream).ReadToEnd();
                        _logger.LogInformation(
                            $"{context.Request.Host.Host}" +
                            $"\r\n{context.Request.Path}" +
                            $"\r\n{context.Request.QueryString.ToString()}" +
                            $"\r\n{context.Connection.RemoteIpAddress.MapToIPv4().ToString()}" +
                            //$"\r\n{string.Join(",", context.Request.Headers.Select(he => he.Key + ":[" + he.Value + "]").ToList())}" +
                            $"\r\n{requestBodyText}\r" +
                            $"\n{responseBody}" +
                            $"\r\n{DateTime.Now}" +
                            $"\r\n{watch.ElapsedMilliseconds}");
                    responseBodyStream.Seek(0, SeekOrigin.Begin);

                        await responseBodyStream.CopyToAsync(originalResponseBody);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        byte[] data = System.Text.Encoding.UTF8.GetBytes("Unhandled Error occured. Please, try again in a while.");
                        originalResponseBody.Write(data, 0, data.Length);
                    }
                    finally
                    {
                        context.Request.Body = originalRequestBody;
                        context.Response.Body = originalResponseBody;
                    }
                }
            }
        }
    }
}
