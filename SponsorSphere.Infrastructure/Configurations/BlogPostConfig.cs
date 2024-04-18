using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class BlogPostConfig : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            //builder.HasOne(bp => bp.Author)
            //    .WithMany(u => u.Posts)
            //    .HasForeignKey(bp => bp.Author)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
