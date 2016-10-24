using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamCityCompanion.Models;
using Xamarin.Forms;

namespace TeamCityCompanion
{
    public partial class ProjectDetails : ContentPage
    {
        public ObservableCollection<BuildType> BuildTypeCollection { get; set; }

        public Project Project
        {
            get { return _project; }
            set
            {
                _project = value;
                LoadProjectBuildTypes();
            }
        }

        private Project _project;
        private readonly ITeamCityService _teamCityService;

        public ProjectDetails()
        {
            _teamCityService = DependencyService.Get<ITeamCityService>();
            BuildTypeCollection = new ObservableCollection<BuildType>();

            InitializeComponent();

            // TODO: This is synchronous loading. Not good. Refactor to make it asynchronous
            LoadProjectBuildTypes();
        }

        private async void LoadProjectBuildTypes()
        {
            var buildTypes = await _teamCityService.GetBuildsForProjects(Project.Id);
            BuildTypeCollection.Clear();
            foreach (var buildType in buildTypes)
            {
                BuildTypeCollection.Add(buildType);
            }
        }
    }
}
