using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace LuckyCode.Core.Middleware
{
    /// <summary>
    /// 防盗链中间件
    /// </summary>
    public class HotlinkingPreventionMiddleware
    {
        private readonly string _wwwrootFolder;
        private readonly RequestDelegate _next;

        public HotlinkingPreventionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _wwwrootFolder = env.WebRootPath;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            var isImage = context.Response.ContentType.StartsWith("image/");

            if (isImage)
            {
                var applicationUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}";
                var headersDictionary = context.Request.Headers;
                var urlReferrer = headersDictionary[HeaderNames.Referer].ToString();

                if (!string.IsNullOrEmpty(urlReferrer) && !urlReferrer.StartsWith(applicationUrl))
                {
                    var unauthorizedImagePath = Path.Combine(_wwwrootFolder, "Images/Unauthorized.png");
                    context.Response.Clear();
                    await context.Response.SendFileAsync(unauthorizedImagePath);
                }
            }
        }
    }

    public class Hotlinking : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<HotlinkingPreventionMiddleware>();
                next(app);
            };
        }
    }
}
