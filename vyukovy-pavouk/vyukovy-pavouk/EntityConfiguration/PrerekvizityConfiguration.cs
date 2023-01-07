using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
        public class PrerekvizityConfiguration : IEntityTypeConfiguration<Prerekvizity>
        {
            public void Configure(EntityTypeBuilder<Prerekvizity> builder)
            {
                builder.HasKey(id => id.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
        }
    }
