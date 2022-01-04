using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredTeamResultRowsGroup
    {
        public long ScoredTeamResultRowRefId { get; set; }
        public long ScoredResultRowRefId { get; set; }

        public virtual ScoredResultRowEntity ScoredResultRowRef { get; set; }
        public virtual ScoredTeamResultRowEntity ScoredTeamResultRowRef { get; set; }
    }
}
