using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoringSession
    {
        public long ScoringRefId { get; set; }
        public long SessionRefId { get; set; }

        public virtual ScoringEntity ScoringRef { get; set; }
        public virtual SessionBaseEntity SessionRef { get; set; }
    }
}
