using System;
using ADFS_Demo2.Areas.Identity.Data;
using ADFS_Demo2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ADFS_Demo2.Areas.Identity.IdentityHostingStartup))]
namespace ADFS_Demo2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ADFS_Demo2Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ADFS_Demo2ContextConnection")));

                services.AddDefaultIdentity<ADFS_Demo2User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ADFS_Demo2Context>();
            });
        }
    }
}