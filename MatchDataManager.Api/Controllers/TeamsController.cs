using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    [HttpPost]
    public IActionResult AddTeam(Team team)
    {
        TeamsRepository.AddTeam(team);
        return CreatedAtAction(nameof(GetById), new {id = team.Id}, team);
    }

    [HttpDelete]
    public IActionResult DeleteTeam(Guid teamId)
    {
        TeamsRepository.DeleteTeam(teamId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(TeamsRepository.GetAllTeams());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var location = TeamsRepository.GetTeamById(id);
        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPut]
    public IActionResult UpdateTeam(Team team)
    {
        TeamsRepository.UpdateTeam(team);
        return Ok(team);
    }
}