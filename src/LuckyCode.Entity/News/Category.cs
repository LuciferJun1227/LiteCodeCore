using System.Collections.Generic;

namespace LuckyCode.Entity.News
{
    public partial class Category
    {
        public Category()
        {
            this.NewsArticles = new List<NewsArticle>();
        }

        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HyperLink { get; set; }
        public string ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public string SortCode { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CategoryType { get; set; }
        public virtual ICollection<NewsArticle> NewsArticles { get; set; }
    }
}
