using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class StatisticSetEntity
    {
        public StatisticSetEntity()
        {
            DriverStatisticRows = new HashSet<DriverStatisticRowEntity>();
            LeagueStatisticSets = new HashSet<StatisticSetEntity>();
            DependendStatisticSets = new HashSet<StatisticSetEntity>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long UpdateInterval { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool RequiresRecalculation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long? CurrentChampId { get; set; }
        public long? SeasonId { get; set; }
        public long? ScoringTableId { get; set; }
        public int? FinishedRaces { get; set; }
        public bool? IsSeasonFinished { get; set; }
        public string ImportSource { get; set; }
        public string Description { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string Discriminator { get; set; }

        public virtual MemberEntity CurrentChamp { get; set; }
        public virtual ScoringTableEntity ScoringTable { get; set; }
        public virtual SeasonEntity Season { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRows { get; set; }
        public virtual ICollection<StatisticSetEntity> LeagueStatisticSets { get; set; }
        public virtual ICollection<StatisticSetEntity> DependendStatisticSets { get; set; }
    }
}
