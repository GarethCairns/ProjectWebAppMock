using ProjectWebAppMock.Models;
using System.Linq;


namespace ProjectWebAppMock.Interfaces

{
   public interface IMilestoneRepository
    {

    IQueryable<Milestone> Milestones { get; }

    void SaveMilestone(Milestone milestone);

    Milestone DeleteMilestone(int milestoneID);

    }
}
