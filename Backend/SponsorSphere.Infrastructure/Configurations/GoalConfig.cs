﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SponsorSphere.Domain.Models;
using System.Reflection.Emit;

namespace SponsorSphere.Infrastructure.Configurations
{
    public class GoalConfig : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.Property(g => g.Sport)
                .HasConversion<int>();

            builder.Property(g => g.AmountNeeded)
                .HasConversion<decimal>();


            builder.HasKey(g => new { g.AthleteId, g.SportEventId });

            builder
                .HasOne(g => g.Athlete)
                .WithMany()
                .HasForeignKey(g => g.AthleteId)
                .HasPrincipalKey(u => u.Id);

            builder
                .HasQueryFilter(g => !g.Athlete!.IsDeleted);

        }
    }
}
