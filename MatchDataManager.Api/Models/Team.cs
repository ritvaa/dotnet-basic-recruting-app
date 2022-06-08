using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Models;

public class Team : Entity
{
    [Required]
    [StringLength(255)]
    [Remote ("IsTeamNameUnique", "Teams", ErrorMessage = "Team name already exists.")]
    public string Name { get; set; }

    [StringLength(55)]
    public string CoachName { get; set; }
}