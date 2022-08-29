using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class LeagueMemberEntity
    {
        public LeagueMemberEntity()
        {
        }

        public long MemberId { get; set; }
        public long LeagueId { get; set; }
        public long? TeamId { get; set; }

        public virtual MemberEntity Member { get; set; }
        public virtual LeagueEntity League { get; set; }
        public virtual TeamEntity Team { get; set; }
    }
}
