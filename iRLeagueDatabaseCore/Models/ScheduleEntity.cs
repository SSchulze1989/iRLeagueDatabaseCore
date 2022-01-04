using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScheduleEntity
    {
        public ScheduleEntity()
        {
            ScoringEntities = new HashSet<ScoringEntity>();
            SessionBaseEntities = new HashSet<SessionBaseEntity>();
        }

        public long ScheduleId { get; set; }
        public long LeagueId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long SeasonSeasonId { get; set; }

        public virtual SeasonEntity SeasonSeason { get; set; }
        public virtual ICollection<ScoringEntity> ScoringEntities { get; set; }
        public virtual ICollection<SessionBaseEntity> SessionBaseEntities { get; set; }
    }
}
