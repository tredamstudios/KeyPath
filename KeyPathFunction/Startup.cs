using KeyPathFunctions.DAL;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(KeyPathFunctions.Startup))] 

namespace KeyPathFunctions
{
    /// <summary>
    /// Class created to allow for dependancy injection in Azure Functions and connections to the SQL Database
    /// </summary>
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();


            string connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<KeyPathContext>(options =>
                options.UseSqlServer(connectionString));

        }
    }
}

