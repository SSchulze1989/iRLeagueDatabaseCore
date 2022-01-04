using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultCleanestDriver
    {
        public long ResultRefId { get; set; }
        public long ScoringRefId { get; set; }
        public long LeagueMemberRefId { get; set; }

        public virtual MemberEntity LeagueMemberRef { get; set; }
        public virtual ScoredResultEntity ScoredResultEntity { get; set; }
    }
}
