using iRLeagueApiCore.Communication.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultsFilterOptionEntity
    {
        public long ResultsFilterId { get; set; }
        public long LeagueId { get; set; }
        public long ScoringId { get; set; }
        public string ResultsFilterType { get; set; }
        public string ColumnPropertyName { get; set; }
        public ComparatorType Comparator { get; set; }
        public bool Exclude { get; set; }
        public string FilterValues { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public bool FilterPointsOnly { get; set; }

        public virtual ScoringEntity Scoring { get; set; }
    }
}
