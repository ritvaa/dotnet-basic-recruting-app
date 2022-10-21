using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Models;

public class Match : Entity
{
    [Required]
    public virtual Team HomeTeam { get; set; }
    
    [Required]
    public virtual Team AwayTeam { get; set; }

    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public virtual Location Location { get; set; }
}