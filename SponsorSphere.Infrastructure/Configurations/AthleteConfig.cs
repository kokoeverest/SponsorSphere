using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class AthleteConfig : IEntityTypeConfiguration<Athlete>
    {
        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder.Property(ath => ath.LastName)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(ath => ath.Sport)
                .HasConversion<int>();


            builder.HasMany(ath => ath.Sponsorships)
                .WithOne()
                .HasForeignKey(s => s.AthleteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ath => ath.Goals)
                .WithOne()
                .HasForeignKey(s => s.AthleteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ath => ath.Achievements)
                .WithOne()
                .HasForeignKey(s => s.AthleteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
