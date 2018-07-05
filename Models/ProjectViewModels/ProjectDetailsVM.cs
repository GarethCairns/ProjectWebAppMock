using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppMock.Models.ProjectViewModels
{
  public class ProjectDetailsVM
  {

    public Project project { get; set; }
    [Display(Name = "Project Owner")]
    public string ownerName { get; set; }
    [Display(Name = "Workstream")]
    public string workstreamName { get; set; }
    [Display(Name = "PID Type")]
    public string typeName { get; set; }
  }
}
