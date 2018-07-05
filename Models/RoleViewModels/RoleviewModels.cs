using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProjectWebAppMock.Models.RoleViewModels
{
    public class RoleviewModels
    {

    public class RoleEditModel
    {
      public IdentityRole Role { get; set; }
      public IEnumerable<ApplicationUser> Members { get; set; }
      public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
      [Required]
      public string RoleName { get; set; }
      public string RoleId { get; set; }
      public string[] IdsToAdd { get; set; }
      public string[] IdsToDelete { get; set; }
    }

  }
}
