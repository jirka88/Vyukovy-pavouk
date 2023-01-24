using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class GroupStudentConfiguration : IEntityTypeConfiguration<GroupStudent>
    {
        // Skupina : Student --> M:N
        public void Configure(EntityTypeBuilder<GroupStudent> builder)
        {
            builder.HasKey(id => new { id.GroupID, id.StudentID });
            builder.HasOne(s => s.Group)
                .WithMany(s => s.GroupStudents)
                .HasForeignKey(s => s.GroupID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Student)
                .WithMany(s => s.GroupStudents)
                .HasForeignKey(s => s.StudentID);
        }
    }
}
