using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppMock.Models
{

  /// <summary>
  /// Entity Model for milestones, 1:many with projects
  /// </summary>
    public class Milestone
    {

    public int MilestoneId { get; set; }
    [Display(Name = "Name")]
    public string MilestoneName { get; set; }
    [Display(Name = "Deadline")]
    public DateTime Deadline { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    public Status Status { get; set; }
    }

  public enum Status
  {
  Incomplete = 0,
  Complete = 1,
  Late = 2
  }
}
