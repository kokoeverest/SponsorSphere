using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class SportEventConfig : IEntityTypeConfiguration<SportEvent>
    {
        public void Configure(EntityTypeBuilder<SportEvent> builder)
        {

            builder.Property(se => se.Sport)
                .HasConversion<int>();

            builder.Property(se => se.EventType)
                 .HasConversion<int>();

            builder.Property(se => se.Name)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(se => se.Country)
                .IsRequired(true)
                .HasConversion<int>();
        }
    }
}
