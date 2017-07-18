using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LuckyCode.Core.Utility;

namespace LuckyCode.ViewModels.News
{
    public class ArticleViewModel
    {
        public System.Guid ArticleId { get; set; }
        [Display(Name = "分类名称")]
        public string CategoryId { get; set; }
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入名称")]
        public string Title { get; set; }
        public string Summarize { get; set; }
        [Display(Name = "来源")]
        public string Source { get; set; }
        [Display(Name = "作者")]
        public string Author { get; set; }
        public string Editor { get; set; }
        [Display(Name = "关键字")]
        public string KeyWord { get; set; }
        [Display(Name = "是否置顶")]
        public bool IsTop { get; set; }
        [Display(Name = "是否热点")]
        public bool IsHot { get; set; }
        [Display(Name = "")]
        public bool IsComment { get; set; }
        [Display(Name = "是否锁定")]
        public bool IsLock { get; set; }
        
        public bool IsSlide { get; set; }
        [Display(Name = "上传图片")]
        public string ImgUrl { get; set; }
        public bool IsImage { get; set; }
        public int ClickNum { get; set; }
        public System.DateTime CreateDate { get; set; }
        [Display(Name = "资讯内容")]
        public string ArticleText { get; set; }
        public List<ListItemEntity> CategoryEntities { get; set; }=new List<ListItemEntity>();
    }
}
