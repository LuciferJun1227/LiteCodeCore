using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.News.Mapping
{
    public class LinkMap : EntityMappingConfiguration<Link>
    {
        public override void Map(EntityTypeBuilder<Link> b)
        {
            // Primary Key
            b.HasKey(t => t.LinkID);

            // Properties
            b.Property(t => t.Title)
                .HasMaxLength(150);

            b.Property(t => t.UserName)
                .HasMaxLength(50);

            b.Property(t => t.UserTel)
                .HasMaxLength(50);

            b.Property(t => t.UserEmail)
                .HasMaxLength(150);

            b.Property(t => t.WebUrl)
                .IsRequired()
                .HasMaxLength(150);

            b.Property(t => t.ImageUrl)
                .HasMaxLength(150);

            // Table & Column Mappings
            b.ToTable("Links");
            b.Property(t => t.LinkID).HasColumnName("LinkID");
            b.Property(t => t.Title).HasColumnName("Title");
            b.Property(t => t.UserName).HasColumnName("UserName");
            b.Property(t => t.UserTel).HasColumnName("UserTel");
            b.Property(t => t.UserEmail).HasColumnName("UserEmail");
            b.Property(t => t.IsImage).HasColumnName("IsImage");
            b.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
            b.Property(t => t.WebUrl).HasColumnName("WebUrl");
            b.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            b.Property(t => t.IsLock).HasColumnName("IsLock");
            b.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
