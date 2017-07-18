using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LuckyCode.WebFrameWork.MvcCaptcha
{
    public class CaptchaRemoteAttribute: RemoteAttribute
    {
        public CaptchaRemoteAttribute(string captchaName)
        {
            AdditionalFields = captchaName;
        }

        protected override string GetUrl(ClientModelValidationContext context)
        {

            return CaptchaMiddleware.ValidCaptcha;
        }
    }
}
