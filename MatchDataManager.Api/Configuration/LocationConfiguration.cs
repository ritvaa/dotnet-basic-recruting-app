using MatchDataManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDataManager.Api.Configuration;
public class LocationConfiguration
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.Property(l => l.Name).IsRequired();
        builder.Property(l => l.City).IsRequired();
        builder.ToTable("Location");
    }
}