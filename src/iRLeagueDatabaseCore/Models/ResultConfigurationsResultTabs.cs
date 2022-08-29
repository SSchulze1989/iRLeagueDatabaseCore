using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class ResultConfigurationsResultTabs
    {
        public long LeagueId { get; set; }
        public long ResultTabRefId { get; set; }
        public long ResultConfigurationRefId { get; set; }

        public virtual ResultTabEntity ResultTabRef { get; set; }
        public virtual ResultConfigurationEntity ResultConfigurationRef { get; set; }
    }
}
