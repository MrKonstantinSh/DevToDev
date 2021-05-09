using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevToDev.Infrastructure.Persistence.Configurations.Article
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Domain.Entities.Article.Article>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Article.Article> builder)
        {
            builder.Property(a => a.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(a => a.Url)
                .IsRequired();

            builder.Property(a => a.DateOfCreation)
                .IsRequired();

            builder.Property(a => a.DateOfLastUpdate)
                .IsRequired();
        }
    }
}