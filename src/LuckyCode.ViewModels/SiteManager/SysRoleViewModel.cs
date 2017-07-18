using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LuckyCode.ViewModels.SiteManager
{
    public class SysRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
        [Display(Name = "角色类型")]
        public RoleType RoleType { get; set; }
        [Display(Name = "角色描述")]
        public string RoleDescription { get; set; }
        [Display(Name = "")]
        public int? Sort { get; set; }
        [Display(Name = "能否删除")]
        public bool IsDelete { get; set; }
        public DateTime? CreateTime { get; set; }
    }

    public class SysRoleModuleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<SysModuleBase> ModuleBases { get; set; } = new List<SysModuleBase>();
    }

    public class SysRoleModulePurviewViewModel
    {
        public string RoleId { get; set; }
        public string ControllerName { get; set; }
        public long PurviewSum { get; set; }
    }
}
