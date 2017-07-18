using System;
using System.Collections.Generic;
using System.Linq;
using LuckyCode.Core.Utility;
using LuckyCode.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.WebSite.Controllers {
    [Route("/api/[controller]")]
    public class NewsArticleController : Controller {

        [HttpGet]
        public IActionResult Load() {
            return Result.Success().ToJsonResult();
        }

        [HttpGet("pageIndex/{pageindex}/pageSize/{pageSize}")]
        public IActionResult Paged(int pageIndex, int pageSize) {
            var models = new List<ArticleViewModel>();
            for (int i = 0; i < 100; i++) {
                models.Add(new ArticleViewModel() {
                    ArticleId = Guid.NewGuid(),
                    Title = "测试标题" + i,
                    CreateDate = DateTime.Now.AddDays(i)
                });
            }
            var result = models.Skip(pageIndex * pageSize)
                               .Take(pageSize);
            return Result.Success()
                         .ToPagedResult(models.Count, result.Select(t => new {
                             t.ArticleId,
                             t.Title,
                             t.CreateDate
                         }));
        }
        [HttpPost]
        public IActionResult Add() {
            return Result.Success().ToJsonResult();
        }

        [HttpPut]
        public IActionResult Update() {
            return Result.Success().ToJsonResult();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete() {
            return Result.Success().ToJsonResult();
        }

    }
}
