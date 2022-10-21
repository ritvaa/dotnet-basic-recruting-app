using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMatchesRepository _MatchesRepository;

    public MatchController(IMatchesRepository MatchesRepository)
    {
        _MatchesRepository = MatchesRepository;
    }
    
    [HttpPost]
    public IActionResult AddMatch(Match Match)
    {
        _MatchesRepository.Add(Match);
        return CreatedAtAction(nameof(GetById), new {id = Match.Id}, Match);
    }

    [HttpDelete]
    public IActionResult DeleteMatch(Guid MatchId)
    {
        _MatchesRepository.DeleteMatch(MatchId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_MatchesRepository.GetAllMatches());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var Match = _MatchesRepository.GetMatchById(id);
        if (Match is null)
        {
            return NotFound();
        }

        return Ok(Match);
    }

    [HttpPut]
    public IActionResult UpdateMatch(Match Match)
    {
        try
        {
            _MatchesRepository.UpdateMatch(Match);
            return Ok(Match);
        }
        catch (ArgumentException )
        {
            return NotFound();
        }

    }
}