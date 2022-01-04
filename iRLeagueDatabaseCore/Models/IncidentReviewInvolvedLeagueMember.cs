using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class IncidentReviewInvolvedLeagueMember
    {
        public long ReviewRefId { get; set; }
        public long MemberRefId { get; set; }

        public virtual MemberEntity MemberRef { get; set; }
        public virtual IncidentReviewEntity ReviewRef { get; set; }
    }
}
