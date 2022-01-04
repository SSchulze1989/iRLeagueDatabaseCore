using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultHardCharger
    {
        public long ResultRefId { get; set; }
        public long ScoringRefId { get; set; }
        public long MemberRefId { get; set; }

        public virtual MemberEntity MemberRef { get; set; }
        public virtual ScoredResultEntity ScoredResultEntity { get; set; }
    }
}
