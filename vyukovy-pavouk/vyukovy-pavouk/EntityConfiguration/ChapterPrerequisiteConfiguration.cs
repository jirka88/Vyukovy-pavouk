using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class ChapterPrerequisiteConfiguration : IEntityTypeConfiguration<ChapterPrerequisite>
    {
        //kapitola : prerekvizity --> M : N 
        public void Configure(EntityTypeBuilder<ChapterPrerequisite> builder)
        {
            builder.HasKey(id => new { id.ChapterID, id.PrerequisiteID });
            builder.HasKey(id => id.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(k => k.Chapter)
                .WithMany(k => k.ChapterPrerequisites)
                .HasForeignKey(id => id.ChapterID);
            builder.HasOne(p => p.Prerequisite)
                .WithMany(p => p.ChapterPrerequisites)
                .HasForeignKey(id => id.PrerequisiteID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
