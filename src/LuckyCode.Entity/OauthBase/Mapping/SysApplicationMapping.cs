using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.OauthBase.Mapping
{
    public class SysApplicationMapping : EntityMappingConfiguration<SysApplication>
    {
        public override void Map(EntityTypeBuilder<SysApplication> b)
        {
            b.ToTable("Sys_Application");
            b.HasKey(a => a.Id).HasName("Id");
            b.Property(a => a.Id).IsRequired().HasMaxLength(128);
            b.Property(a => a.ApplicationName).IsRequired().HasColumnType("nvarchar(256)").HasColumnName("ApplicationName");
            b.Property(a => a.ApplicationUrl).HasMaxLength(256).HasColumnType("nvarchar(256)").HasColumnName("ApplicationUrl");
            b.Property(a => a.CreateTime).HasColumnName("CreateTime");
        }
    }
}
