using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.OauthBase.Mapping
{
    public class SysModulesMapping : EntityMappingConfiguration<SysModules>
    {
        public override void Map(EntityTypeBuilder<SysModules> builder)
        {
            builder.ToTable("Sys_Modules");
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.Application).WithMany(b=>b.Moduleses).HasForeignKey(ur => ur.ApplicationId);
        }
    }
}
