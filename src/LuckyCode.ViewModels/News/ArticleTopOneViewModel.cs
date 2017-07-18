using System;

namespace LuckyCode.ViewModels.News
{
    /// <summary>
    /// 印度医疗类
    /// </summary>
    public class ArticleTopOneViewModel
    {
        public Guid ArticleID { get; set; }
        public string Title { get; set; }
        public long CategoryID { get; set; }
        public string ArticleText { get; set; }
    }

    public class ArticleFromCategoryId
    {
        public Guid ArticleID { get; set; }
        public string Title { get; set; }
        public long CategoryID { get; set; }
        public string ArticleText { get; set; }
        public string ImgUrl { get; set; }
    }
}
