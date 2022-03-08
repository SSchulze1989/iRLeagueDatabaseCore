using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultEntity
    {
        public ScoredResultEntity()
        {
            CleanestDrivers = new HashSet<MemberEntity>();
            HardChargers = new HashSet<MemberEntity>();
            ScoredResultRows = new HashSet<ScoredResultRowEntity>();
            ScoredTeamResultRows = new HashSet<ScoredTeamResultRowEntity>();
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

        public virtual MemberEntity FastestAvgLapDriver { get; set; }
        public virtual MemberEntity FastestLapDriver { get; set; }
        public virtual MemberEntity FastestQualyLapDriver { get; set; }
        public virtual ResultEntity Result { get; set; }
        public virtual ScoringEntity Scoring { get; set; }
        public virtual ICollection<MemberEntity> CleanestDrivers { get; set; }
        public virtual ICollection<MemberEntity> HardChargers { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRows { get; set; }
    }
}
