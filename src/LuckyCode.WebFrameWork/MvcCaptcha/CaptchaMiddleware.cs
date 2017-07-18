using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LuckyCode.WebFrameWork.MvcCaptcha
{
    public class CaptchaMiddleware
    {
        public string captchadata = "/____Capcha";
        public const string ValidCaptcha = "/____ValidCaptcha____";
        private readonly RequestDelegate _next;
        private ICaptchaValidManager _captcha;

        public CaptchaMiddleware(RequestDelegate next, ICaptchaValidManager captcha)
        {
            _next = next;
            _captcha = captcha;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == captchadata)
            {
                string guid = Guid.NewGuid().ToString();
                var key = context.Request.Query["key"];
                if (!string.IsNullOrEmpty(key))
                {
                    var ar=_captcha.CreateCaptcha(key);
                    context.Response.ContentType = "image/gif";
                    await context.Response.Body.WriteAsync(ar, 0, ar.Length);
                }
                
            }
            else if (context.Request.Path.Value == ValidCaptcha)
            {
                var code = context.Request.Query[context.Request.Query.Keys.FirstOrDefault()];
                var key = context.Request.Query[context.Request.Query.Keys.LastOrDefault()];
                await context.Response.WriteAsync(_captcha.IsValid(key, code).ToString().ToLower());
            }
            else
            {
                await _next(context);
            }
        }
    }
}
