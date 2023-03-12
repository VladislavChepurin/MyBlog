using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models;

namespace MyBlog.Data.Repositiry.Configuration;

public class ArticleTegConfiguration : IEntityTypeConfiguration<ArticleTeg>
{
    public void Configure(EntityTypeBuilder<ArticleTeg> builder)
    {
        builder.ToTable("ArticleTeg").HasKey(sc => new { sc.ArticlesId, sc.TegsId});
    }
}
