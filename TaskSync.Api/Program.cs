// ============================================
// Api/Program.cs (replace)
// ============================================

using TaskSync.Api.Extensions;
using TaskSync.Application;
using TaskSync.Infrastructure;
using TaskSync.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApplicationMiddleware();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();