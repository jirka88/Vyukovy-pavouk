using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class StudentSplneniConfiguration : IEntityTypeConfiguration<StudentSplneni>
    {
        // student : splneni --> M : N 
        public void Configure(EntityTypeBuilder<StudentSplneni> builder)
        {
            builder.HasKey(id => new { id.StudentId, id.SplneniId });
            builder.HasOne(s => s.student)
                .WithMany(s => s.StudentSplneni)
                .HasForeignKey(id => id.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.splneni)
                .WithMany(s => s.StudentSplneni)
                .HasForeignKey(id => id.SplneniId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
