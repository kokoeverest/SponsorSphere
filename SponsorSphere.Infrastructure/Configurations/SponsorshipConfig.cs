//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using SponsorSphere.Domain.Models;

//namespace SponsorSphere.Infrastructure.Configurations
//{
//    public class SponsorshipConfig : IEntityTypeConfiguration<Sponsorship>
//    {
//        public void Configure(EntityTypeBuilder<Sponsorship> builder)
//        {
//            builder.Property(s => s.Level)
//                .HasConversion<int>();

//            builder.Property(s => s.Amount)
//                .HasConversion<decimal>();

//            //builder.HasKey(s => new { s.Athlete, s.Sponsor });

//            builder.HasOne(s => s.AthleteSponsorship)
//                .WithMany(ath => ath.Sponsorships)
//                .HasForeignKey(s => s.AthleteSponsorship)
//                .OnDelete(DeleteBehavior.NoAction);

//            builder.HasOne(s => s.SponsorCompany)
//                .WithMany(sp => sp.Sponsorships)
//                .HasForeignKey(s => s.SponsorCompany)
//                .OnDelete(DeleteBehavior.NoAction);

//            builder.HasOne(s => s.SponsorIndividual)
//                .WithMany(sp => sp.Sponsorships)
//                .HasForeignKey(s => s.SponsorIndividual)
//                .OnDelete(DeleteBehavior.NoAction);
//        }
//    }
//}
