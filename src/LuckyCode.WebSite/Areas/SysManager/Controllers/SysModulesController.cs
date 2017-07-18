using System;
using System.Threading.Tasks;
using LiteCode.WebSite.Areas.SysManager;
using LuckyCode.Core.Filtes;
using LuckyCode.IService;
using LuckyCode.ViewModels;
using LuckyCode.ViewModels.SiteManager;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.WebSite.Areas.SysManager.Controllers
{

    public class SysModulesController : BaseController
    {
        private ISysModulesService _modulesService;
        private ISysApplicationService _applicationService;

        public SysModulesController(ISysModulesService modulesService, ISysApplicationService applicationService)
        {
            _modulesService = modulesService;
            _applicationService = applicationService;
        }

        [Resource("系统菜单")]
        public ActionResult Index()
        {
            return View();
        }
        [Resource("Ajax获取列表")]
        public async Task<IActionResult> GetListViewModel(int pageIndex, int pageSize)
        {
            var page =await _modulesService.GetPagedList(pageIndex, pageSize);
            return Json(new TableViewModel<SysModuleViewModel>() {Rows = page, Total = page.TotalCount});
        }
        [Resource("添加模块")]
        public async Task<IActionResult> Create()
        {
            var model = new SysModuleViewModel();
            model.ParentList =await _modulesService.ModuleItemEntities();
            model.ApplicationList =await _applicationService.AppItemEntities();
            model.AreaName = "SysManager";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SysModuleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modulesService.SaveSysModule(model);
                return Redirect("/SysManager/SysModules/Index");
            }
            return View();
        }
        [Resource("编辑模块")]
        public async Task<IActionResult> Edit(string id)
        {
            var model =await _modulesService.GetModuleViewModel(id);
            model.ParentList =await _modulesService.ModuleItemEntities();
            model.ApplicationList =await _applicationService.AppItemEntities();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SysModuleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modulesService.UpdateSysModule(model);
                return Redirect("/SysManager/SysModules/Index");
            }
            return RedirectToAction("Edit", model.Id);
        }
        [Resource("删除模块")]
        public async Task<IActionResult> Delete(string id)
        {
            await Task.FromResult(0);
            return Json(new ReturnResult() {ErrorCode = "0", Message = "成功"});
        }
        [Resource("模块排序")]
        public async Task<IActionResult> ModuleSort(string id)
        {
            var model =await _modulesService.GetModuleSortViewModels(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModuleSort(SysModuleSortViewModel[] Items)
        {
            try
            {
                await _modulesService.UpdateModuleSort(Items);
                return Json(true);
            }
            catch (Exception ex)
            {
               
            }
            return Redirect("/SysManager/SysModules/Index");
        }
        [Resource("验证操作名")]
        public async Task<IActionResult> ValidateActionName(string id, string controllerName, string actionName)
        {
            bool m =await _modulesService.ValidateActionName(id, controllerName, actionName);
            if(!m)
                return Json(true);
            return Json("模块操作名称已经存在！");
        }
    }
}