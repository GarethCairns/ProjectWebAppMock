using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppMock.Models
{
  /// <summary>
  /// This is a full-fat project class, with PID and IAs, it takes the logged-in user as the project owner, currently.
  /// </summary>
    public class Project
    {

    #region PID
    public Guid ProjectId { get; set; }
      [Display(Name = "Title")]
      public string ProjectTitle { get; set; }
      [Display(Name ="Description")]
      public string ProjectDescription { get; set; }
      [Display(Name ="Objective")]
      public string ProjectObjective { get; set; }
      [Display(Name ="Assumptions")]
      public string ProjectAssumptions { get; set; }
      [Display(Name ="Owner")]
      public ApplicationUser ProjectOwner { get; set; }
      [Display(Name ="PID Type")]
      public PIDType pIDType { get; set; }
      [Display(Name ="Workstream")]
      public Workstream workstream { get; set; }
    #endregion


    #region IIA
    [Display(Name ="How Many people will be affected?")]
     public string HowManyPeopleAffected { get; set; }
      [Display(Name ="What is their age rang?")]
    public string AgeRange { get; set; }
    [Display(Name ="Where do they live?")]
     public string WhereLiving { get; set; }
    public Approval IIAApproved { get; set; }
    #endregion

    #region QIA
    [Display(Name ="Is a QIA required?")]
    public string QIARequired { get; set; }
    [Display(Name ="Patient Safety Description")]
    public string PatientSafetyDesc { get; set; }
    [Display(Name ="Patient Safety Consequence")]
    public int PSC { get; set; }
    [Display( Name = "Patient Safety Likelihood")]
    public int PSL { get; set; }
    [Display(Name ="Patient Safety Risk")]
    public int PSR { get; set; }
    public Approval QIAApproved { get; set; }
    #endregion
  }

  public class PIDType
  {
    public int PIDTypeId { get; set; }
    public string PIDTypeName { get; set; }
  }

  public class Workstream
  {
    public int WorkstreamId { get; set; }
    public string WorkstreamName { get; set; }
  }

  public enum Approval
  {
    Unassessed =0,
    Yes = 1,
    No = 2
  }

  public enum Yes_No
  {
    blank = 0,
    Yes = 1,
    No = 2
  }

 
}
