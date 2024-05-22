using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class SponsorshipConfig : IEntityTypeConfiguration<Sponsorship>
    {
        public void Configure(EntityTypeBuilder<Sponsorship> builder)
        {
            builder.Property(s => s.Level)
                .HasConversion<int>();

            builder.Property(s => s.Amount)
                .HasConversion<decimal>();


            builder.HasKey(s => new { s.AthleteId, s.SponsorId });

            builder
                .HasOne(s => s.Athlete)
                .WithMany()
                .HasForeignKey(s => s.AthleteId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasQueryFilter(s => !s.Athlete!.IsDeleted);

            builder
                .HasOne(s => s.Sponsor)
                .WithMany()
                .HasForeignKey(s => s.SponsorId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(s => !s.Sponsor!.IsDeleted);
        }
    }
}
