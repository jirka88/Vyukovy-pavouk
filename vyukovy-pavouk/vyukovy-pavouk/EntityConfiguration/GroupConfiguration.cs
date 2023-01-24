using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(id => id.Id);
            builder.Property(TM => TM.TmGroup).HasMaxLength(2048).IsRequired();
            builder.HasOne(x => x.Subject)
                    .WithOne(x => x.Group)
                    .HasForeignKey<Subject>(y => y.GroupID); 
        }
    }
}
