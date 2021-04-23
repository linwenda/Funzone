﻿using Funzone.Domain.ZoneUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.ZoneUsers
{
    public class ZoneUserEntityTypeConfiguration : IEntityTypeConfiguration<ZoneUser>
    {
        public void Configure(EntityTypeBuilder<ZoneUser> builder)
        {
            builder.ToTable("ZoneUsers");

            builder.HasKey(p => new {p.ZoneId, p.UserId});

            builder.OwnsOne(p => p.Role, r =>
            {
                r.Property(rp => rp.Value)
                    .HasColumnName("Role")
                    .HasColumnType("varchar(20)");
            });
        }
    }
}