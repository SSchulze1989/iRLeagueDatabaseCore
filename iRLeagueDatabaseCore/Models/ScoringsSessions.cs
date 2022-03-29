using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoringsSessions
    {
        public long LeagueId { get; set; }
        public long SessionRefId { get; set; }
        public long ScoringRefId { get; set; }

        public virtual SessionEntity SessionRef { get; set; }
        public virtual ScoringEntity ScoringRef { get; set; }
    }
}
