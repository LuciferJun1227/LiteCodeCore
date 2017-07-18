using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LuckyCode.WebFrameWork.MvcCaptcha
{
    public class CaptchaValidManager : ICaptchaValidManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CaptchaValidManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        private string GenerateOptionsKey(string key)
        {
            return $"{key}_captcha_options";
        }

        private string GenerateValueKey(string key)
        {
            return $"{key}_captcha_value";

        }

        private ISession Session => _contextAccessor.HttpContext.Session;
       

        
        public byte[] CreateCaptcha(string key)
        {
             byte[] ar;
            var options = new MvcCaptchaOptions();
            options.TextLength = 4;
            var ci = new MvcCaptchaImage(options);
            ci.ResetText();
            using (var b = ci.RenderImage())
            {
                using (var mem = new MemoryStream())
                {
                    b.Save(mem, ImageFormat.Gif);
                     ar = mem.ToArray();
                }
                var valueKey = GenerateValueKey(key);
                string code = ci.Text;
                Session.SetString(valueKey, code);
                string str = Session.GetString(valueKey);
            }
            return ar;
        }

        public string GetCaptchaCode(string key)
        {
            string str= GenerateValueKey(key);
            return Session.GetString(str);
        }

       
        public bool IsValid(string key, string real)
        {
            var code = GetCaptchaCode(key);
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(real))
            {
                return false;
            }
            return code.Equals(real, StringComparison.OrdinalIgnoreCase);
        }
    }
}
