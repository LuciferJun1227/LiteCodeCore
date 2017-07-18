using System;
using System.Threading.Tasks;
using LiteCode.WebSite.Areas.SysManager;
using LuckyCode.IService.News;
using LuckyCode.ViewModels;
using LuckyCode.ViewModels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LuckyCode.Core.Filtes;

namespace LuckyCode.WebSite.Areas.SysManager.Controllers
{
    public class NewsBannerController : BaseController
    {
        private string indexUrl = "/SysManager/NewsBanner/Index";
        private INewsBannerService _bannerService;
        private ILogger _logger;

        public NewsBannerController(ILogger<NewsBannerController> logger, INewsBannerService bannerService)
        {
            _bannerService = bannerService;
            _logger = logger;
        }

        [Resource("Banner管理")]
        public ActionResult Index()
        {
            return View();
        }
        [Resource("Ajax获取列表")]
        public ActionResult GetListViewModel(int pageIndex, int pageSize)
        {
            var page = _bannerService.GetPagedList(pageIndex, pageSize);
            return Json(new TableViewModel<NewsBannerViewModel>() { Rows = page, Total = page.TotalCount });
        }
        [Resource("Banner添加")]
        public ActionResult Create()
        {
            var model = new NewsBannerViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NewsBannerViewModel model)
        {

            if (ModelState.IsValid)
            {
                var self = this;
                try
                {
                    _bannerService.SaveNewsBanner(model);
                    return Redirect(indexUrl);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }
        [Resource("Banner编辑")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _bannerService.GetNewsBannerViewModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(NewsBannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bannerService.UpdateNewsBanner(model);
                    return Redirect(indexUrl);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View();
        }
        [Resource("Banner删除")]
        public ActionResult Delete(string id)
        {
            return Json(true);
        }
    }
}