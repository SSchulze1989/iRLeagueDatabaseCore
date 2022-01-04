using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultEntity
    {
        public ScoredResultEntity()
        {
            ScoredResultCleanestDrivers = new HashSet<ScoredResultCleanestDriver>();
            ScoredResultHardChargers = new HashSet<ScoredResultHardCharger>();
            ScoredResultRowEntities = new HashSet<ScoredResultRowEntity>();
            ScoredTeamResultRowEntities = new HashSet<ScoredTeamResultRowEntity>();
        }

        public long ResultId { get; set; }
        public long ScoringId { get; set; }
        public long FastestLap { get; set; }
        public long FastestQualyLap { get; set; }
        public long FastestAvgLap { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public string Discriminator { get; set; }
        public long? FastestAvgLapDriverMemberId { get; set; }
        public long? FastestLapDriverMemberId { get; set; }
        public long? FastestQualyLapDriverMemberId { get; set; }

        public virtual MemberEntity FastestAvgLapDriverMember { get; set; }
        public virtual MemberEntity FastestLapDriverMember { get; set; }
        public virtual MemberEntity FastestQualyLapDriverMember { get; set; }
        public virtual ResultEntity Result { get; set; }
        public virtual ScoringEntity Scoring { get; set; }
        public virtual ICollection<ScoredResultCleanestDriver> ScoredResultCleanestDrivers { get; set; }
        public virtual ICollection<ScoredResultHardCharger> ScoredResultHardChargers { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRowEntities { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRowEntities { get; set; }
    }
}
