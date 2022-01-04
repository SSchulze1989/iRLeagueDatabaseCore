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
            ResultEntities = new HashSet<ResultEntity>();
            ScheduleEntities = new HashSet<ScheduleEntity>();
            ScoringEntities = new HashSet<ScoringEntity>();
            ScoringTableEntities = new HashSet<ScoringTableEntity>();
            StatisticSetEntities = new HashSet<StatisticSetEntity>();
        }

        public long SeasonId { get; set; }
        public long LeagueId { get; set; }
        public LeagueEntity League { get; set; }
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

        public virtual ScoringEntity MainScoringScoring { get; set; }
        public virtual ICollection<ResultEntity> ResultEntities { get; set; }
        public virtual ICollection<ScheduleEntity> ScheduleEntities { get; set; }
        public virtual ICollection<ScoringEntity> ScoringEntities { get; set; }
        public virtual ICollection<ScoringTableEntity> ScoringTableEntities { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSetEntities { get; set; }
    }
}
