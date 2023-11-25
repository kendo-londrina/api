using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ken_lo.Domain.Familias;

namespace ken_lo.Infra.Data.Config;

public class FamiliaConfig : IEntityTypeConfiguration<Familia>
{
    public void Configure(EntityTypeBuilder<Familia> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.EscolaId, t.Nome })
            .IsUnique(true);
        builder.Property(t => t.Nome)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        builder.ToTable("Familias");
    }
}
