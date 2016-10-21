using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TeamCityCompanion;
using TeamCityCompanion.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(TeamCityService))]

namespace TeamCityCompanion
{
    public class TeamCityService : ITeamCityService
    {
        private const string TeamCityProjects = "/app/rest/projects";

        private readonly ICommunicator _communicator;
        public TeamCityService()
        {
            _communicator = DependencyService.Get<ICommunicator>();
        }

        public async Task<List<Project>> GetProjects()
        {
            var endpoint = $"{AccountManager.Current.Server}{(AccountManager.Current.IsGuest ? "/guestAuth" : string.Empty)}";

            var result = await _communicator.Get(endpoint, TeamCityProjects);
            return BuildProjectStructure(result);
        }

        private static List<Project> BuildProjectStructure(XmlReader data)
        {
            var projects = new List<Project>();

            if (data == null)
                return projects;

            while (data.Read())
            {
                if (data.Name != "project")
                    continue;

                var project = new Project()
                {
                    Id = data.GetAttribute("id"),
                    Name = data.GetAttribute("name"),
                    Description = data.GetAttribute("description"),
                    ParentProjectId = data.GetAttribute("parentProjectId")
                };

                projects.Add(project);
            }

            return projects;
        }
    }
}
