using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public static class TeamsRepository
{
    private static readonly List<Team> _teams = new();

    public static void AddTeam(Team team)
    {
        team.Id = Guid.NewGuid();
        _teams.Add(team);
    }

    public static void DeleteTeam(Guid teamId)
    {
        var team = _teams.FirstOrDefault(x => x.Id == teamId);
        if (team is not null)
        {
            _teams.Remove(team);
        }
    }

    public static IEnumerable<Team> GetAllTeams()
    {
        return _teams;
    }

    public static Team GetTeamById(Guid id)
    {
        return _teams.FirstOrDefault(x => x.Id == id);
    }

    public static void UpdateTeam(Team team)
    {
        var existingTeam = _teams.FirstOrDefault(x => x.Id == team.Id);
        if (existingTeam is null || team is null)
        {
            throw new ArgumentException("Team doesn't exist.", nameof(team));
        }

        existingTeam.CoachName = team.CoachName;
        existingTeam.Name = team.Name;
    }
}