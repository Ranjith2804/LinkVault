using LinkVault.Services;
using LinkVault.Middlewares;
using LinkVault.Endpoints;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<Linkservice>();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();

//app.UseAuthorization();
app.MapLinkendpoints();
app.MapHealthcheck();
app.Run();
