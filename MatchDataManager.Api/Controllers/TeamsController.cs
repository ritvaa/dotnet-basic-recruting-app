using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamRepository _teamRepository;

    public TeamsController(ITeamRepository teamsRepository)
    {
        _teamRepository = teamsRepository;
    }
    
    [HttpPost]
    public IActionResult AddTeam(Team team)
    {
        _teamRepository.AddTeam(team);
        return CreatedAtAction(nameof(GetById), new {id = team.Id}, team);
    }

    [HttpDelete]
    public IActionResult DeleteTeam(Guid teamId)
    {
        _teamRepository.DeleteTeam(teamId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_teamRepository.GetAllTeams());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var location = _teamRepository.GetTeamById(id);
        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPut]
    public IActionResult UpdateTeam(Team team)
    {
        _teamRepository.UpdateTeam(team);
        return Ok(team);
    }
}