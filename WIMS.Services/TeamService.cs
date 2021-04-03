using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models.TeamModels;
using WIMS.MVC.Data;

namespace WIMS.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTeam(TeamCreate model)
        {
            Team teamToAdd = new Team
            {
                TeamName = model.TeamName,
                //Manager = await _context.Users.SingleAsync(u => u.Id == model.ManagerId)
            };
            await _context.Teams.AddAsync(teamToAdd);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<IEnumerable<TeamListItem>> GetTeams()
        {
            return await _context.Teams.Select(t => new TeamListItem
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                ManagerName = t.Users.Single(u => u.IsManager == true).UserName,
                EmployeeNames = t.Users.Where(u => u.IsManager == false).Select(u => u.UserName).ToList()
            }).ToListAsync();            
        }

        public async Task<TeamDetail> GetTeamById(int id)
        {
            Team teamToGet = await _context.Teams.FindAsync(id);
            TeamDetail detail = new TeamDetail
            {
                TeamId = teamToGet.TeamId,
                TeamName = teamToGet.TeamName

                //ManagerName = teamToGet.Users.SingleOrDefault(u => u.IsManager == true).UserName
/*                            EmployeeNames = teamToGet.Users.Where(u => u.IsManager == false).
                    Select(u => u.UserName).ToList()*/
            };
            if (teamToGet.Users.Count > 0)
            {
                var manager = teamToGet.Users.SingleOrDefault(u => u.IsManager == true);
                detail.ManagerName = manager == null ? null : manager.UserName;
                foreach(var user in teamToGet.Users)
                {
                    if (!user.IsManager)
                    {
                        detail.EmployeeNames.Add(user.UserName);
                    }
                }
            }
            else
            {
                detail.ManagerName = null;
            }
            return detail;
        }

        //Edits Team Name
        public async Task<bool> EditTeam(TeamEdit model)
        {
            Team teamToEdit = await _context.Teams.FindAsync(model.TeamId);
            teamToEdit.TeamName = model.TeamName;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<bool> EditTeamMembers(int teamId, List<TeamMembersEdit> model)
        {
            foreach(var teamMember in model)
            {
                var user = await _context.Users.FindAsync(teamMember.UserId);
                if (teamMember.IsSelected)
                {
                    user.TeamId = teamId;
                }
                else if (!teamMember.IsSelected && user.TeamId == teamId)
                {
                    user.TeamId = null;
                }
                else
                {
                    continue;
                }
            }
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        //Delete Team
        public async Task<bool> DeleteTeam(int teamId)
        {
            Team teamToDelete = await _context.Teams.FindAsync(teamId);
            foreach(var user in _context.Users)
            {
                if (user.TeamId == teamId)
                {
                    user.TeamId = null;
                }
            }
            _context.Remove(teamToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
