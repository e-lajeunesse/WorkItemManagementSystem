using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Models.TeamModels;

namespace WIMS.Services
{
    public interface ITeamService
    {
        Task<bool> CreateTeam(TeamCreate model);
        Task<IEnumerable<TeamListItem>> GetTeams();
        Task<TeamDetail> GetTeamById(int id);
        Task<bool> EditTeam(TeamEdit model);
        Task<bool> EditTeamMembers(int teamId,List<TeamMembersEdit> model);
        Task<bool> DeleteTeam(int teamId);
    }
}
