using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class SponsorIndividualConfig : IEntityTypeConfiguration<SponsorIndividual>
    {
        public void Configure(EntityTypeBuilder<SponsorIndividual> builder)
        {
            builder.Property(si => si.LastName)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(si => si.BirthDate)
                .IsRequired(true);
        }
    }
}
