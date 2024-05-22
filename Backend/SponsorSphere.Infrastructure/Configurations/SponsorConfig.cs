using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class SponsorConfig : IEntityTypeConfiguration<Sponsor>
    {
        public void Configure(EntityTypeBuilder<Sponsor> builder)
        {
            builder.ToTable("Sponsors");

            builder.HasMany(ath => ath.Sponsorships)
                .WithOne()
                .HasForeignKey(s => s.SponsorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
