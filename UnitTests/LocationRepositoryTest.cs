using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MatchDataManager.Api;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using MySql.Data.MySqlClient;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest;

public class LocationRepositoryTest
{
    public IConfiguration Configuration { get; }
    private  MatchDataManagerDbContext _context;
    private  ILocationsRepository _locationsRepository;
    private readonly ITestOutputHelper output;

    public LocationRepositoryTest(ITestOutputHelper output)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MatchDataManagerDbContext>();
        optionsBuilder.UseMySQL(
            "server=localhost;userid=root;port=3306;database=MatchDataManager_db;");
        
        _context = new MatchDataManagerDbContext(optionsBuilder.Options);
        var databaseSeed = new DatabaseSeed(_context);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        databaseSeed.Seed();
        _locationsRepository = new LocationsRepository(_context);

        this.output = output;
    }

    // [Fact]
    // public void AddLocationReturnsNewLocation()
    // {
    //
    //     var location = new Location()
    //     {
    //         Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
    //         Name = "ExampleLocation",
    //         City = "ExampleCity"
    //     };
    //     
    //     _locationsRepository.AddLocation(location);
    //
    //     var addedLocation =
    //         _locationsRepository.GetLocationById(Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"));
    //     Assert.NotNull(addedLocation);
    // }
    [Fact]
    public void AddLocationReturnsNewLocation()
    {

        var location = new Location()
        {
            Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
            Name = "ExampleLocation",
            City = "ExampleCity"
        };
        
        _locationsRepository.AddLocation(location);

        // var addedLocation = _locationsRepository.GetLocationById(Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"));
        var addedLocation = _context.Locations.FirstOrDefault(x => x.Id == location.Id);
        Assert.NotNull(addedLocation);
    }
    private List<ValidationResult> ValidateModel<T>(T model)
    {
        var context = new ValidationContext(model, null, null);
        var result = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(model, context, result, true);

        return result;
    }

    [Fact]
    public void AddLocationNameNotUniqueThrowsException()
    {
        var location = new Location()
        {
            Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
            Name = "Location1",
            City = "ExampleCity"
        };
        Assert.Throws<ArgumentException>(()=> _locationsRepository.AddLocation(location));
    }

    [Fact]
    public void AddLocationNameIsNullThrowsException()
    {
        var location = new Location()
        {
            Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
            City = "ExampleCity"
        };
        Assert.Throws<DbUpdateException>(()=>_locationsRepository.AddLocation(location));
    }

    [Fact]
    public void AddLocationCityIsNullThrowsException()
    {
        var location = new Location()
        {
            Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
            Name = "ExampleName"
        };
        Assert.Throws<DbUpdateException>(()=>_locationsRepository.AddLocation(location));

    }
    [Fact]
    public void TooLongLocationNameThrowsException()
    {
        var location = new Location()
        {
            Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
            Name = "Dpqawccfxnmjwynejhxrzhihthijufdirfcyhcavqvzijripjifmmicavucefwidivtgahcgvjicnwcbbavrbcvfcagrjqvvhgbkgtjrzxfyextzqvumxaxycthqurpqfzuarwjeqhadfdkthjwztwztbhkrbzbviuqekcxjihigdyzqbttefyugvcyegarhkufqcdquhmwqjrrunzizdmbufzbzjyjfztkymdxyucadnnedibmtkmazqgvebpcv",
            City = "ExampleCity"
        };
        var results = ValidateModel(location);
        // output.WriteLine(results[0].ErrorMessage);
        
        Assert.True(results.Any(x => x.ErrorMessage == "The field Name must be a string with a maximum length of 255."));
    }

    [Fact]
    public void ToLongCityNameThrowsException()
    {
        var location = new Location()
        {
            Id = Guid.ParseExact("623f8a0f-e5fc-43e3-979e-9cd20d4d9220", "D"),
            Name = "ExampleName",
            City = "Uueqqmbcrcuzimxdiauzwbtrpwccqjhzvhjhdakbgqtymurqjqtyizku"
        };
        var results = ValidateModel(location);
        Assert.True(results.Any(x => x.ErrorMessage == "The field City must be a string with a maximum length of 55."));
    }

    [Fact]
    public void UpdateExistingLocation()
    {
        var existingLocation =
            _locationsRepository.GetLocationById(Guid.ParseExact("86e11657-e38f-4041-8081-5164155ff364", "D"));
        
        Assert.NotNull(existingLocation);

        existingLocation.City = "ExampleCity";
        existingLocation.Name = "ExampleName";
        
        _locationsRepository.UpdateLocation(existingLocation);

        var updatedObject =  _locationsRepository.GetLocationById(Guid.ParseExact("86e11657-e38f-4041-8081-5164155ff364", "D"));
        
        Assert.Equal("ExampleName", updatedObject.Name);
        Assert.Equal("ExampleCity", updatedObject.City);
    }

    [Fact]
    public void UpdateNotExistingThrowsException()
    {
        var location = new Location()
        {
            City = "ExampleCity1",
            Id = Guid.ParseExact("fc96cb4c-f992-4018-8f40-abd1a0ea86a4", "D"),
            Name = "ExampleName1"
        };
        
        Assert.Throws<ArgumentException>(() => _locationsRepository.UpdateLocation(location));
    }
    
    
}