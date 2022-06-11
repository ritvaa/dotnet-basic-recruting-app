using System.Configuration;
using MatchDataManager.Api;
using MatchDataManager.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<MatchDataManagerDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MatchDataManagerDbContext")));
builder.Services.AddTransient<DatabaseSeed>();
builder.Services.AddControllers();
builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();
// builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MatchDataManagerDbContext>();
    var databaseSeed = serviceScope.ServiceProvider.GetRequiredService<DatabaseSeed>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    databaseSeed.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();