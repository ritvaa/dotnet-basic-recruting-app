using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public interface ILocationsRepository
{
    public void AddLocation(Location location);
    public void DeleteLocation(Guid locationId);
    public IEnumerable<Location> GetAllLocations();
    public Location GetLocationById(Guid id);
    public void UpdateLocation(Location location);
    public bool IsLocationNameUnique(string locationName);
    
}