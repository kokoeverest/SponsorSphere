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
                .WithMany(p => p.BlogPosts);


            //builder.HasMany(p => p.Pictures)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
