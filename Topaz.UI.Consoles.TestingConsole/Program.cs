using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Consoles.TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();
            // Kick off our actual code
            serviceProvider.GetService<ConsoleApp>().Run();
            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<TopazDbContext, TopazDbContext>();
            services.AddDbContext<TopazDbContext>(options => options.UseSqlite("Data Source=TopazDb.db"));
            services.AddTransient<ConsoleApp>();
            return services;
        }
    }

    public class ConsoleApp
    {
        private TopazDbContext _db;
        public ConsoleApp(TopazDbContext db)
        {
            _db = db;
        }

        // Application starting point
        public void Run()
        {
            _db.SaveChanges();
            Console.WriteLine("done");
        }
    }
}
