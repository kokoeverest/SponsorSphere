using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            //builder.HasMany(p => p.BlogPosts)
            //    .WithMany(bp => bp.Pictures);

            //builder.HasMany(p => p.BlogPosts)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}