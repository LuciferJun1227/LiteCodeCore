using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.OauthBase.Mapping
{
    public class SysDepartmentMapping:EntityMappingConfiguration<SysDepartment>
    {
        public override void Map(EntityTypeBuilder<SysDepartment> b)
        {
            b.HasKey(t => t.DepartmentId);

            // Properties
            b.Property(t => t.DepartmentId)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(t => t.ParentId)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(t => t.DepartmentName)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            b.ToTable("Department");
            b.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            b.Property(t => t.ParentId).HasColumnName("ParentId");
            b.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            b.Property(t => t.Description).HasColumnName("Description");
            b.Property(t => t.State).HasColumnName("State");
            b.Property(t => t.Sort).HasColumnName("Sort");

            // Relationships

            //b.HasOne(t => t.Parent)
            //    .WithMany(t => t.Departments)
            //    .HasForeignKey(d => d.ParentId);
        }
    }
}
