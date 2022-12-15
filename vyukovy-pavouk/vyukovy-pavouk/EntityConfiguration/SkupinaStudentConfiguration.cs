using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class SkupinaStudentConfiguration : IEntityTypeConfiguration<SkupinaStudent>
    {
        // Skupina : Student --> M:N
        public void Configure(EntityTypeBuilder<SkupinaStudent> builder)
        {
            builder.HasKey(id => new { id.SkupinaID, id.StudentID });
            builder.HasOne(s => s.Skupina)
                .WithMany(s => s.Student)
                .HasForeignKey(s => s.SkupinaID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Student)
                .WithMany(s => s.SkupinaStudent)
                .HasForeignKey(s => s.StudentID);
        }
    }
}
