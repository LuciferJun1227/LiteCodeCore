using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Entity.News.Mapping
{
    public class NewsArticleTextMap : EntityMappingConfiguration<NewsArticleText>
    {
        public override void Map(EntityTypeBuilder<NewsArticleText> b)
        {
            // Primary Key
            b.HasKey(t => t.ArticleTextID);

            // Properties
            // Table & Column Mappings
            b.ToTable("NewsArticleText");
            b.Property(t => t.ArticleTextID).HasColumnName("ArticleTextID");
            b.Property(t => t.ArticleID).HasColumnName("ArticleID");
            b.Property(t => t.ArticleText).HasColumnName("ArticleText").HasColumnType("text");
            b.Property(t => t.NoHtml).HasColumnName("NoHtml").HasColumnType("text");

            // Relationships
            b.HasOne(t => t.NewsArticle)
                .WithMany(t => t.NewsArticleTexts)
                .HasForeignKey(d => d.ArticleID);

        }
    }
}
