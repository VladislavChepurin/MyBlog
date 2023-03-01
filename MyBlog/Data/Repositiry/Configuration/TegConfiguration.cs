using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models.Articles;

namespace MyBlog.Data.Repository
{
    public class TegConfiguration : IEntityTypeConfiguration<Teg>
    {
        public void Configure(EntityTypeBuilder<Teg> builder)
        {
            builder.ToTable("TegTable").HasKey(p => p.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();
        }

    }
}
