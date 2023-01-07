using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class KapitolaPrerekvizitaConfiguration : IEntityTypeConfiguration<KapitolaPrerekvizita>
    {
        //kapitola : prerekvizity --> M : N 
        public void Configure(EntityTypeBuilder<KapitolaPrerekvizita> builder)
        {
            builder.HasKey(id => new { id.KapitolaID, id.PrerekvizitaID });
            builder.HasKey(id => id.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(k => k.kapitola)
                .WithMany(k => k.KapitolaPrerekvizita)
                .HasForeignKey(id => id.KapitolaID);
            builder.HasOne(p => p.prerekvizita)
                .WithMany(p => p.KapitolaPrerekvizita)
                .HasForeignKey(id => id.PrerekvizitaID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
