using TaskSync.Api.Extensions;
using TaskSync.Application;
using TaskSync.Infrastructure;
using TaskSync.Persistence;
using TaskSync.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseApplicationMiddleware();

app.UseSwagger();

app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    db.Database.EnsureCreated();

    await ApplicationDbContextSeed.SeedAsync(db);
}

app.Run();