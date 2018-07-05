using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectWebAppMock.Models;

namespace ProjectWebAppMock.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    public static async Task CreateAdminAccount(IServiceProvider serviceProvider,
            IConfiguration configuration)
    {

      UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      string username = configuration["Data:AdminUser:Name"];
      string email = configuration["Data:AdminUser:Email"];
      string password = configuration["Data:AdminUser:Password"];
      string role = configuration["Data:AdminUser:Role"];

      if (await userManager.FindByNameAsync(username) == null)
      {
        if (await roleManager.FindByNameAsync(role) == null)
        {
          await roleManager.CreateAsync(new IdentityRole(role));
        }

        ApplicationUser user = new ApplicationUser
        {
          UserName = username,
          Email = email
        };

        IdentityResult result = await userManager
            .CreateAsync(user, password);
        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(user, role);
        }
      }
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<PIDType> PIDTypes { get; set; }
    public DbSet<Workstream> Workstreams { get; set; }
    public DbSet<Milestone> Milestones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //Maybe you can, maybe we should, but we didn't
        }
    }
}
