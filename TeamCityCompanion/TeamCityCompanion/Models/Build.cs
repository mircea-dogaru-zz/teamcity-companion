using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamCityCompanion.Models
{
    public class Build
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
    }
}
