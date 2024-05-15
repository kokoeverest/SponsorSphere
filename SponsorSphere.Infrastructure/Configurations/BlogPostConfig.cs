using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class BlogPostConfig : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasMany(bp => bp.Pictures)
               .WithMany()
               .UsingEntity<BlogPostPicture>(
                   j => j.HasOne(bp => bp.Picture)
                         .WithMany()
                         .HasForeignKey(bp => bp.PictureId),
                   j => j.HasOne(bp => bp.BlogPost)
                         .WithMany()
                         .HasForeignKey(bp => bp.BlogPostId),
                   j =>
                   {
                       j.ToTable("BlogPostPictures");
                       j.HasKey(bp => new { bp.BlogPostId, bp.PictureId });
                   });

            builder
                .HasQueryFilter(bp => !bp.Author!.IsDeleted);
        }
    }
}
