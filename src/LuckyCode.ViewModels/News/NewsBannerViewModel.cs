using System;
using System.ComponentModel.DataAnnotations;

namespace LuckyCode.ViewModels.News
{
    public class NewsBannerViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "点击跳转的地址")]
        public string Url { get; set; }
        [Display(Name = "滚动顺序")]
        public int Sort { get; set; }
        [Display(Name = "图片地址")]
        public string ImageUrl { get; set; }
        [Display(Name = "是否删除")]
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
