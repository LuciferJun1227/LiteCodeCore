using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LuckyCode.IService;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.WebSite.Areas.SysManager.ViewComponent
{
    public class LeftNavViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private ISysModulesService _modulesService;
        public LeftNavViewComponent(ISysModulesService modulesService)
        {
            _modulesService = modulesService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string roleId = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Claim claim = ((ClaimsIdentity)User.Identity).Claims.SingleOrDefault(a => a.Type == ClaimTypes.Role);
                if (claim != null)
                {
                    roleId = claim.Value;
                }
            }
            var model = await _modulesService.GetSysModuleViewModels(roleId);
            return View("LeftNav",model);
        }
    }
}
