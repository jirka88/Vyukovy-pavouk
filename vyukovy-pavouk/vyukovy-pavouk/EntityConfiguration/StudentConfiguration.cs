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
            builder.Property(j => j.Jmeno).HasMaxLength(48).IsRequired();
            builder.Property(p => p.Prijmeni).HasMaxLength(48).IsRequired();
            builder.Property(e => e.email).HasMaxLength(62).IsRequired();
        }
    }
}
