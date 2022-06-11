using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

public interface ITeamsController
{
    public IActionResult AddTeam(Team team);
    public IActionResult DeleteTeam(Guid teamId);
    public IActionResult Get();
    public IActionResult GetById(Guid id);
    public IActionResult UpdateTeam(Team team);
}