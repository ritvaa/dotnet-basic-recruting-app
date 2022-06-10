using MatchDataManager.Api.Configuration;
using MatchDataManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchDataManager.Api;

public class MatchDataManagerDbContext : DbContext
{
    public MatchDataManagerDbContext(DbContextOptions<MatchDataManagerDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new LocationConfiguration());
        builder.ApplyConfiguration(new TeamConfiguration());
    }
}