using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class EventResultConfigs
    {
        public long LeagueId { get; set; }
        public long EventRefId { get; set; }
        public long ResultConfigRefId { get; set; }

        public virtual EventEntity Event { get; set; }
        public virtual ResultConfigurationEntity ResultConfig { get; set; }
    }
}
