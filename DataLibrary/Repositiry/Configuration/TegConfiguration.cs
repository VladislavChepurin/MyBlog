using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Contracts.Models.Tegs;

namespace DataLibrary.Data.Repository;

public class TegConfiguration : IEntityTypeConfiguration<Teg>
{
    public void Configure(EntityTypeBuilder<Teg> builder)
    {
        builder.ToTable("TegTable").HasKey(p => p.Id);            
    }

}
