using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.Property(o => o.Reference).HasMaxLength(2048).IsRequired();
            builder.HasKey(id => id.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(k => k.Chapter)
                .WithMany(z => z.Assignments)
                .HasForeignKey(id => id.ChapterID);
        }
    }
}
