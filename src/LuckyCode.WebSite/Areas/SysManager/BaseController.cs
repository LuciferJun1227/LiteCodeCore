using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteCode.Core.Filtes;
using LuckyCode.Core.Filtes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiteCode.WebSite.Areas.SysManager
{
    [Authorize(policy:Policies.CanViewUsers)]
    [Area("SysManager")]
    public class BaseController: Controller
    {
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            
        }
    }
}
