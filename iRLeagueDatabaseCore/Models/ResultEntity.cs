using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultEntity : IVersionEntity
    {
        public ResultEntity()
        {
            ResultRows = new HashSet<ResultRowEntity>();
            ScoredResults = new HashSet<ScoredResultEntity>();
        }

        public long ResultId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long? SeasonId { get; set; }
        public long LeagueId { get; set; }
        public bool RequiresRecalculation { get; set; }
        public long PoleLaptime { get; set; }

        public virtual SessionEntity Session { get; set; }
        public virtual SeasonEntity Season { get; set; }
        public virtual IRSimSessionDetailsEntity IRSimSessionDetails { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRows { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResults { get; set; }
    }
}
