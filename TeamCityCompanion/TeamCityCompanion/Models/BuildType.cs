using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamCityCompanion.Models
{
    public class BuildType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Build LatestBuild { get; set; }
    }
}
