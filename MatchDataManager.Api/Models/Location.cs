using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Models;

public class Location : Entity
{
    [Required]
    [StringLength(255)]
    [Remote ("IsLocationNameUnique", "Location", ErrorMessage = "Location name already exists.")]
    public string Name { get; set; }

    [Required]
    [StringLength(55)]
    public string City { get; set; }
}