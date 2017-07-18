using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LuckyCode.WebFrameWork.MvcCaptcha
{
    public interface ICaptchaValidManager
    {
        
        byte[] CreateCaptcha(string key);

        string GetCaptchaCode(string key);

        bool IsValid(string key, string real);
    }
}
