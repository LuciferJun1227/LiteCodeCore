using System;
using System.Linq;
using System.Threading.Tasks;
using LiteCode.WebSite.Areas.SysManager;
using LuckyCode.Core.Filtes;
using LuckyCode.Core.Utility.Sequence;
using LuckyCode.Entity.IdentityEntity;
using LuckyCode.IService;
using LuckyCode.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCode.WebSite.Areas.SysManager.Controllers
{

    public class SysUsersController : BaseController
    {
        private ISysUserService _usersService;
        private ISysRolesService _rolesService;
        private UserManager<SysUsers> _userManager;
        public SysUsersController(ISysUserService usersService,ISysRolesService rolesService, UserManager<SysUsers> userManager)
        {
            _usersService = usersService;
            _rolesService = rolesService;
            _userManager = userManager;
        }

        [Resource("用户管理")]
        public  IActionResult Index()
        {
            return View();
        }
        [Resource("Ajax获取列表")]
        public async Task<IActionResult> GetListViewModel(int pageIndex,int pageSize)
        {
            var page =await _usersService.GetPagedList(pageIndex,pageSize);
            return Json(new {total = page.TotalCount, rows = page});
        }
        [Resource("添加用户")]
        public async Task<IActionResult> Create()
        {
            var model = new SysUsersCreateViewModel();
            model.RoleItemEntities =await _rolesService.GetRoleItemEntities();
            return View(model);
        }
        [Resource("修改密码")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            await Task.FromResult(0);
            ChangePasswordViewModel model=new ChangePasswordViewModel();
            model.Id = id;
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var entity = await _userManager.FindByIdAsync(model.Id);
            var result=await _userManager.ChangePasswordAsync(entity, model.OldPassword, model.NewPassword);
            if(result.Errors.Any())
            {
                
                ModelState.AddModelError("CustomError", "修改密码失败");
            }
            if (ModelState.IsValid) return Json(new { success = true });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SysUsersCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysUsers();
                entity.Id = SequenceQueue.NewIdString("");
                entity.UserName = model.Username;
                entity.FullName = model.FullName;
                entity.IsLock = false;
                entity.Email = model.Email;
                entity.EmailConfirmed = false;
                entity.TwoFactorEnabled = false;
                entity.PhoneNumber = model.Tel;
                entity.PhoneNumberConfirmed = false;
                entity.LockoutEnabled = true;
                entity.CreateTime=DateTime.Now;
                try
                {
                    entity.Roles.Add(new SysUserRole() {UserId = entity.Id});
                    var result =await _userManager.CreateAsync(entity, model.Password);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    if (!ModelState.IsValid) return View();
                }
                catch (Exception ex)
                {
                    
                    throw;
                }


                return Redirect("/SiteManager/SysUsers/Index");
            }
            
            return View();
        }
        [Resource("编辑用户")]
        public async Task<IActionResult> Edit(string id)
        {
            var entity =await _userManager.FindByIdAsync(id);
            
            var model =await _usersService.GetSysUsersViewModel(id);
           // model.RoleItemEntities = _rolesService.GetRoleItemEntities();
           // var firstOrDefault = HttpContext.GetOwinContext().GetUserManager<LuckyCoreContext>().DbSet<SysUserRole>().FirstOrDefault(a=>a.UserId==id);
           // if (firstOrDefault != null) model.RoleId= firstOrDefault.RoleId;
            return View(model);
        }
        /*
        [HttpPost]
        public async Task<IActionResult> Edit(SysUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(model.Id);
                if (entity != null)
                {
                    entity.Id = model.Id;
                    entity.UserName = model.UserName;
                    entity.FullName = model.FullName;
                    entity.IsLock = model.IsLock;
                    entity.Email = model.Email;
                    entity.EmailConfirmed = false;
                    entity.TwoFactorEnabled = false;
                    entity.PhoneNumber = model.Tel;
                    entity.PhoneNumberConfirmed = false;
                    entity.LockoutEnabled = true;
                    entity.LastLoginDate = DateTime.Now;
                    entity.CreateTime = model.CreateTime;
                    entity.LastModify = DateTime.Now;
                    entity.LockoutEndDateUtc = DateTime.Now.AddYears(20);
                    try
                    {
                       var set= HttpContext.GetOwinContext().GetUserManager<LuckyCoreContext>().DbSet<SysUserRole>();
                        var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        var oldRoleId = set.FirstOrDefault(a=>a.UserId==entity.Id)?.RoleId;
                        if (oldRoleId != model.RoleId)
                        {
                            if(oldRoleId!=null)
                                set.Remove(set.FirstOrDefault(a => a.UserId == entity.Id));
                            set.Add(new SysUserRole() { RoleId = model.RoleId,UserId = model.Id});
                            HttpContext.GetOwinContext().GetUserManager<LuckyCoreContext>().SaveChanges();
                        }
                        var result = manager.Update(entity);
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                        if (!ModelState.IsValid) return View();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    return Redirect("/SiteManager/SysUsers/Index");
                }
            }
            return View(model);
        }*/
        [Resource("验证用户名")]
        public async Task<IActionResult> ValidateUserName(string id, string userName)
        {
            bool user =await _usersService.Exits(id,userName);//_navRepository.Single(a => a.NavId != navId && a.NavName == navName);
            if (!user)
                return Json(true);
            return Json("用户名称已经存在！");
        }
    }
}