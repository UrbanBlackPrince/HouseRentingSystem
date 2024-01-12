﻿using HouseRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Data.Configurations
{
    public class HouseEntityConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder
                .HasOne(h => h.Category)
                .WithMany(x => x.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Agent)
                 .WithMany(x => x.OwnedHouses)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

