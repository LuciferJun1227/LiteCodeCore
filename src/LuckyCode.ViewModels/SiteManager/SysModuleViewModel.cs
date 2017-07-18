using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LuckyCode.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckyCode.ViewModels.SiteManager
{
    public class SysModuleSortViewModel
    {
        public string Id { get; set; }
        public string ModuleName { get; set; }
    }
    public class SysModuleBase
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public long PurviewSum { get; set; }
        public int ModuleType { get; set; }
        public string ActionName { get; set; }
        public bool IsValidPurView { get; set; }

    }
    public class SysModuleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "应用程序")]
        public string ApplicationId { get; set; }
        public List<ListItemEntity> ApplicationList { get; set; } = new List<ListItemEntity>();
        [Display(Name = "父级模块")]
        public string ParentId { get; set; }
        public List<ListItemEntity> ParentList { get; set; } = new List<ListItemEntity>();
        [Display(Name = "模块层级")]
        public short ModuleLayer { get; set; }
        [Display(Name = "区域名称")]
        [Required(ErrorMessage = "不能为空")]
        public string AreaName { get; set; }
        [Display(Name = "模块名称")]
        [Required(ErrorMessage = "不能为空")]
        public string ModuleName { get; set; }
        [Display(Name = "控制器")]
        public string ControllerName { get; set; }
        [Display(Name = "操作")]
        [Remote("ValidateActionName", "SysModules", "SysManager", AdditionalFields = "Id,ControllerName,ActionName")]
        public string ActionName { get; set; }
        [Display(Name = "模块类型")]
        [UIHint("_EnumRadio")]
        public ModuleType ModuleType { get; set; }
        [Display(Name = "是否展开")]
        public bool IsExpand { get; set; }
        public List<SelectListItem> ExpadItems { get; set; }
        [Display(Name = "图标")]
        public string Icon { get; set; }
        [Display(Name = "模块描述")]
        public string ModuleDescription { get; set; }
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        [ScaffoldColumn(false)]
        public long PurviewSum { get; set; }
        public int Sort { get; set; }
        [Display(Name = "是否验证权限")]
        public bool IsValidPurView { get; set; } = true;
    }
}
