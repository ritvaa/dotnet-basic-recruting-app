using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

public interface ILocationsController
{
    public IActionResult AddLocation(Location location);
    public IActionResult DeleteLocation(Guid locationId);
    public IActionResult Get();
    public IActionResult GetById(Guid id);
    public IActionResult UpdateLocation(Location location);
}