using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class BlogPostPictureConfig : IEntityTypeConfiguration<BlogPostPicture>
    {
        public void Configure(EntityTypeBuilder<BlogPostPicture> builder)
        {
            builder.HasKey(bp => new { bp.BlogPostId, bp.PictureId });
        }
    }
}

