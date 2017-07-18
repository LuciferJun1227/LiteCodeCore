using System;
using System.Threading.Tasks;
using LiteCode.WebSite.Areas.SysManager;
using LuckyCode.IService.News;
using LuckyCode.ViewModels;
using LuckyCode.ViewModels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LuckyCode.WebSite.Areas.SysManager.Controllers
{
    public class NewsArticleController : BaseController
    {
        private string indexUrl = "/SysManager/NewsArticle/Index";
        private INewsArticleService _articleService;
        private ICategoryService _categoryService;
        private ILogger _logger;
        public NewsArticleController(INewsArticleService articleService, ICategoryService categoryService, ILogger<NewsArticleController> logger)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _logger = logger;
        }
        [Resource("资讯管理")]
        public ActionResult Index()
        {
            return View();
        }
        [Resource("Ajax获取列表")]
        public ActionResult GetListViewModel(int pageIndex, int pageSize)
        {
            var page = _articleService.GetPagedList(pageIndex, pageSize);
            return Json(new TableViewModel<ArticleViewModel>() { Rows = page, Total = page.TotalCount });
        }
        [Resource("资讯添加")]
        public ActionResult Create()
        {
            var model = new ArticleViewModel();
            model.CategoryEntities = _categoryService.AppItemEntities();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                _articleService.SaveNewsArticles(model);
                return Redirect(indexUrl);
            }
            return RedirectToAction("Create");
        }
        [Resource("资讯编辑")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _articleService.GetNewsArticlesViewModel(id);
            model.CategoryEntities = _categoryService.AppItemEntities();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                _articleService.UpdateNewsArticles(model);
                return Redirect(indexUrl);
            }
            return RedirectToAction("Edit", new { id = model.ArticleId });
        }
        [Resource("资讯删除")]
        public ActionResult Delete(Guid id)
        {
            _articleService.DeleteNewsArticles(id);
            return Json(true);
        }
    }
}