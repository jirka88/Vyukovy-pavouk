using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class SkupinaConfiguration : IEntityTypeConfiguration<Skupina>
    {
        public void Configure(EntityTypeBuilder<Skupina> builder)
        {
            builder.HasKey(id => id.Id);
            builder.Property(TM => TM.TmSkupina).HasMaxLength(2048).IsRequired();
            builder.HasOne(x => x.predmet)
                    .WithOne(x => x.Skupina)
                    .HasForeignKey<Predmet>(y => y.SkupinaID); 
        }
    }
}
