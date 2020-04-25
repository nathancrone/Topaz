using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Topaz.Common.Models;
using Topaz.Data;
using Topaz.UI.Consoles.UserConsole.Interfaces;
using Topaz.UI.Consoles.UserConsole.Services;


namespace Topaz.UI.Consoles.UserConsole
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
               {
                   services.AddOptions();
                   services.AddTransient<AuthDbContext, AuthDbContext>();
                   services.AddDbContext<AuthDbContext>(options => options.UseSqlite("Data Source=AuthDb.db"));
                   services.AddIdentity<AppUser, AppRole>()
                       .AddEntityFrameworkStores<AuthDbContext>();
                   services.AddScoped<IUserCreationService, UserCreationService>();
                   services.AddSingleton<IHostedService, ConsoleService>();
               })
               .ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.AddConsole();
               });

            await builder.RunConsoleAsync();
        }
    }
}
