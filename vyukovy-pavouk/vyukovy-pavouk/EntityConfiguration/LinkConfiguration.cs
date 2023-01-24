using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.Property(o => o.Reference).HasMaxLength(2048).IsRequired();
            builder.Property(n => n.Name).HasMaxLength(15);
            builder.HasKey(i => i.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            // kapitola : Videa --> 1 : M
            builder.HasOne(i => i.Chapter)
                .WithMany(v => v.Links)
                .HasForeignKey(p => p.ChapterID);
        }

}
}
