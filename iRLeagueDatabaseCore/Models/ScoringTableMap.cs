using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoringTableMap
    {
        public long LeagueId { get; set; }
        public long ScoringTableRefId { get; set; }
        public long ScoringRefId { get; set; }

        public virtual ScoringEntity ScoringRef { get; set; }
        public virtual ScoringTableEntity ScoringTableRef { get; set; }
    }
}
