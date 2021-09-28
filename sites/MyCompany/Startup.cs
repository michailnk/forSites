using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Service;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace MyCompany {
   public class Startup {
      public IConfiguration Configuration { get; }
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
         }

      public void ConfigureServices(IServiceCollection services) {

         services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic }));

         //����������� ������ �� appsettings.json
         Configuration.Bind("Project", new Config());
         //����������� ����� ����������� ���������� ����� ��������� ������������
         //� �������� �������� (�� ������ http ������ ����� ����������� ����� �����������)
         services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
         services.AddTransient<IServiceItemsRepository,EFServiceItemsRepository>();
         services.AddTransient<DataManager>();

         //���������� �������� ��. � ������� ��������� ������ �����������
         //���������� �������� ��������� ������ Config
         services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

         //���������� ������� �����������  identity 
         services.AddIdentity<IdentityUser, IdentityRole>(opts => {
            opts.User.RequireUniqueEmail = true;
            opts.Password.RequiredLength = 6;
            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequireDigit = false;
         }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

         // authentication cookie
         services.ConfigureApplicationCookie(options => {
            options.Cookie.Name = "myCompanyAuth";
            options.Cookie.HttpOnly = true;
            options.LoginPath = "/account/login";
            options.AccessDeniedPath = "/account/accessdenied";
            options.SlidingExpiration = true;
         });

         //��������� ������������ � ������������� (MVC)
         services.AddControllersWithViews(x => {
            x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
         }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
         //services.AddControllersWithViews().SetCompatibilityVersion
         //(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

         // Admin area
         services.AddAuthorization(x => {
            x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
         });
       }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment())
         app.UseDeveloperExceptionPage();

      app.UseStaticFiles();

      app.UseRouting();

      app.UseCookiePolicy();
      app.UseAuthentication();
      app.UseAuthorization();

      //������� ��������� �� ������������ � ��������������
      app.UseEndpoints(endpoints => {
         endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
         endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      }
   }
}
