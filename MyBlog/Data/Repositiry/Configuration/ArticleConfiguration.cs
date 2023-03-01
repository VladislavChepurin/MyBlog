using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models.Articles;

namespace MyBlog.Data.Repository
{
    public class ArticleConfiguration: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("ArticleTable").HasKey(p => p.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();
        }

    }
}
