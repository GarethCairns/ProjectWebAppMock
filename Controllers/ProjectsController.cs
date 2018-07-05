using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectWebAppMock.Data;
using ProjectWebAppMock.Interfaces;
using ProjectWebAppMock.Models;
using ProjectWebAppMock.Models.ProjectViewModels;

namespace ProjectWebAppMock.Controllers
{
  public class ProjectsController : Controller
  {
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private IMilestoneRepository _milestoneRepo;
    public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> usrMgr, IMilestoneRepository m_rep)
    {
      _context = context;
      _userManager = usrMgr;
      _milestoneRepo = m_rep;
    }

    private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    //TODO Refactor this controller to use Repositories for Projects and Milestones.


    // GET: Projects
    public async Task<IActionResult> Index()
    {
      return View(await _context.Projects.ToListAsync());
    }

    // GET: Projects/Details/5
    public IActionResult Details(Guid id)
    {
      if (id == null)
      {
        return NotFound();
      }
      else
      {
        Project _project = _context.Projects.Include(p => p.workstream).Include(p => p.ProjectOwner).Include(p => p.pIDType).Where(p => p.ProjectId == id).FirstOrDefault();

        //this appears to be lazyloading and causing an issue
        //Project _project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectId == id);
        if (_project == null)
        {
          return NotFound();
        }
        else
        {

          int wksNum = _context.Projects.Find(id).workstream.WorkstreamId;

          ProjectDetailsVM pvm = new ProjectDetailsVM
          {
            project = _project
          };
          if (_project.ProjectOwner != null)
          {
            pvm.ownerName = _project.ProjectOwner.UserName;
          }
          if (_project.workstream != null)
          {
            pvm.workstreamName = _project.workstream.WorkstreamName;
          }
          if(_project.pIDType != null)
          {
            pvm.typeName = _project.pIDType.PIDTypeName;
          }

          return View(pvm);
        }
      }
    }

    // GET: Projects/Create
    /// <summary>
    /// This is the naughty version, without repositories, where the list items etc are created and passed through here.
    /// </summary>
    /// <returns>Project Create View Model, and associated SelectListItems</returns>
    public IActionResult Create()
    {

      //create the view
      ProjectCreateVM pvcm = new ProjectCreateVM
      {

        //Get all the PID types,order them and add just their IDs and Names to a Select Item List in the View Model
        PidTypes = _context.PIDTypes.OrderBy(p => p.PIDTypeName).Select(n =>
        new SelectListItem
        {
          Value = n.PIDTypeId.ToString(),
          Text = n.PIDTypeName
        })
        .ToList(),

        //As above but for Workstreams
        Workstreams = _context.Workstreams.OrderBy(w => w.WorkstreamName).Select(n =>
        new SelectListItem
        {
          Value = n.WorkstreamId.ToString(),
          Text = n.WorkstreamName
        }).ToList()
      };

      //pass the pre-populated view model to the view
      return View(pvcm);
    }

    // POST: Projects/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    /// <summary>
    /// takes an instance of project and adds it to the Db, logging the current user as the project owner.
    /// </summary>
    /// <param name="project">ProjectCreateVM</param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectCreateVM pVM)
    {
      //Model state valid returns false but it still saves fine.
      if (ModelState.IsValid)
      {

        //Manually move over all the values form the VM to the actual Project - except calculated fields (Patient Safety Risk "PSR") and Infered fields - Project Owner [logged in user]
        //Need approach for catagorical and dynamic list types: Workstreams and Pid Type.
        Project project = new Project
        {
          AgeRange = pVM.AgeRange,
          HowManyPeopleAffected = pVM.HowManyPeopleAffected,
          IIAApproved = Approval.Unassessed,
          QIAApproved = Approval.Unassessed,
          QIARequired = pVM.QIARequired,
          workstream = await _context.Workstreams.FindAsync(pVM.WorkstreamId),
          PatientSafetyDesc = pVM.PatientSafetyDesc,
          pIDType = await _context.PIDTypes.FindAsync(pVM.PIDTypeId),
          ProjectAssumptions = pVM.ProjectAssumptions,
          ProjectDescription = pVM.ProjectDescription,
          ProjectObjective = pVM.ProjectObjective,
          ProjectTitle = pVM.ProjectTitle,
          PSC = pVM.PSC.GetValueOrDefault(),
          PSL = pVM.PSL.GetValueOrDefault(),
          WhereLiving = pVM.WhereLiving
        };

        //generate and add project ID
        project.ProjectId = Guid.NewGuid();

        //check, fetch and add user, as owner
        if (User.Identity.IsAuthenticated)
        {
          ApplicationUser user = await GetCurrentUserAsync();
          if (user != null)
          {
            project.ProjectOwner = user;
          }
        }


        //check and calculate impact scores - this should have a null check, but the ints aren't null to start...
        project.PSR = project.PSC * project.PSL;

        _context.Add(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        ViewBag.ErrorCount = ModelState.ErrorCount;
        return View(pVM);
      }
    }

    // GET: Projects/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectId == id);
      if (project == null)
      {
        return NotFound();
      }

      return View(project);
    }

    // POST: Projects/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Project project)
    {
      if (id != project.ProjectId)
      {
        return NotFound();
      }
      else
      {

        //update PSR
        project.PSR = project.PSC * project.PSL;
        try
        {
          _context.Update(project);
          await _context.SaveChangesAsync();

        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ProjectExists(project.ProjectId))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        ViewBag.Success = "Your change where saved";
        return View(project);
      }
    }

    // GET: Projects/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var project = await _context.Projects
          .SingleOrDefaultAsync(m => m.ProjectId == id);
      if (project == null)
      {
        return NotFound();
      }

      return View(project);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectId == id);
      _context.Projects.Remove(project);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ProjectExists(Guid id)
    {
      return _context.Projects.Any(e => e.ProjectId == id);
    }

    public IActionResult SeedDatabase()
    {
      SeedData.EnsurePupulated(HttpContext.RequestServices);
      return RedirectToAction(nameof(Index));
    }

  }
}
