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

    public class SysDepartmentController : BaseController
    {
        private ISysDepartmentService _departmentService;

        public SysDepartmentController(ISysDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Resource("部门管理")]
        public ActionResult Index()
        {
            return View();
        }
        [Resource("Ajax获取列表")]
        public async Task<IActionResult> GetListViewModel(int pageIndex, int pageSize)
        {
            var page =await _departmentService.GetPagedList(pageIndex, pageSize);
            return this.Json(new TableViewModel<SysDepartmentViewModel>() {Total = page.TotalCount,Rows = page});
        }
        [Resource("添加部门")]
        public async Task<IActionResult> Create()
        {
            SysDepartmentViewModel model = new SysDepartmentViewModel();
            model.ParentList = await _departmentService.GetDepartmentTree();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SysDepartmentViewModel model)
        {
            try
            {
                await _departmentService.SaveSysDepartment(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }
        [Resource("编辑部门")]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model =await _departmentService.GetSysDepartmentViewModel(id);
                model.ParentList = await _departmentService.GetDepartmentTree();
                return View(model);
            }
            catch (Exception)
            {
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SysDepartmentViewModel model)
        {
            try
            {
                await _departmentService.UpdateSysDeparment(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        [Resource("删除部门")]
        public async Task<IActionResult> Delete(string id)
        {
            await Task.FromResult(0);
            return Json("tr");
        }
    }
}