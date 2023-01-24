using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class StudentConfiguration
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(id => id.Id);
            builder.Property(j => j.Name).HasMaxLength(48).IsRequired();
            builder.Property(p => p.Surname).HasMaxLength(48).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(62).IsRequired();
        }
    }
}
