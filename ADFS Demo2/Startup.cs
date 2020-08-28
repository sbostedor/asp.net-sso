using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ADFS_Demo2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace ADFS_Demo2
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform

                options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            });

            services.AddAuthentication()
            
            .AddWsFederation(options =>
            {
                    
                    // MetadataAddress represents the Active Directory instance used to authenticate users.
                    // Make sure you edit the address to be your ADFS metadata and not mine :-)
                    options.MetadataAddress = "https://<yourdomain>/FederationMetadata/2007-06/FederationMetadata.xml";
                    //options.MetadataAddress = "https://login.microsoftonline.com/.....";

                  // Wtrealm is the app's identifier in the Active Directory instance.
                  // For ADFS, use the relying party's identifier, its WS-Federation Passive protocol URL:
                  //options.Wtrealm = "https://localhost:44329/";
                  options.Wtrealm = "https://identity1.wingtip.toys/";

                // For AAD, use the Application ID URI from the app registration's Overview blade:
                    //options.Wtrealm = "api://4f613ce4-4e92-4e1e-9abe-3fb855ea8f54";
            }


            );

            

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
/*                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();*/
            }

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
