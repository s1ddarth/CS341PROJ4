using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace program
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
								
					  //
					  // NOTE: added this configuration to change "Pages" as default folder to "Views":
					  //
					  services.AddMvc().WithRazorPagesRoot("/Views");
					
					  //
					  // NOTE: added this to avoid forgery checks on post-backs:
					  //
					  services.AddMvc().AddRazorPagesOptions(options =>
             {
							 options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
						 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting(); 

    // Middleware that run after routing occurs. Usually the following appear here:
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors();
    // These middleware can take different actions based on the endpoint.

    // Executes the endpoint that was selected by routing.
    app.UseEndpoints(endpoints =>
    {
        // Mapping of endpoints goes here:
        endpoints.MapControllers();
        endpoints.MapRazorPages();
    });
            
        }
    }
}
