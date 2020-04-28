using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Topaz.Data;
using Topaz.Common.Models;

[assembly: HostingStartup(typeof(Topaz.UI.Razor.Areas.Admin.AdminHostingStartup))]
namespace Topaz.UI.Razor.Areas.Admin
{
    public class AdminHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                // services.AddDbContext<AuthDbContext>(options =>
                //     options.UseSqlite(
                //         context.Configuration.GetConnectionString("AuthDbContext")));

                // services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //     .AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }
}