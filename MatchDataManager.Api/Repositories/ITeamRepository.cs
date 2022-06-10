using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public interface ITeamRepository
{
    public void AddTeam(Team team);
    public void DeleteTeam(Guid teamId);
    public IEnumerable<Team> GetAllTeams();
    public Team GetTeamById(Guid id);
    public void UpdateTeam(Team team);
    public bool IsTeamNameUnique(string Name);

}