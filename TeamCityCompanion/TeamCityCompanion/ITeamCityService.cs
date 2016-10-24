using System.Collections.Generic;
using System.Threading.Tasks;
using TeamCityCompanion.Models;

namespace TeamCityCompanion
{
    public interface ITeamCityService
    {
        Task<List<Project>> GetProjects();
        Task<List<BuildType>> GetBuildsForProjects(string projectId);
        Task<Build> GetLatestBuild(string buildTypeId);
    }
}
