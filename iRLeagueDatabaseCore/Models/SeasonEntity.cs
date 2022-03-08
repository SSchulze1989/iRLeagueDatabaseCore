using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class SeasonEntity
    {
        public SeasonEntity()
        {
            Results = new HashSet<ResultEntity>();
            Schedules = new HashSet<ScheduleEntity>();
            Scorings = new HashSet<ScoringEntity>();
            ScoringTables = new HashSet<ScoringTableEntity>();
            StatisticSets = new HashSet<StatisticSetEntity>();
        }

        public long SeasonId { get; set; }
        public long LeagueId { get; set; }
        public string SeasonName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long? MainScoringScoringId { get; set; }
        public bool HideCommentsBeforeVoted { get; set; }
        public bool Finished { get; set; }
        public DateTime? SeasonStart { get; set; }
        public DateTime? SeasonEnd { get; set; }

        public virtual LeagueEntity League { get; set; }
        public virtual ScoringEntity MainScoring { get; set; }
        public virtual ICollection<ResultEntity> Results { get; set; }
        public virtual ICollection<ScheduleEntity> Schedules { get; set; }
        public virtual ICollection<ScoringEntity> Scorings { get; set; }
        public virtual ICollection<ScoringTableEntity> ScoringTables { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSets { get; set; }
    }
}
