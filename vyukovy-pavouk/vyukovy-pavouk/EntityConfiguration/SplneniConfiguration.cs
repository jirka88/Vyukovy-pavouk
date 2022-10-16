using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class SplneniConfiguration
    {
        public class PredmetConfiguration : IEntityTypeConfiguration<Splneni>
        {
            public void Configure(EntityTypeBuilder<Splneni> builder)
            {
                builder.HasKey(id => id.Id);
                builder.HasOne(s => s.student)
                    .WithMany(s => s.Splneni)
                    .HasForeignKey(s => s.Id_studenta);
            }
        }
    }
}
