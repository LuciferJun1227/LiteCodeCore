using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LuckyCode.Core.Utility;

namespace LuckyCode.ViewModels.News
{
    public class CategoryViewModel
    {
        public string CategoryId { get; set; }
        [Display(Name = "分类名称")]
        [Required(ErrorMessage = "请输入名称")]
        public string Title { get; set; }
        [Display(Name = "分类描述")]
        public string Description { get; set; }
        [Display(Name = "链接地址")]
        public string HyperLink { get; set; }
        [Display(Name = "父级分类")]
        public string ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public string SortCode { get; set; }
        public System.DateTime CreateDate { get; set; }
        public List<ListItemEntity> CategoryEntities { get; set; }=new List<ListItemEntity>();
    }
}
