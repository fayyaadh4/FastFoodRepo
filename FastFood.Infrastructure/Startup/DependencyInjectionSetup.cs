
using FastFood.Application.Services;
using FastFood.Core.Services;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Infrastructure.Implementation;
using FastFood.Repo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace FastFood.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, 
            IHostBuilder hosts,  
            IConfiguration configuration)
        {
            // Add services to the container.

            services.AddControllers();

            services.AddTransient<Seed>();

            // registers automapper 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //registers the relationship between the interface and the repository implementing it
            // NOT USING AS IM USING UNITOFWORK
            /*services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeRoleRepository, RoleRepository>();*/

            // register services in Dependency injection container
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRoleService, RoleService>();

            // register unit of work 
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // used to register the Identity Authorization into the services collection
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            // register Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration).CreateLogger();
            /*
             Without writing to appsettings.json file
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                // logs/FastFood-.txt will write to file folder and dash will add date
                .WriteTo.File("logs/FastFood-.txt", rollingInterval:RollingInterval.Day)
                .CreateLogger();
            */
            // used so serilog will log all http requests
            hosts.UseSerilog();
            // registers the db context  with the sql server database inside the dependency injection container
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthorization();

            // registers the datacontext with the IdentityUser
            services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            // Injecting the MediatR to our DI container by telling it  to scan all assemblies in Program.cs
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjectionSetup).Assembly));

            services.AddApplicationServices();
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjectionSetup).Assembly));

            return services;
        }
    }
}
