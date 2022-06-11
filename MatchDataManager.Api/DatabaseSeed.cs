using MatchDataManager.Api.Models;

namespace MatchDataManager.Api;

public class DatabaseSeed
{
    private readonly MatchDataManagerDbContext _context;

    public DatabaseSeed(MatchDataManagerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        var locationsList = BuildLocationList();
        _context.Locations.AddRange(locationsList);
        _context.SaveChanges();
        
        var teamsList = BuildTeamsList();
        _context.Teams.AddRange(teamsList);
        _context.SaveChanges();
    }

    private IEnumerable<Location> BuildLocationList()
    {
        var locationsList = new List<Location>();
        var location1 = new Location()
        {
            Id = Guid.ParseExact("86e11657-e38f-4041-8081-5164155ff364", "D"),
            Name = "Location1",
            City = "City1"
        };
        locationsList.Add(location1);

        var location2 = new Location()
        {
            Id = Guid.ParseExact("82712f6f-51ca-4724-9b38-365840344c93", "D"),
            Name = "Location2",
            City = "City2"
        };
        locationsList.Add(location2);

        var location3 = new Location()
        {
            Id = Guid.ParseExact("d6b19ebd-846c-463c-951a-25dade9dbfb9", "D"),
            Name = "Location3",
            City = "City3"
        };
        locationsList.Add(location3);

        return locationsList;
    }

    private IEnumerable<Team> BuildTeamsList()
    {
        var teamsList = new List<Team>();
        var team1 = new Team()
        {
            CoachName = "CoachName1",
            Id = Guid.ParseExact("359757c5-c5d8-48ee-a702-dcc6e239d083", "D"),
            Name = "TeamName1"
        };
        
        var team2 = new Team()
        {
            CoachName = "CoachName2",
            Id = Guid.ParseExact("33ddfc6a-5887-44db-923e-01f9c1cae38e", "D"),
            Name = "TeamName2"
        };
        
        var team3 = new Team()
        {
            CoachName = "CoachName3",
            Id = Guid.ParseExact("5af1ec5a-895b-47ae-9c0c-ec56e1293865", "D"),
            Name = "TeamName3"
        };

        return teamsList;
    }

}
