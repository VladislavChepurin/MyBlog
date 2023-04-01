using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Contracts.Models.Articles;

namespace DataLibrary.Data.Repository;

public class ArticleConfiguration: IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("ArticleTable").HasKey(p => p.Id);          
    }
}
