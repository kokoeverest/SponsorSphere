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

            builder.Ignore(a => a.AthleteAchievement);

            builder.HasKey(a => new { a.AthleteAchievement, a.SportEvent });
            
        }
    }
}
