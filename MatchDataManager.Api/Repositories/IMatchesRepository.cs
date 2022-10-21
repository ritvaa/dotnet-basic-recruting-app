using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public interface IMatchesRepository
{
    public void DeleteMatch(Guid matchId);
    public IEnumerable<Match> GetAllMatches();
    public Match GetMatchById(Guid id);
    public void UpdateMatch(Match match);
    void Add(Match match);
}