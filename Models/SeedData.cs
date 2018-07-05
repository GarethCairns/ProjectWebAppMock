using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectWebAppMock.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppMock.Models
{
    public class SeedData
    {

    public static void EnsurePupulated(IApplicationBuilder app)
    {

      //ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
      ApplicationDbContext context = app.ApplicationServices
               .GetRequiredService<ApplicationDbContext>();

      if (!context.Workstreams.Any())
      {
        context.Workstreams.AddRange(
          new Workstream { WorkstreamName = "Planned Care" },
          new Workstream { WorkstreamName = "Urgent Care" },
          new Workstream { WorkstreamName = "Corporate and Infrastructure" }
          );
        context.SaveChanges();
      }
      if (!context.Projects.Any())
      {
        context.Projects.AddRange(
             new Project
             {
               ProjectId = new Guid(),
               ProjectTitle = "Reduce Medication Costs",
               ProjectDescription = "Go to GP surgeries and ask them to use cheaper generics",
               ProjectAssumptions = "Generics are available and cheaper than branded product",
               ProjectObjective = "to save 1/3 of the QIPP target in 4 months",
                // ProjectOwner = new ApplicationUser {  },
                workstream = new Workstream { WorkstreamName = "Prescribing and Meds Opt." },
               pIDType = new PIDType { PIDTypeName = "QIPP" },
               HowManyPeopleAffected = "10,000 note this is text",
               AgeRange = "18 +",
               IIAApproved = Approval.Unassessed,
               QIARequired = "Yes a QIA is required",
               PatientSafetyDesc = "People may not get effective treatment",
               PSC = 4,
               PSL = 2,
               PSR = 8,
               WhereLiving = "Throughout the land",
               QIAApproved = Approval.Yes
             },
             new Project
             {
               ProjectId = new Guid(),
               ProjectTitle = "Put flash new service in place",
               ProjectDescription = "Hire multidisciplinary advanced healthcare support workers to provide an ultradynamic and lean home-based service",
               ProjectAssumptions = "this will be very cheap and very effective, and these staff definately exist",
               ProjectObjective = "to save the world, and maybe a cheerleader",
                // ProjectOwner = new ApplicationUser {  },
                workstream = new Workstream { WorkstreamName = "Primary and Community Care" },
               pIDType = new PIDType { PIDTypeName = "QIPP" },
               HowManyPeopleAffected = "70 million",
               AgeRange = "18 +",
               IIAApproved = Approval.Yes,
               QIARequired = "Yes a QIA is required",
               PatientSafetyDesc = "This is entirely untested and may not even be real",
               PSC = 5,
               PSL = 5,
               PSR = 25,
               WhereLiving = "Throughout the land",
               QIAApproved = Approval.No
             }
           );
        context.SaveChanges();
      }

      

         
      }

    public static void EnsurePupulated(IServiceProvider services)
    {

      ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

      if (!context.Projects.Any())
      {
        context.Projects.AddRange(
             new Project
             {
               ProjectId = new Guid(),
               ProjectTitle = "Reduce Medication Costs",
               ProjectDescription = "Go to GP surgeries and ask them to use cheaper generics",
               ProjectAssumptions = "Generics are available and cheaper than branded product",
               ProjectObjective = "to save 1/3 of the QIPP target in 4 months",
               // ProjectOwner = new ApplicationUser {  },
               workstream = new Workstream { WorkstreamName = "MOT" },
               pIDType = new PIDType { PIDTypeName = "QIPP" },
               HowManyPeopleAffected = "10,000 note this is text",
               AgeRange = "18 +",
               IIAApproved = Approval.Unassessed,
               QIARequired = "Yes a QIA is required",
               PatientSafetyDesc = "People may not get effective treatment",
               PSC = 4,
               PSL = 2,
               PSR = 8,
               WhereLiving = "Throughout the land",
               QIAApproved = Approval.Yes
             },
             new Project
             {
               ProjectId = new Guid(),
               ProjectTitle = "Put flash new service in place",
               ProjectDescription = "Hire multidisciplinary advanced healthcare support workers to provide an ultradynamic and lean home-based service",
               ProjectAssumptions = "this will be very cheap and very effective, and these staff definately exist",
               ProjectObjective = "to save the world, and maybe a cheerleader",
               // ProjectOwner = new ApplicationUser {  },
               workstream = new Workstream { WorkstreamName = "Primary Care" },
               pIDType = new PIDType { PIDTypeName = "QIPP" },
               HowManyPeopleAffected = "70 million",
               AgeRange = "18 +",
               IIAApproved = Approval.Yes,
               QIARequired = "Yes a QIA is required",
               PatientSafetyDesc = "This is entirely untested and may not even be real",
               PSC = 5,
               PSL = 5,
               PSR = 25,
               WhereLiving = "Throughout the land",
               QIAApproved = Approval.No
             }
           );
        context.SaveChanges();
      }

      //if (!context.Workstreams.Any())
      //{
      //  context.Workstreams.AddRange(
      //    new Workstream { WorkstreamName = "Planned Care" },
      //    new Workstream { WorkstreamName = "Urgent Care" },
      //    new Workstream { WorkstreamName = "Primary and Community Care" },
      //    new Workstream { WorkstreamName = "Corporate and Infrastructure" }
      //    );
      //  context.SaveChanges();
      //}


    }

  }

    }

