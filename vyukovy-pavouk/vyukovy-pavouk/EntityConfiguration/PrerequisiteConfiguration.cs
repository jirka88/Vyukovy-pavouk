using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
        public class PrerequisiteConfiguration : IEntityTypeConfiguration<Prerequisite>
        {
            public void Configure(EntityTypeBuilder<Prerequisite> builder)
            {
                builder.HasKey(id => id.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
        }
    }
