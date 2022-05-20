using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultRowEntity : ResultRowBase
    {
        public ResultRowEntity()
        {
        }

        public long ResultRowId { get; set; }
        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public int SubSessionNr { get; set; }
        public long MemberId { get; set; }
        public long? TeamId { get; set; }
        public bool PointsEligible { get; set; }


        public virtual MemberEntity Member { get; set; }
        public virtual SubResultEntity SubResult { get; set; }
        public virtual TeamEntity Team { get; set; }
    }
}
