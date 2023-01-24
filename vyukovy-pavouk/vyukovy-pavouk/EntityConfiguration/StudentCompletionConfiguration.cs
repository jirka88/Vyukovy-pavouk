using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class StudentCompletionConfiguration : IEntityTypeConfiguration<StudentCompletion>
    {
        // student : splneni --> M : N 
        public void Configure(EntityTypeBuilder<StudentCompletion> builder)
        {
            builder.HasKey(id => new { id.StudentID, id.CompletionID });
            builder.HasOne(s => s.Student)
                .WithMany(s => s.StudentCompletion)
                .HasForeignKey(id => id.StudentID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Completion)
                .WithMany(s => s.StudentCompletions)
                .HasForeignKey(id => id.CompletionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
