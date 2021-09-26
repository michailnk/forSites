using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyCompany {
   public class Startup {
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
         }

      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services) {
         services.AddRazorPages();
         //поддержка контроллеров и представлений (MVC)
         services.AddControllersWithViews().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
         }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            }
         else {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
            }

         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseAuthorization();

         //система основаная на контроллерах и представлениях
         app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
          }
      }
   }
