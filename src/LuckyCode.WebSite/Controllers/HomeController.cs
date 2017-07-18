using LuckyCode.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.WebSite.Controllers {
    public class HomeController : Controller {
        public HomeController() {
             
        } 
        public IActionResult Index() {
            return View();
        }

        public IActionResult Test() {
            return View(new ArticleViewModel() { Title = "abc",Author = "1234"});
        }

    }
}
