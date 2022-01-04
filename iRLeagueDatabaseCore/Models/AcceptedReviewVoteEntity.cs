using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class AcceptedReviewVoteEntity
    {
        public AcceptedReviewVoteEntity()
        {
            ReviewPenaltyEntities = new HashSet<ReviewPenaltyEntity>();
        }

        public long ReviewVoteId { get; set; }
        public long LeagueId { get; set; }
        public long ReviewId { get; set; }
        public long? MemberAtFaultId { get; set; }
        public int Vote { get; set; }
        public long? CustomVoteCatId { get; set; }
        public string Description { get; set; }

        public virtual VoteCategoryEntity CustomVoteCat { get; set; }
        public virtual MemberEntity MemberAtFault { get; set; }
        public virtual IncidentReviewEntity Review { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenaltyEntities { get; set; }
    }
}
