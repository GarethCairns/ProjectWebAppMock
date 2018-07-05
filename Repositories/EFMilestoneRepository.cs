using ProjectWebAppMock.Data;
using ProjectWebAppMock.Interfaces;
using ProjectWebAppMock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppMock.Repositories
{

  /// <summary>
  /// CRUD Code Here  - default pattern of persistance layer - interacted with through the associated interface, conducts the direct interaction with DbContext, loosely coupling with Controllers.
  /// </summary>
  public class EFMilestoneRepository : IMilestoneRepository
    {

    private ApplicationDbContext context;

    public EFMilestoneRepository(ApplicationDbContext ctx)
    {
      context = ctx;
    }

    public IQueryable<Milestone> Milestones => context.Milestones;

    public void SaveMilestone(Milestone milestone)
    {
      if(milestone.MilestoneId == 0)
      {
        context.Milestones.Add(milestone);
      }
      else
      {
        Milestone upDatedMilestone = context.Milestones.FirstOrDefault(m => m.MilestoneId == milestone.MilestoneId);
        if(upDatedMilestone != null)
        {
          upDatedMilestone.Deadline = milestone.Deadline;
          upDatedMilestone.Description = milestone.Description;
          upDatedMilestone.MilestoneName = milestone.MilestoneName;
          upDatedMilestone.Status = milestone.Status;
        }
        context.SaveChanges();
      }
    }

    public Milestone DeleteMilestone(int milestoneID)
    {
      Milestone deletedMilestone = context.Milestones.FirstOrDefault(m => m.MilestoneId == milestoneID);
      if(deletedMilestone != null)
      {
        context.Milestones.Remove(deletedMilestone);
        context.SaveChanges();
      }
      return deletedMilestone;
    }


    }
}
