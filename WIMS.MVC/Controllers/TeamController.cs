using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models.TeamModels;
using WIMS.Services;

namespace WIMS.MVC.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class TeamController : Controller
    {
        private readonly ITeamService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeamController(ITeamService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        
        //GET: Team/Index
        // Lists all teams
        public IActionResult Index()
        {
            IEnumerable<TeamListItem> teams = _service.GetTeams().Result;
            return View(teams);
        }

        //GET: Team/Create
        // Create a new Team
        public IActionResult Create()
        {
            return View();
        }

        //POST: Team/Create
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool wasAdded = await _service.CreateTeam(model);
            if (wasAdded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to add Team");
            return View(model);
        }

        //Get: Team/Details
        public async Task<IActionResult> Details(int id)
        {
            TeamDetail detail = await _service.GetTeamById(id);
            return View(detail);
        }

        //GET: Team/Edit
        public async Task<IActionResult> Edit(int id)
        {
            TeamDetail team = await _service.GetTeamById(id);
            TeamEdit model = new TeamEdit
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName
            };
            foreach (var user in _userManager.Users)
            {
                if (user.TeamId == id)
                {
                    model.Employees.Add(user.UserName);
                }
            }
            return View(model);
        }

        //POST: Team/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.TeamId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            bool wasUpdated = await _service.EditTeam(model);
            if (wasUpdated)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to update");
            return View(model);
        }

        //GET: Team/EditTeamMembers
        public async Task<IActionResult> EditTeamMembers(int teamId)
        {
            TeamDetail team = await _service.GetTeamById(teamId);
            ViewBag.teamName = team.TeamName;
            var model = new List<TeamMembersEdit>();
            foreach(var user in _userManager.Users)
            {               
                var teamMember = new TeamMembersEdit
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = user.TeamId == teamId,
                    IsManager = user.IsManager
                };
                model.Add(teamMember);
            }
            return View(model);
        }

        //POST: Team/EditTeamMembers
        [HttpPost]
        public async Task<IActionResult> EditTeamMembers(int teamId,List<TeamMembersEdit> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool wasUpdated = await _service.EditTeamMembers(teamId, model);
            if (!wasUpdated)
            {
                ModelState.AddModelError("", "Update failed");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        //GET: Team/Delete/{id}
        [HttpGet]
        
        public async Task<IActionResult> Delete(int id)
        {
            TeamDetail team = await _service.GetTeamById(id);
            return View(team);
        }

        //POST: Team/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (await _service.DeleteTeam(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
