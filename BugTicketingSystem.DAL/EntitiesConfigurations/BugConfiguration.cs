using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL;

public class BugConfiguration : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).HasMaxLength(255);
        builder.Property(b => b.Status).HasMaxLength(255);
    }
}
