using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models;

namespace MyBlog.Data.Repositiry.Configuration
{
    public class InviteConfiguration : IEntityTypeConfiguration<Invate>
    {
        public void Configure(EntityTypeBuilder<Invate> builder)
        {
            builder.ToTable("InviteTable").HasKey(p => p.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
