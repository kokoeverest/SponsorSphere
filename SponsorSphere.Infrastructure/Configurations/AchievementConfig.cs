using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class AchievementConfig : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.Property(a => a.Sport)
                 .HasConversion<int>();


            builder.HasKey(a => new { a.AthleteId, a.SportEventId });

            builder.HasOne(a => a.SportEvent)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
