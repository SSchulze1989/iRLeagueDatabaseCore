using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class CommentReviewVoteEntity
    {
        public long ReviewVoteId { get; set; }
        public long CommentId { get; set; }
        public long LeagueId { get; set; }
        public long? MemberAtFaultId { get; set; }
        public long? VoteCategoryId { get; set; }
        public string Description { get; set; }

        public virtual CommentBaseEntity Comment { get; set; }
        public virtual VoteCategoryEntity VoteCategory { get; set; }
        public virtual MemberEntity MemberAtFault { get; set; }
    }
}
