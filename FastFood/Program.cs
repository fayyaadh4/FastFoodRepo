using FastFood;
using FastFood.Data;
using FastFood.Filters;
using FastFood.Interfaces;
using FastFood.Middleware;
using FastFood.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using FastFood.Startup;

var builder = WebApplication.CreateBuilder(args);

// refactored program.cs file to make it look neater since there were alot of services registered in the dependency injection container
// passed in the builder configuratio for use in registration of sql server database
builder.Services.RegisterService(builder.Configuration);

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// refactored swagger configuration
app.ConfigureSwagger();

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

// injects the custom middleware into the API pipeline
// also done after authorization in pipeline for this one
app.UseMiddleware<GlobalErrorHandlingMiddleware>();


app.MapControllers();


app.Run();
