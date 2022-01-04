using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class LeagueMemberEntity
    {
        public LeagueMemberEntity()
        {
            AcceptedReviewVoteEntities = new HashSet<AcceptedReviewVoteEntity>();
            CommentReviewVoteEntities = new HashSet<CommentReviewVoteEntity>();
            DriverStatisticRowEntities = new HashSet<DriverStatisticRowEntity>();
            IncidentReviewInvolvedLeagueMembers = new HashSet<IncidentReviewInvolvedLeagueMember>();
            ResultRowEntities = new HashSet<ResultRowEntity>();
            ScoredResultCleanestDrivers = new HashSet<ScoredResultCleanestDriver>();
            ScoredResultEntityFastestAvgLapDriverMembers = new HashSet<ScoredResultEntity>();
            ScoredResultEntityFastestLapDriverMembers = new HashSet<ScoredResultEntity>();
            ScoredResultEntityFastestQualyLapDriverMembers = new HashSet<ScoredResultEntity>();
            ScoredResultHardChargers = new HashSet<ScoredResultHardCharger>();
            StatisticSetEntities = new HashSet<StatisticSetEntity>();
        }

        public long MemberId { get; set; }
        public long LeagueId { get; set; }
        public long? TeamId { get; set; }

        public virtual TeamEntity Team { get; set; }
        public virtual ICollection<AcceptedReviewVoteEntity> AcceptedReviewVoteEntities { get; set; }
        public virtual ICollection<CommentReviewVoteEntity> CommentReviewVoteEntities { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntities { get; set; }
        public virtual ICollection<IncidentReviewInvolvedLeagueMember> IncidentReviewInvolvedLeagueMembers { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRowEntities { get; set; }
        public virtual ICollection<ScoredResultCleanestDriver> ScoredResultCleanestDrivers { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResultEntityFastestAvgLapDriverMembers { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResultEntityFastestLapDriverMembers { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResultEntityFastestQualyLapDriverMembers { get; set; }
        public virtual ICollection<ScoredResultHardCharger> ScoredResultHardChargers { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSetEntities { get; set; }
    }
}
