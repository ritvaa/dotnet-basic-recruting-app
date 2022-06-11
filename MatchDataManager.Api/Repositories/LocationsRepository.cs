using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public class LocationsRepository : ILocationsRepository
{
    private readonly MatchDataManagerDbContext _context;
    
    public LocationsRepository(MatchDataManagerDbContext context)
    {
        _context = context;
    }

    public void AddLocation(Location location)
    {
        location.Id = Guid.NewGuid();
        
        if (!(IsLocationNameUnique(location.Name)))
        {
            throw new ArgumentException("Location name must be unique");
        }
        _context.Add(location);
    }

    public void DeleteLocation(Guid locationId)
    {
        var location = _context.Locations.FirstOrDefault(x => x.Id == locationId);
        if (location is not null)
        {
            _context.Remove(location);
        }
    }

    public IEnumerable<Location> GetAllLocations()
    {
        return _context.Locations.ToList();
    }

    public Location GetLocationById(Guid id)
    {
        return _context.Locations.FirstOrDefault(x => x.Id == id);
    }

    public void UpdateLocation(Location location)
    {
        if (!(IsLocationNameUnique(location.Name)))
        {
            throw new ArgumentException("Location name must be unique");
        }
        var existingLocation = _context.Locations.FirstOrDefault(x => x.Id == location.Id);
        if (existingLocation is null || location is null)
        {
            throw new ArgumentException("Location doesn't exist.", nameof(location));
        }

        existingLocation.City = location.City;
        existingLocation.Name = location.Name;
    }
    
    public bool IsLocationNameUnique(string locationName)
    {
        var location = _context.Locations.FirstOrDefault(x => x.Name == locationName);
        if (location is null)
        {
            return true;
        }
        return false;
    }
}