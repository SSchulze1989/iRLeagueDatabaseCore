using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultRowEntity
    {
        public ScoredResultRowEntity()
        {
            ReviewPenalty = new HashSet<ReviewPenaltyEntity>();
            ScoredTeamResultRows = new HashSet<ScoredTeamResultRowEntity>();
        }

        public long ScoredResultRowId { get; set; }
        public long LeagueId { get; set; }
        public long ResultId { get; set; }
        public long ScoringId { get; set; }
        public long ResultRowId { get; set; }
        public double RacePoints { get; set; }
        public double BonusPoints { get; set; }
        public double PenaltyPoints { get; set; }
        public int FinalPosition { get; set; }
        public double FinalPositionChange { get; set; }
        public double TotalPoints { get; set; }
        public long? TeamId { get; set; }
        public bool PointsEligible { get; set; }

        public virtual ResultRowEntity ResultRow { get; set; }
        public virtual ScoredResultEntity Scoring { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual AddPenaltyEntity AddPenalty { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenalty { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRows { get; set; }
    }
}
