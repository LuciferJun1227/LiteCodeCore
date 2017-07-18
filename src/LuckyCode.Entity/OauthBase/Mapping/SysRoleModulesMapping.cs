using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.OauthBase.Mapping
{
    public class SysRoleModulesMapping : EntityMappingConfiguration<SysRoleModules>
    {
        public override void Map(EntityTypeBuilder<SysRoleModules> builder)
        {
            builder.ToTable("Sys_RoleModules");
            builder.HasKey(a => new {a.ModuleId, a.RoleId});
        }
    }
}
