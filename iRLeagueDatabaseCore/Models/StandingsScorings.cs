using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class StandingsScorings
    {
        public long LeagueId { get; set; }
        public long StandingRefId { get; set; }
        public long ScoringRefId { get; set; }

        public virtual ScoringEntity ScoringRef { get; set; }
        public virtual StandingEntity StandingRef { get; set; }
    }
}
