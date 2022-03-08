using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class MemberEntity
    {
        public MemberEntity()
        {
            AcceptedReviewVoteEntities = new HashSet<AcceptedReviewVoteEntity>();
            CommentReviewVoteEntities = new HashSet<CommentReviewVoteEntity>();
            DriverStatisticRowEntities = new HashSet<DriverStatisticRowEntity>();
            InvolvedReviews = new HashSet<IncidentReviewEntity>();
            ResultRowEntities = new HashSet<ResultRowEntity>();
            CleanestDriverResults = new HashSet<ScoredResultEntity>();
            FastestAvgLapResults = new HashSet<ScoredResultEntity>();
            FastestLapResults = new HashSet<ScoredResultEntity>();
            FastestQualyLapResults = new HashSet<ScoredResultEntity>();
            HardChargerResults = new HashSet<ScoredResultEntity>();
            StatisticSetEntities = new HashSet<StatisticSetEntity>();
        }

        public long MemberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string IRacingId { get; set; }
        public string DanLisaId { get; set; }
        public string DiscordId { get; set; }

        public virtual ICollection<AcceptedReviewVoteEntity> AcceptedReviewVoteEntities { get; set; }
        public virtual ICollection<CommentReviewVoteEntity> CommentReviewVoteEntities { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntities { get; set; }
        public virtual ICollection<IncidentReviewEntity> InvolvedReviews { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRowEntities { get; set; }
        public virtual ICollection<ScoredResultEntity> CleanestDriverResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestAvgLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestQualyLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> HardChargerResults { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSetEntities { get; set; }
    }
}
