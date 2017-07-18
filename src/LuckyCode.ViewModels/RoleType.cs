using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LuckyCode.ViewModels
{
    public enum RoleType
    {
        [Display(Name = "超级管理员")]
        Admin = 0,
        [Display(Name = "一般用户")]
        User = 1,
        [Display(Name = "受限用户")]
        Guest = 2,
        [Display(Name = "公开用户")]
        Public = 3,
        [Display(Name = "未定义")]
        Undefined = 20
    }

    public enum ModuleType
    {
        [Description("菜单")]
        Menu = 0,
        [Description("按钮")]
        Button = 1,
        [Description("无刷新")]
        Ajax = 2,
        [Description("Tab内页")]
        Tab = 3,
        [Description("分部页")]
        PartialAction = 4,
        [Description("未定义")]
        Underfined = 16
    }
}
