using Order.Infrastructure;
using Order.Infrastructure.Data.Extensions;
using Ordering.API;
using Ordering.Application;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseApiServives();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
