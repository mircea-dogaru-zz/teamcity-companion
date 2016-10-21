﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeamCityCompanion.Models;
using Xamarin.Forms;

namespace TeamCityCompanion
{
    public partial class Projects : ContentPage
    {
        public string MainText => "Projects";
        public ObservableCollection<Project> ProjectsCollection { get; set; }

        private readonly ITeamCityService _teamCityService;
        private List<Project> _allProjects;

        public Projects()
        {
            ProjectsCollection = new ObservableCollection<Project>();
            InitializeComponent();
            _teamCityService = DependencyService.Get<ITeamCityService>();

            ProjectsList.ItemsSource = ProjectsCollection;
            Filter.TextChanged += Filter_TextChanged;

            RefreshProjectList();
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProjects(e.NewTextValue);
        }

        public void FilterProjects(string text)
        {
            text = text.ToLower();
            AddProjectsToList(_allProjects.Where(p => p.Name.ToLower().Contains(text)));
        }

        private async void RefreshProjectList()
        {
            _allProjects = await _teamCityService.GetProjects();

            AddProjectsToList(_allProjects);
        }

        private void AddProjectsToList(IEnumerable<Project> projects)
        {
            ProjectsCollection.Clear();

            foreach (var project in projects)
            {
                ProjectsCollection.Add(project);
            }
        }
    }
}
