using Funzone.Domain.Zones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class ZoneEntityTypeConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.ToTable("Zones");

            builder.HasKey(p => p.Id);
        }
    }
}