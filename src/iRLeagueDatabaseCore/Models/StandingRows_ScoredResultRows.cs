using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class StandingRows_ScoredResultRows
    {
        public long LeagueId { get; set; }
        public long StandingRowRefId { get; set; }
        public long ScoredResultRowRefId { get; set; }

        public virtual StandingRowEntity StandingRow { get; set; }
        public virtual ScoredResultRowEntity ScoredResultRow { get; set; }
    }
}
