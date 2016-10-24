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
        private const string TeamCityProject = "/app/rest/projects/id:{0}";
        private const string TeamCityBuildStatus = "/app/rest/builds/?locator=buildType:{0}&count=1";

        private readonly ICommunicator _communicator;
        public TeamCityService()
        {
            _communicator = DependencyService.Get<ICommunicator>();
        }

        public async Task<List<Project>> GetProjects()
        {
            var endpoint = GetEndpoint();

            var result = await _communicator.Get(endpoint, TeamCityProjects);
            return BuildProjectStructure(result);
        }

        public async Task<List<BuildType>> GetBuildsForProjects(string projectId)
        {
            var endpoint = GetEndpoint();

            var result = await _communicator.Get(endpoint, string.Format(TeamCityProject, projectId));
            return await BuildBuildTypeStructure(result);
        }

        public async Task<Build> GetLatestBuild(string buildTypeId)
        {
            var endpoint = GetEndpoint();

            var result = await _communicator.Get(endpoint, string.Format(TeamCityBuildStatus, buildTypeId));
            return BuildBuildStructure(result);
        }

        private static string GetEndpoint()
        {
           return $"{AccountManager.Current.Server}{(AccountManager.Current.IsGuest ? "/guestAuth" : string.Empty)}";
        }

        private static Build BuildBuildStructure(XmlReader data)
        {
            if (data == null)
                return null;

            var build = new Build();

            while (data.Read())
            {
                if (data.Name != "build")
                    continue;

                build.Id = data.GetAttribute("id");
                build.Number = data.GetAttribute("number");
                build.Status = data.GetAttribute("status");
                build.State = data.GetAttribute("state");

                break;
            }

            data.Dispose();

            return build;
        }
        private async Task<List<BuildType>> BuildBuildTypeStructure(XmlReader data)
        {
            var buildTypes = new List<BuildType>();

            if (data == null)
                return buildTypes;

            while (data.Read())
            {
                if (data.Name != "buildType")
                    continue;

                var buildType = new BuildType()
                {
                    Id = data.GetAttribute("id"),
                    Name = data.GetAttribute("name"),
                    Description = data.GetAttribute("description"),
                };

                buildType.LatestBuild = await GetLatestBuild(buildType.Id);

                buildTypes.Add(buildType);
            }

            data.Dispose();

            return buildTypes;
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

            data.Dispose();

            return projects;
        }
    }
}
