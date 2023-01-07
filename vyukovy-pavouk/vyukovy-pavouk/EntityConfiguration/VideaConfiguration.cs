using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class VideaConfiguration : IEntityTypeConfiguration<Videa>
    {
        public void Configure(EntityTypeBuilder<Videa> builder)
        {
            builder.Property(o => o.Odkaz).HasMaxLength(2048).IsRequired();
            builder.Property(n => n.Nazev).HasMaxLength(15);
            builder.HasKey(i => i.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            // kapitola : Videa --> 1 : M
            builder.HasOne(i => i.kapitola)
                .WithMany(v => v.Videa)
                .HasForeignKey(p => p.KapitolaID);
        }

}
}
