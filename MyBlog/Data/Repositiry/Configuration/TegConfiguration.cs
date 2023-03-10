using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models.Tegs;

namespace MyBlog.Data.Repository;

public class TegConfiguration : IEntityTypeConfiguration<Teg>
{
    public void Configure(EntityTypeBuilder<Teg> builder)
    {
        builder.ToTable("TegTable").HasKey(p => p.Id);            
    }

}
