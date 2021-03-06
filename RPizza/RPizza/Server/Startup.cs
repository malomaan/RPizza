using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using RPizza.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Http;

namespace RPizza.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RPizzaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RPizzaContext"));
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
            })
             .AddCookie()
             .AddTwitter(twitterOptions =>
             {
                 twitterOptions.ConsumerKey =
     "U9DbAaVcDPYO3RVFlDo4w";
                 twitterOptions.ConsumerSecret =
     "l6HWZa8F5MJmbBkGSzL6gMjgZMererT5KROxAzws9o";
                 twitterOptions.Events.OnRemoteFailure = (context) =>
                 {
                     context.HandleResponse();
                     return context.Response.WriteAsync(
          "<script>window.close();</script>");
                 };
             });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
