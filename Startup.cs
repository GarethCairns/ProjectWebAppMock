using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectWebAppMock.Data;
using ProjectWebAppMock.Models;
using ProjectWebAppMock.Services;
using ProjectWebAppMock.Repositories;
using ProjectWebAppMock.Interfaces;

namespace ProjectWebAppMock
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
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentity<ApplicationUser, IdentityRole>(
        //opts => { opts.User.RequireUniqueEmail = true;  //Example configuration setting - to add depending on CCG policy see tail end of Ch 28 - fig 10+    }
        )
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      // Add application services.
      services.AddTransient<IEmailSender, EmailSender>();
      services.AddTransient<IMilestoneRepository, EFMilestoneRepository>();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }
      app.UseBrowserLink();
      app.UseStaticFiles();
      app.UseAuthentication();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });

      ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
     SeedData.EnsurePupulated(app);
    }
  }
}
