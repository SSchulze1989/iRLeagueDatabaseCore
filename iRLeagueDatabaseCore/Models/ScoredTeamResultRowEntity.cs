using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredTeamResultRowEntity
    {
        public ScoredTeamResultRowEntity()
        {
            ScoredResultRows = new HashSet<ScoredResultRowEntity>();
        }

        public long ScoredResultRowId { get; set; }
        public long ScoredResultId { get; set; }
        public long ScoringId { get; set; }
        public long TeamId { get; set; }
        public DateTime? Date { get; set; }
        public int ClassId { get; set; }
        public string CarClass { get; set; }
        public double RacePoints { get; set; }
        public double BonusPoints { get; set; }
        public double PenaltyPoints { get; set; }
        public int FinalPosition { get; set; }
        public int FinalPositionChange { get; set; }
        public double TotalPoints { get; set; }
        public long AvgLapTime { get; set; }
        public long FastestLapTime { get; set; }

        public virtual ScoredResultEntity ScoredResult { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
    }
}
