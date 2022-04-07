using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class TeamEntity : IVersionEntity
    {
        public TeamEntity()
        {
            LeagueMemberEntities = new HashSet<MemberEntity>();
            ResultRows = new HashSet<ResultRowEntity>();
            ScoredResultRows = new HashSet<ScoredResultRowEntity>();
            ScoredTeamResultRows = new HashSet<ScoredTeamResultRowEntity>();
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
        public virtual ICollection<ResultRowEntity> ResultRows { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRows { get; set; }
    }
}
