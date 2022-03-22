using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class AddPenaltyEntity
    {
        public long LeagueId { get; set; }
        public long ScoredResultRowId { get; set; }
        public int PenaltyPoints { get; set; }

        public virtual ScoredResultRowEntity ScoredResultRow { get; set; }
    }
}
