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


            builder.HasKey(a => new { a.AthleteId, a.SportEventId });

            builder
                .HasOne(u => u.Athlete)
                .WithMany()
                .HasForeignKey(u => u.AthleteId)
                .HasPrincipalKey(u => u.Id);

            builder
                .HasQueryFilter(u => !u.Athlete.IsDeleted);

        }
    }
}
