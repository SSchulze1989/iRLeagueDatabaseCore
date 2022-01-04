using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultRowEntity
    {
        public ScoredResultRowEntity()
        {
            DriverStatisticRowEntityFirstResultRows = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityLastResultRows = new HashSet<DriverStatisticRowEntity>();
            ReviewPenaltyEntities = new HashSet<ReviewPenaltyEntity>();
            ScoredTeamResultRowsGroups = new HashSet<ScoredTeamResultRowsGroup>();
        }

        public long ScoredResultRowId { get; set; }
        public long ScoredResultId { get; set; }
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
        public virtual ScoredResultEntity Scor { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual AddPenaltyEntity AddPenaltyEntity { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityFirstResultRows { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityLastResultRows { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenaltyEntities { get; set; }
        public virtual ICollection<ScoredTeamResultRowsGroup> ScoredTeamResultRowsGroups { get; set; }
    }
}
