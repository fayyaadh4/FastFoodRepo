using FastFood;
using FastFood.Middleware;
using Microsoft.AspNetCore.Identity;
using FastFood.Startup;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// refactored program.cs file to make it look neater since there were alot of services registered in the dependency injection container
// passed in the builder configuratio for use in registration of sql server database
// passed in builder.host for the injection of IHostBuilder in DISetup
builder.Services.RegisterService(builder.Host, builder.Configuration);

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

// used so serilog will log Http calls for all requests
app.UseSerilogRequestLogging();

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

// injects the custom middleware into the API pipeline
// also done after authorization in pipeline for this one
app.UseMiddleware<GlobalErrorHandlingMiddleware>();


app.MapControllers();


app.Run();
