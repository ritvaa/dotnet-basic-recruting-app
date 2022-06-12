using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public class TeamsRepository : ITeamRepository
{
    private readonly MatchDataManagerDbContext _context;

    public TeamsRepository(MatchDataManagerDbContext context)
    {
        _context = context;
    }
    public void AddTeam(Team team)
    {
        team.Id = Guid.NewGuid();
        if (!(IsTeamNameUnique(team.Name)))
        {
            throw new ArgumentException("Team name must be unique");
        }
        _context.Add(team);
        _context.SaveChanges();
    }

    public void DeleteTeam(Guid teamId)
    {
        var team = _context.Teams.FirstOrDefault(x => x.Id == teamId);
        if (team is not null)
        {
            _context.Remove(team);
        }
        _context.SaveChanges();
    }

    public IEnumerable<Team> GetAllTeams()
    {
        return _context.Teams;
    }

    public Team GetTeamById(Guid id)
    {
        return _context.Teams.FirstOrDefault(x => x.Id == id);
    }

    public void UpdateTeam(Team team)
    {
        if (!(IsTeamNameUnique(team.Name)))
        {
            throw new ArgumentException("Team name must be unique");
        }
        var existingTeam = _context.Teams.FirstOrDefault(x => x.Id == team.Id);

        if (existingTeam is null || team is null)
        {
            throw new ArgumentException("Team doesn't exist.", nameof(team));
        }

        existingTeam.CoachName = team.CoachName;
        existingTeam.Name = team.Name;
        _context.SaveChanges();
    }

    public bool IsTeamNameUnique(string Name)
    {
        var team = _context.Teams.FirstOrDefault(x => x.Name == Name);
        if (team is null)
        {
            return true;
        }
        return false;
    }
}