using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultEntity : IVersionEntity
    {
        public ResultEntity()
        {
            SubResults = new HashSet<SubResultEntity>();
            ScoredResults = new HashSet<ScoredResultEntity>();
        }

        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public long? IRSimSessionDetailsId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public bool RequiresRecalculation { get; set; }

        public virtual SessionEntity Session { get; set; }
        public virtual IRSimSessionDetailsEntity IRSimSessionDetails { get; set; }
        public virtual ICollection<SubResultEntity> SubResults { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResults { get; set; }
    }
}
