using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    [HttpPost]
    public IActionResult AddLocation(Location location)
    {
        LocationsRepository.AddLocation(location);
        return CreatedAtAction(nameof(GetById), new {id = location.Id}, location);
    }

    [HttpDelete]
    public IActionResult DeleteLocation(Guid locationId)
    {
        LocationsRepository.DeleteLocation(locationId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(LocationsRepository.GetAllLocations());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var location = LocationsRepository.GetLocationById(id);
        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPut]
    public IActionResult UpdateLocation(Location location)
    {
        LocationsRepository.UpdateLocation(location);
        return Ok(location);
    }
}