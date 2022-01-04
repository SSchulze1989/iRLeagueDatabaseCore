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
            IncidentReviewsInvolved = new HashSet<IncidentReviewInvolvedLeagueMember>();
            ResultRowEntities = new HashSet<ResultRowEntity>();
            CleanestDriverResults = new HashSet<ScoredResultCleanestDriver>();
            FastestAvgLapResults = new HashSet<ScoredResultEntity>();
            FastestLapResults = new HashSet<ScoredResultEntity>();
            FastestQualyLapResults = new HashSet<ScoredResultEntity>();
            HardChargerResults = new HashSet<ScoredResultHardCharger>();
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
        public virtual ICollection<IncidentReviewInvolvedLeagueMember> IncidentReviewsInvolved { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRowEntities { get; set; }
        public virtual ICollection<ScoredResultCleanestDriver> CleanestDriverResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestAvgLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestQualyLapResults { get; set; }
        public virtual ICollection<ScoredResultHardCharger> HardChargerResults { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSetEntities { get; set; }
    }
}
