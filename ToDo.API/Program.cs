using Microsoft.EntityFrameworkCore;
using ToDo.API.Application.Interfaces;
using ToDo.API.Application.Services;
using ToDo.API.Domain.Core.Repositories;
using ToDo.API.Infrastructure.Data;
using ToDo.API.Infrastructure.Repositories;
using ToDo.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMissionService, MissionService>();

var app = builder.Build();

app.MapGrpcService<MissionItemServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
