using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
        public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
        {
            public void Configure(EntityTypeBuilder<Subject> builder)
            {
                builder.HasKey(i => i.Id);
                builder.Property(n => n.Name).HasMaxLength(30).IsRequired();
                builder.Property(v => v.Created).HasMaxLength(62).IsRequired();
            }
        }

}
