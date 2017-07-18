using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LuckyCode.Entity.IdentityEntity
{
    public class SysRoleClaims : IdentityRoleClaim<string>
    {
        
    }
    public class SysRoles : IdentityRole<string>
    {
        public string RoleName { get; set; }
        public int RoleType { get; set; }
        public string RoleDescription { get; set; }
        public int Sort { get; set; }
        public bool IsAllowDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
   
}
