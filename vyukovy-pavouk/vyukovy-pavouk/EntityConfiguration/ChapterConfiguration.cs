using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter> 
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(n => n.Name).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Perex).HasMaxLength(255);
            builder.Property(c => c.Content).IsRequired();

            // 1:M --> Predmet : kapitoly
            builder.HasOne(p => p.Subject)
               .WithMany(k => k.Chapters)
               .HasForeignKey(p => p.SubjectID)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
