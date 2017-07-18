using System.ComponentModel.DataAnnotations;

namespace LuckyCode.ViewModels.News
{
    public class LinkViewModel
    {
        public System.Guid LinkID { get; set; }
        [Display(Name = "链接名称")]
        [Required(ErrorMessage = "请输入名称")]
        public string Title { get; set; }
        [Display(Name = "链接联系人")]
        public string UserName { get; set; }
        [Display(Name = "联系电话")]
        public string UserTel { get; set; }
        [Display(Name = "联系邮箱")]
        public string UserEmail { get; set; }
        [Display(Name = "是否图片链接")]
        public bool IsImage { get; set; }
        [Display(Name = "")]
        public int DisplayOrder { get; set; }
        [Display(Name = "网络地址")]
        [Required(ErrorMessage = "请输入网址")]
        public string WebUrl { get; set; }
        [Display(Name = "图片地址")]
        public string ImageUrl { get; set; }
        [Display(Name = "是否锁定")]
        public bool IsLock { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
