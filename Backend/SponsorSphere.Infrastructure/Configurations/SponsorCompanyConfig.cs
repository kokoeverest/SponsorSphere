using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class SponsorCompanyConfig : IEntityTypeConfiguration<SponsorCompany>
    {
        public void Configure(EntityTypeBuilder<SponsorCompany> builder)
        {
            builder.Property(sc => sc.Iban)
                .IsRequired(true)
                .HasMaxLength(34);
        }
    }
}
