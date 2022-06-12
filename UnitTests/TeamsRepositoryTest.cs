using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using MatchDataManager.Api;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest;

public class TeamsRepositoryTest
{
    public IConfiguration Configuration { get; }
    private  MatchDataManagerDbContext _context;
    private  ITeamRepository _teamRepository;
    private readonly ITestOutputHelper output;

    public TeamsRepositoryTest(ITestOutputHelper output)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MatchDataManagerDbContext>();
        optionsBuilder.UseMySQL(
            "server=localhost;userid=root;port=3306;database=MatchDataManager_db;");
        
        _context = new MatchDataManagerDbContext(optionsBuilder.Options);
        var databaseSeed = new DatabaseSeed(_context);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        databaseSeed.Seed();
        _teamRepository = new TeamsRepository(_context);

        this.output = output;
    }
    private List<ValidationResult> ValidateModel<T>(T model)
    {
        var context = new ValidationContext(model, null, null);
        var result = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(model, context, result, true);

        return result;
    }

    [Fact]
    public void AddTeamReturnsNewTeam()
    {
        var team = new Team()
        {
            CoachName = "ExampleCoach",
            Id = Guid.ParseExact("01f8cceb-0efe-4f0b-943e-0b159af2c914", "D"),
            Name = "ExampleTeam"
        };
        _teamRepository.AddTeam(team);
        
        var addedTeam = _context.Teams.FirstOrDefault(t => t.Id == team.Id);
        Assert.NotNull(addedTeam);
    }

    [Fact]
    public void AddTeamNameIsNullThrowsExcetion()
    {
        var team = new Team()
        {
            CoachName = "ExampleCoach",
            Id = Guid.ParseExact("01f8cceb-0efe-4f0b-943e-0b159af2c914", "D"),
            Name = null
        };
        Assert.Throws<DbUpdateException>(() => _teamRepository.AddTeam(team));
    }

    [Fact]
    public void TooLongTeamNameThrowsExcetion()
    {
        var team = new Team()
        {
            CoachName = "ExampleCoach",
            Id = Guid.ParseExact("01f8cceb-0efe-4f0b-943e-0b159af2c914", "D"),
            Name =
                "Dpqawccfxnmjwynejhxrzhihthijufdirfcyhcavqvzijripjifmmicavucefwidivtgahcgvjicnwcbbavrbcvfcagrjqvvhgbkgtjrzxfyextzqvumxaxycthqurpqfzuarwjeqhadfdkthjwztwztbhkrbzbviuqekcxjihigdyzqbttefyugvcyegarhkufqcdquhmwqjrrunzizdmbufzbzjyjfztkymdxyucadnnedibmtkmazqgvebpcv",
        };
        var result = ValidateModel(team);
        // output.WriteLine(result[0].ErrorMessage);
        Assert.True(result.Any(x=>x.ErrorMessage=="The field Name must be a string with a maximum length of 255."));
    }

    [Fact]
    public void TooLongCoachNameThrowsExcetion()
    {
        var team = new Team()
        {
            CoachName = "Uueqqmbcrcuzimxdiauzwbtrpwccqjhzvhjhdakbgqtymurqjqtyizku",
            Id = Guid.ParseExact("01f8cceb-0efe-4f0b-943e-0b159af2c914", "D"),
            Name = "ExampleTeam"
        };
        var result = ValidateModel(team);
        output.WriteLine(result[0].ErrorMessage);
        Assert.True(result.Any(x=>x.ErrorMessage=="The field CoachName must be a string with a maximum length of 55."));
    }

    [Fact]
    public void UpdateExistingTeam()
    {
        var existingTeam =
            _teamRepository.GetTeamById(Guid.ParseExact("5af1ec5a-895b-47ae-9c0c-ec56e1293865", "D"));
        
        Assert.NotNull(existingTeam);

        existingTeam.CoachName = "ExampleCoachName";
        existingTeam.Name = "ExampleName";
        
        _teamRepository.UpdateTeam(existingTeam);

        var updatedObject =  _teamRepository.GetTeamById(Guid.ParseExact("5af1ec5a-895b-47ae-9c0c-ec56e1293865", "D"));
        
        Assert.Equal("ExampleName", updatedObject.Name);
        Assert.Equal("ExampleCoachName", updatedObject.CoachName);
    }
    
    [Fact]
    public void UpdateNonExistingTeamThrowsException()
    {
        var team = new Team()
        {
            CoachName = "ExampleCoach",
            Id = Guid.ParseExact("01f8cceb-0efe-4f0b-943e-0b159af2c914", "D"),
            Name = "ExampleTeam"
        };
        Assert.Throws<ArgumentException>(() => _teamRepository.UpdateTeam(team));
    }
}