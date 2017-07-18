using System.ComponentModel.DataAnnotations;

namespace LuckyCode.ViewModels.SiteManager
{
    public class SysApplicationViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "请填写名称")]
        public string ApplicationName { get; set; }
        public string ApplicationUrl { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
