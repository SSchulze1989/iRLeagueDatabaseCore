using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ReviewPenaltyEntity
    {
        public long LeagueId { get; set; }
        public long ResultRowId { get; set; }
        public long ReviewId { get; set; }
        public int PenaltyPoints { get; set; }
        public long? ReviewVoteId { get; set; }

        public virtual ScoredResultRowEntity ResultRow { get; set; }
        public virtual IncidentReviewEntity Review { get; set; }
        public virtual AcceptedReviewVoteEntity ReviewVote { get; set; }
    }
}
