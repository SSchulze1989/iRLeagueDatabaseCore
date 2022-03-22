using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultRowEntity
    {
        public ResultRowEntity()
        {
            ScoredResultRows = new HashSet<ScoredResultRowEntity>();
        }

        public long ResultRowId { get; set; }
        public long LeagueId { get; set; }
        public long ResultId { get; set; }
        public double StartPosition { get; set; }
        public double FinishPosition { get; set; }
        public long MemberId { get; set; }
        public int CarNumber { get; set; }
        public int ClassId { get; set; }
        public string Car { get; set; }
        public string CarClass { get; set; }
        public double CompletedLaps { get; set; }
        public double LeadLaps { get; set; }
        public int FastLapNr { get; set; }
        public double Incidents { get; set; }
        public int Status { get; set; }
        public long QualifyingTime { get; set; }
        public long Interval { get; set; }
        public long AvgLapTime { get; set; }
        public long FastestLapTime { get; set; }
        public double PositionChange { get; set; }
        public string IracingId { get; set; }
        public int SimSessionType { get; set; }
        public int OldIrating { get; set; }
        public int NewIrating { get; set; }
        public int SeasonStartIrating { get; set; }
        public string License { get; set; }
        public double OldSafetyRating { get; set; }
        public double NewSafetyRating { get; set; }
        public int OldCpi { get; set; }
        public int NewCpi { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int CarId { get; set; }
        public double CompletedPct { get; set; }
        public DateTime? QualifyingTimeAt { get; set; }
        public int Division { get; set; }
        public int OldLicenseLevel { get; set; }
        public int NewLicenseLevel { get; set; }
        public int NumPitStops { get; set; }
        public string PittedLaps { get; set; }
        public int NumOfftrackLaps { get; set; }
        public string OfftrackLaps { get; set; }
        public int NumContactLaps { get; set; }
        public string ContactLaps { get; set; }
        public long? TeamId { get; set; }
        public bool PointsEligible { get; set; }

        public virtual MemberEntity Member { get; set; }
        public virtual ResultEntity Result { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
    }
}
