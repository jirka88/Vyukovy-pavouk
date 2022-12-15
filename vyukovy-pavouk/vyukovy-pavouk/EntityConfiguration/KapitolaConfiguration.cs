using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class KapitolaConfiguration : IEntityTypeConfiguration<Kapitola> 
    {
        public void Configure(EntityTypeBuilder<Kapitola> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(n => n.Název).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Perex).HasMaxLength(255);
            builder.Property(c => c.Kontent).IsRequired();

            // 1:M --> Predmet : kapitoly
            builder.HasOne(p => p.predmet)
               .WithMany(k => k.Kapitoly)
               .HasForeignKey(p => p.PredmetID)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
