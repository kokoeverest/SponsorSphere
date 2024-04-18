using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;
using System.Reflection.Emit;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class AchievementConfig : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.Property(a => a.Sport)
                 .HasConversion<int>();

            builder.HasOne(achievement => achievement.Athlete)
                .WithMany()
                .HasForeignKey(achievement => achievement.AthleteId);

            builder.HasOne(achievement => achievement.SportEvent)
                .WithMany()
                .HasForeignKey(achievement => achievement.SportEventId);

            builder.HasKey(a => new { a.AthleteId, a.SportEventId });            
        }
    }
}
