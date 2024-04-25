﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(u => u.Email)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(u => u.Country)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                .IsRequired(true)
                .HasMaxLength(16);

            builder.Property(u => u.Website)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(u => u.FaceBookLink)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(u => u.InstagramLink)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(u => u.TwitterLink)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(u => u.StravaLink)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(u => u.PictureId)
                .IsRequired(false);


            builder.HasMany(u => u.Posts)
                .WithOne()
                .HasForeignKey(u => u.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(u => u.PictureOrLogo)
            //    .WithOne()
            //    .HasForeignKey(p => (Picture)p)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
