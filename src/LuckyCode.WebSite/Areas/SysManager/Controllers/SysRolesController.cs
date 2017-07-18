using System.Linq;
using System.Threading.Tasks;
using LiteCode.WebSite.Areas.SysManager;
using LuckyCode.Core.Filtes;
using LuckyCode.IService;
using LuckyCode.ViewModels;
using LuckyCode.ViewModels.SiteManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.WebSite.Areas.SysManager.Controllers
{

    public class SysRolesController : BaseController
    {
        private ISysRolesService _rolesService;
        private ISysModulesService _modulesService;
        public SysRolesController(ISysRolesService rolesService, ISysModulesService sysModulesService)
        {
            _rolesService = rolesService;
            _modulesService = sysModulesService;
        }
        [Resource("角色管理")]
        public async Task<IActionResult> Index()
        {
            await Task.FromResult(0);
            
            return View();
        }
        [Resource("Ajax获取列表")]
        public async Task<IActionResult> GetListViewModel(int pageIndex, int pageSize)
        {
            var page =await _rolesService.GetPagedList(pageIndex, pageSize);
            return Json(new TableViewModel<SysRoleViewModel>() {Rows = page,Total = page.TotalCount});
        }
        [Resource("添加角色")]
        public async Task<IActionResult> Create()
        {
            await Task.FromResult(0);
            var model = new SysRoleViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SysRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _rolesService.SaveSysRole(model);
                return Redirect("/SysManager/SysRoles/Index");
            }
            return View(model);
        }
        [Resource("编辑角色")]
        public async Task<IActionResult> Edit(string id)
        {
            var model =await _rolesService.GetSysRoleViewModel(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SysRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _rolesService.UpdateSysModule(model);
                return Redirect("/SysManager/SysRoles/Index");
            }
            return View();
        }
        [Resource("删除角色")]
        public async Task<IActionResult> Delete(string id)
        {
            await _rolesService.DeleteSysRole(id);
            return Json(new ReturnResult() { ErrorCode = "0", Message = "成功" });
        }
        [Resource("角色权限")]
        public async Task<IActionResult> EditPurview(string id)
        {
            var model=new SysRoleModuleViewModel();
            var roleModel =await _rolesService.GetSysRoleViewModel(id);
            model.RoleId = roleModel.Id;
            model.RoleName = roleModel.RoleName;
            model.ModuleBases =await _modulesService.GetModuleBases();
            ViewBag.ModuleControllers =await _modulesService.GetControllerNameList();
            ViewBag.PurViewSum =await _rolesService.GetModulePurviewViewModel(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPurview(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var model=new SysRoleModuleViewModel();
                model.RoleId= form["RoleId"];
                var arr = form.Keys.ToList();
                for (int i = 0; i < form.Count; i++)
                {
                    if (arr[i] != "__RequestVerificationToken" && arr[i] != "RoleId")
                    {
                        var module = new SysModuleBase();
                        string s = form[arr[i]];
                        string[] str_num = s.Split(',');
                        long a = str_num.Select(t=> long.Parse(t)).Sum();
                        module.ControllerName = arr[i];
                        module.PurviewSum = a;
                        model.ModuleBases.Add(module);
                    }
                }
                await _rolesService.SaveRoleModule(model);
            }
            return RedirectToAction("EditPurview", new {Id= form["RoleId"] });
        }
    }
}