using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class BlogPostPictureConfig : IEntityTypeConfiguration<BlogPostPicture>
    {
        public void Configure(EntityTypeBuilder<BlogPostPicture> builder)
        {
            builder.ToTable("BlogPostPictures");

            builder.HasKey(bp => new { bp.BlogPostId, bp.PictureId });
            builder
                .HasQueryFilter(bp => !bp.BlogPost.Author.IsDeleted);
        }
    }
}

