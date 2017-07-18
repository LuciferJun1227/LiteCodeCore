using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LuckyCode.Entity.IdentityEntity
{
    public class SysUserTokens : IdentityUserToken<string>
    {
        
    }
    public class SysUsers : IdentityUser<string>
    {
        public string DepartmentId { get; set; }
        public string FullName { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsLock { get; set; }
    }

}
