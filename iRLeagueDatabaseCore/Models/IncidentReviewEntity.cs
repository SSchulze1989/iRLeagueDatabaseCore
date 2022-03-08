﻿using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class IncidentReviewEntity
    {
        public IncidentReviewEntity()
        {
            AcceptedReviewVoteEntities = new HashSet<AcceptedReviewVoteEntity>();
            CommentBaseEntities = new HashSet<CommentBaseEntity>();
            InvolvedMembers = new HashSet<MemberEntity>();
            ReviewPenaltyEntities = new HashSet<ReviewPenaltyEntity>();
        }

        public long ReviewId { get; set; }
        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public string AuthorUserId { get; set; }
        public string AuthorName { get; set; }
        public string IncidentKind { get; set; }
        public string FullDescription { get; set; }
        public string OnLap { get; set; }
        public string Corner { get; set; }
        public TimeSpan TimeStamp { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public string ResultLongText { get; set; }
        public string IncidentNr { get; set; }

        public virtual SessionEntity Session { get; set; }
        public virtual ICollection<AcceptedReviewVoteEntity> AcceptedReviewVoteEntities { get; set; }
        public virtual ICollection<CommentBaseEntity> CommentBaseEntities { get; set; }
        public virtual ICollection<MemberEntity> InvolvedMembers { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenaltyEntities { get; set; }
    }
}
