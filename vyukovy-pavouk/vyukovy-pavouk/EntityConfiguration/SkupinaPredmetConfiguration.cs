using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class SkupinaPredmetConfiguration : IEntityTypeConfiguration<SkupinaPredmet>
    {
        // Skupina : Predmet --> M:N
        public void Configure(EntityTypeBuilder<SkupinaPredmet> builder)
        {
            builder.HasKey(id => new {id.SkupinaId, id.PredmetId});
            builder.HasOne(s => s.Skupina)
                .WithMany(s => s.SkupinaPredmet)
                .HasForeignKey(s => s.SkupinaId);
            builder.HasOne(s => s.Predmet)
                .WithMany(s => s.SkupinaPredmet)
                .HasForeignKey(s => s.PredmetId);
        }
    }
}

