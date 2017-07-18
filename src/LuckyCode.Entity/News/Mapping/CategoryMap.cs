using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.News.Mapping
{
    public class CategoryMap : EntityMappingConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> b)
        {
            {
                // Primary Key
                b.HasKey(t => t.CategoryId);

                // Properties
                b.Property(t => t.CategoryId)
                    .IsRequired()
                    .HasMaxLength(30);

                b.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                b.Property(t => t.Description)
                    .HasMaxLength(50);

                b.Property(t => t.HyperLink)
                    .HasMaxLength(250);

                b.Property(t => t.ParentId)
                    .IsRequired()
                    .HasMaxLength(30);

                b.Property(t => t.SortCode)
                    .HasMaxLength(10);

                b.Property(t => t.CategoryType)
                    .HasMaxLength(50);

                // Table & Column Mappings
                b.ToTable("Category");
                b.Property(t => t.CategoryId).HasColumnName("CategoryID");
                b.Property(t => t.Title).HasColumnName("Title");
                b.Property(t => t.Description).HasColumnName("Description");
                b.Property(t => t.HyperLink).HasColumnName("HyperLink");
                b.Property(t => t.ParentId).HasColumnName("ParentID");
                b.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
                b.Property(t => t.SortCode).HasColumnName("SortCode");
                b.Property(t => t.CreateDate).HasColumnName("CreateDate");
                b.Property(t => t.CategoryType).HasColumnName("CategoryType");
            }
        }
    }
}
