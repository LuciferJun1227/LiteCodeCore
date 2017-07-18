using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LuckyCode.Core.Utility;
using LuckyCode.WebFrameWork.MvcCaptcha;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.ViewModels
{
    
    public class SysUserBaseViewModel:BaseViewModel
    {
        
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入登陆名")]
        [StringLength(20, ErrorMessage = "用户名不能超长")]
        [Remote("ValidateUserName", "SysUsers", "SiteManager", AdditionalFields = "Id,UserName", ErrorMessage = "用户已经被占用，请更换！")]
        public string Username { get; set; }
        [Display(Name = "真实姓名")]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "手机")]
        public string Tel { get; set; }
        [Display(Name = "是否锁定")]
        public bool IsLock { get; set; }
        public List<ListItemEntity> RoleItemEntities { get; set; } = new List<ListItemEntity>();
    }
    public class SysUsersCreateViewModel : SysUserBaseViewModel
    {
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码必填")]
        public string Password { get; set; }
        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请输入确认密码")]
        [Compare("Password",ErrorMessage = "密码必须相同")]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }
        
        public DateTime? CreateTime { get; set; }
        
    }

    public class SysUserListViewModel : SysUserBaseViewModel
    {
        public DateTime? Createtime { get; set; }
    }
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Display(Name = "当前密码")]
        [Required(ErrorMessage = "请输入老密码")]
        public string OldPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "新密码必填")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "密码不匹配")]
        public string ConfirmPassword { get; set; }
    }
    public class SysLoginViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        public bool Rememberme { get; set; }
        public string ReturnUrl { get; set; }
        [Required(ErrorMessage = "验证码必填")]
        [CaptchaRemote("Code",AdditionalFields = "Code,key",ErrorMessage = "验证码错误")]
        public string Code { get; set; }
    }
}
