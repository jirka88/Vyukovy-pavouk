using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
        public class PredmetConfiguration : IEntityTypeConfiguration<Predmet>
        {
            public void Configure(EntityTypeBuilder<Predmet> builder)
            {
                builder.HasKey(i => i.Id);
                builder.Property(n => n.Nazev).HasMaxLength(30).IsRequired();
            }
        }

}
