using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase, ILocationsController
{
    private readonly ILocationsRepository _locationsRepository;

    public LocationsController(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }
    
    [HttpPost]
    public IActionResult AddLocation(Location location)
    {
        _locationsRepository.AddLocation(location);
        return CreatedAtAction(nameof(GetById), new {id = location.Id}, location);
    }

    [HttpDelete]
    public IActionResult DeleteLocation(Guid locationId)
    {
        _locationsRepository.DeleteLocation(locationId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_locationsRepository.GetAllLocations());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var location = _locationsRepository.GetLocationById(id);
        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPut]
    public IActionResult UpdateLocation(Location location)
    {
        try
        {
            _locationsRepository.UpdateLocation(location);
            return Ok(location);
        }
        catch (ArgumentException )
        {
            return NotFound();
        }

    }
}