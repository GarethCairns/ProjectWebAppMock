using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppMock.Models.ProjectViewModels
{
  /// <summary>
  /// ViewModel for Create() - doesn't store calculated or infered content, i.e. risk scores and project owners.
  /// </summary>
    public class ProjectCreateVM
    {

    #region PID
    //public Guid ProjectId { get; set; } //added in the POST
    [Display(Name = "Title")]
    public string ProjectTitle { get; set; }
    [Display(Name = "Description")]
    public string ProjectDescription { get; set; }
    [Display(Name = "Objective")]
    public string ProjectObjective { get; set; }
    [Display(Name = "Assumptions")]
    public string ProjectAssumptions { get; set; }

    /// <summary>
    /// Taken from current logged in user
    /// </summary>
    //[Display(Name = "Owner")]
    //public ApplicationUser ProjectOwner { get; set; }
    [Display(Name = "PID Type")]
    public int? PIDTypeId { get; set; }  //Needs to come from a drop-down sigh
    public List<SelectListItem> PidTypes { get; set; }
    [Display(Name = "Workstream")]
    public int? WorkstreamId { get; set; } // ditto
    public List<SelectListItem> Workstreams { get; set; }
    #endregion


    #region IIA
    [Display(Name = "How Many people will be affected?")]
    public string HowManyPeopleAffected { get; set; }
    [Display(Name = "What is their age rang?")]
    public string AgeRange { get; set; }
    [Display(Name = "Where do they live?")]
    public string WhereLiving { get; set; }
    public Approval IIAApproved { get; set; }
    #endregion

    #region QIA
    [Display(Name = "Is a QIA required?")]
    public string QIARequired { get; set; }
    [Display(Name = "Patient Safety Description")]
    public string PatientSafetyDesc { get; set; }
    [Display(Name = "Patient Safety Consequence")]
    public int? PSC { get; set; }
    [Display(Name = "Patient Safety Likelihood")]
    public int? PSL { get; set; }
    /// <summary>
    /// Calculated field.
    /// </summary>
    //[Display(Name = "Patient Safety Risk")]
    //public int PSR { get; set; }
    public Approval QIAApproved { get; set; }
    #endregion



  }
}
