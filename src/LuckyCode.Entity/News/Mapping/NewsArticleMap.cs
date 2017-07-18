using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.News.Mapping
{
    public class NewsArticleMap : EntityMappingConfiguration<NewsArticle>
    {
        public override void Map(EntityTypeBuilder<NewsArticle> b)
        {
            // Primary Key
            b.HasKey(t => t.ArticleID);

            // Properties
            b.Property(t => t.CategoryID)
                .IsRequired()
                .HasMaxLength(30);

            b.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(250);

            b.Property(t => t.Summarize)
                .HasMaxLength(500);

            b.Property(t => t.Source)
                .HasMaxLength(150);

            b.Property(t => t.Author)
                .HasMaxLength(50);

            b.Property(t => t.Editor)
                .HasMaxLength(50);

            b.Property(t => t.KeyWord)
                .HasMaxLength(150);

            b.Property(t => t.ImgUrl)
                .HasMaxLength(250);

            b.Property(t => t.UserID)
                .HasMaxLength(128);

            // Table & Column Mappings
            b.ToTable("NewsArticles");
            b.Property(t => t.ArticleID).HasColumnName("ArticleID");
            b.Property(t => t.CategoryID).HasColumnName("CategoryID");
            b.Property(t => t.Title).HasColumnName("Title");
            b.Property(t => t.Summarize).HasColumnName("Summarize");
            b.Property(t => t.Source).HasColumnName("Source");
            b.Property(t => t.Author).HasColumnName("Author");
            b.Property(t => t.Editor).HasColumnName("Editor");
            b.Property(t => t.KeyWord).HasColumnName("KeyWord");
            b.Property(t => t.IsTop).HasColumnName("IsTop");
            b.Property(t => t.IsHot).HasColumnName("IsHot");
            b.Property(t => t.IsComment).HasColumnName("IsComment");
            b.Property(t => t.IsLock).HasColumnName("IsLock");
            b.Property(t => t.IsCommend).HasColumnName("IsCommend");
            b.Property(t => t.IsSlide).HasColumnName("IsSlide");
            b.Property(t => t.ImgUrl).HasColumnName("ImgUrl");
            b.Property(t => t.ClickNum).HasColumnName("ClickNum");
            b.Property(t => t.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime");
            b.Property(t => t.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");
            b.Property(t => t.OrgID).HasColumnName("OrgID");
            b.Property(t => t.UserID).HasColumnName("UserID");
            b.Property(t => t.IsImage).HasColumnName("IsImage");
            // Relationships
            b.HasOne(t => t.Category)
                .WithMany(t => t.NewsArticles)
                .HasForeignKey(d => d.CategoryID);

        }
    }
}
