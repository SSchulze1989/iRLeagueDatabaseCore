using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class TeamEntity
    {
        public TeamEntity()
        {
            LeagueMemberEntities = new HashSet<MemberEntity>();
            ResultRowEntities = new HashSet<ResultRowEntity>();
            ScoredResultRowEntities = new HashSet<ScoredResultRowEntity>();
            ScoredTeamResultRowEntities = new HashSet<ScoredTeamResultRowEntity>();
        }

        public long TeamId { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string TeamColor { get; set; }
        public string TeamHomepage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }

        public virtual ICollection<MemberEntity> LeagueMemberEntities { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRowEntities { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRowEntities { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRowEntities { get; set; }
    }
}
