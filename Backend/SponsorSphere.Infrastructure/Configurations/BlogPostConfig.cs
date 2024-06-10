using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class BlogPostConfig : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(bp => bp.Content)
                .IsRequired();

            builder.Property(bp => bp.AuthorId)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(bp => bp.Created)
                .HasConversion<DateTime>()
                .IsRequired();
        }
    }
}
