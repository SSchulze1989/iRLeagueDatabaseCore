using iRLeagueApiCore.Communication.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class AcceptedReviewVoteEntity
    {
        public AcceptedReviewVoteEntity()
        {
            ReviewPenaltys = new HashSet<ReviewPenaltyEntity>();
        }

        public long ReviewVoteId { get; set; }
        public long LeagueId { get; set; }
        public long ReviewId { get; set; }
        public long? MemberAtFaultId { get; set; }
        public long? VoteCategoryId { get; set; }
        public string Description { get; set; }

        public virtual VoteCategoryEntity VoteCategory { get; set; }
        public virtual MemberEntity MemberAtFault { get; set; }
        public virtual IncidentReviewEntity Review { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenaltys { get; set; }
    }
}
