using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class VoteCategoryEntity
    {
        public VoteCategoryEntity()
        {
            AcceptedReviewVotes = new HashSet<AcceptedReviewVoteEntity>();
            CommentReviewVotes = new HashSet<CommentReviewVoteEntity>();
        }

        public long CatId { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
        public int DefaultPenalty { get; set; }

        public virtual ICollection<AcceptedReviewVoteEntity> AcceptedReviewVotes { get; set; }
        public virtual ICollection<CommentReviewVoteEntity> CommentReviewVotes { get; set; }
    }
}
