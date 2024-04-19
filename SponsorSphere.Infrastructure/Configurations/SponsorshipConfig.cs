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

            builder.Property(s => s.SponsorCompanyId)
                .IsRequired(false);
            
            builder.Property(s => s.SponsorIndividualId)
                .IsRequired(false);


            builder.HasKey(s => new { s.AthleteId, s.SponsorCompanyId, s.SponsorIndividualId });
        }
    }
}
