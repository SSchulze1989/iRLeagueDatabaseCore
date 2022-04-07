﻿using iRLeagueApiCore.Communication.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class StandingEntity : IVersionEntity
    {
        public StandingEntity()
        {
            Scorings = new HashSet<ScoringEntity>();
            StatisticSets = new HashSet<StatisticSetEntity>();
        }

        public long StandingId { get; set; }
        public int ScoringKind { get; set; }
        public string Name { get; set; }
        public int DropWeeks { get; set; }
        public int AverageRaceNr { get; set; }
        public string ScoringFactors { get; set; }
        public DropRacesOption DropRacesOption { get; set; }
        public int ResultsPerRaceCount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long SeasonId { get; set; }
        public long LeagueId { get; set; }

        public virtual SeasonEntity Season { get; set; }
        public virtual ICollection<ScoringEntity> Scorings { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSets { get; set; }
    }
}