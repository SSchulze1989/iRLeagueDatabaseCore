﻿using iRLeagueApiCore.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoringEntity : IVersionEntity
    {
        public ScoringEntity()
        {
            DependendScorings = new HashSet<ScoringEntity>();
            ResultsFilterOptions = new HashSet<ResultsFilterOptionEntity>();
            Standings = new HashSet<StandingEntity>();
        }

        public long LeagueId { get; set; }
        public long ScoringId { get; set; }
        public long ResultConfigId { get; set; }
        public long? PointsRuleId { get; set; }

        public ScoringKind ScoringKind { get; set; }
        public string Name { get; set; }
        public int MaxResultsPerGroup { get; set; }
        public long? ExtScoringSourceId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public bool UseResultSetTeam { get; set; }
        public bool UpdateTeamOnRecalculation { get; set; }
        public bool ShowResults { get; set; }

        public virtual ScoringEntity ExtScoringSource { get; set; }
        public virtual ResultConfigurationEntity ResultConfiguration { get; set; }
        public virtual PointRuleEntity PointsRule { get; set; }
        public virtual ICollection<ScoringEntity> DependendScorings { get; set; }
        public virtual ICollection<ResultsFilterOptionEntity> ResultsFilterOptions { get; set; }
        public virtual ICollection<StandingEntity> Standings { get; set; }
    }

    public class ScoringEntityConfiguration : IEntityTypeConfiguration<ScoringEntity>
    {
        public void Configure(EntityTypeBuilder<ScoringEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ScoringId });

            entity.HasAlternateKey(e => e.ScoringId);

            entity.Property(e => e.ScoringId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.ExtScoringSourceId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.ShowResults)
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ScoringKind)
                .HasConversion<string>();

            entity.HasOne(d => d.ExtScoringSource)
                .WithMany(p => p.DependendScorings)
                .HasForeignKey(d => new { d.LeagueId, d.ExtScoringSourceId });

            entity.HasOne(d => d.ResultConfiguration)
                .WithMany(p => p.Scorings)
                .HasForeignKey(d => new { d.LeagueId, d.ResultConfigId });

            entity.HasOne(d => d.PointsRule)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.PointsRuleId });
        }
    }
}