using System.Collections.Generic;

namespace TeamCityCompanion.Models
{
    public class Project
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ParentProjectId { get; set; }
    }
}
