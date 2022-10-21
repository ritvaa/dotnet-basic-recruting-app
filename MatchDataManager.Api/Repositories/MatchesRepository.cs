using MatchDataManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchDataManager.Api.Repositories;

public class MatchesRepository : IMatchesRepository
{
    private readonly MatchDataManagerDbContext _context;

    public MatchesRepository(MatchDataManagerDbContext context)
    {
        _context = context;
    }

    public void DeleteMatch(Guid matchId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Match> GetAllMatches()
    {
        return _context.Matches.Include(x => x.Location).Include(x => x.AwayTeam).Include(x => x.HomeTeam).ToList();
    }

    public Match GetMatchById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void UpdateMatch(Match match)
    {
        throw new NotImplementedException();
    }

    public void Add(Match match)
    {
    }
}