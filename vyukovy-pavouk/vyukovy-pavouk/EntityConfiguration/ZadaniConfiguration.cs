using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class ZadaniConfiguration : IEntityTypeConfiguration<Zadani>
    {
        public void Configure(EntityTypeBuilder<Zadani> builder)
        {
            builder.Property(o => o.Odkaz).HasMaxLength(2048).IsRequired();
            builder.HasKey(id => id.Id);
            builder.HasOne(k => k.kapitola)
                .WithMany(z => z.Zadani)
                .HasForeignKey(id => id.KapitolaID);
        }
    }
}
