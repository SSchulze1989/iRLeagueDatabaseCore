using iRLeagueApiCore.Common.Enums;
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
            ScoredResults = new HashSet<ScoredResultEntity>();
            Sessions = new HashSet<SessionEntity>();
            Standings = new HashSet<StandingEntity>();
            Seasons = new HashSet<SeasonEntity>();
        }

        public long ScoringId { get; set; }
        public long LeagueId { get; set; }
        public int ScoringKind { get; set; }
        public string Name { get; set; }
        public int DropWeeks { get; set; }
        public int AverageRaceNr { get; set; }
        public int MaxResultsPerGroup { get; set; }
        public bool TakeGroupAverage { get; set; }
        public long? ExtScoringSourceId { get; set; }
        public bool TakeResultsFromExtSource { get; set; }
        public string BasePoints { get; set; }
        public string BonusPoints { get; set; }
        public string IncPenaltyPoints { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long? ConnectedScheduleId { get; set; }
        public long SeasonId { get; set; }
        public bool UseResultSetTeam { get; set; }
        public bool UpdateTeamOnRecalculation { get; set; }
        public long? ParentScoringId { get; set; }
        public SessionType ScoringSessionType { get; set; }
        public ScoringSessionSelectionType SessionSelectType { get; set; }
        public string ScoringWeightValues { get; set; }
        public AccumulateByOption AccumulateBy { get; set; }
        public AccumulateResultsOption AccumulateResultsOption { get; set; }
        public string PointsSortOptions { get; set; }
        public string FinalSortOptions { get; set; }
        public bool ShowResults { get; set; }
        public string Description { get; set; }

        public virtual ScheduleEntity ConnectedSchedule { get; set; }
        public virtual ScoringEntity ExtScoringSource { get; set; }
        public virtual ScoringEntity ParentScoring { get; set; }
        public virtual SeasonEntity Season { get; set; }
        public virtual ICollection<ScoringEntity> DependendScorings { get; set; }
        public virtual ICollection<ResultsFilterOptionEntity> ResultsFilterOptions { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResults { get; set; }
        public virtual ICollection<SessionEntity> Sessions { get; set; }
        public virtual ICollection<StandingEntity> Standings { get; set; }
        public virtual ICollection<SeasonEntity> Seasons { get; set; }
    }

    public class ScoringEntityConfiguration : IEntityTypeConfiguration<ScoringEntity>
    {
        public void Configure(EntityTypeBuilder<ScoringEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ScoringId });

            entity.HasAlternateKey(e => e.ScoringId);

            entity.Property(e => e.ScoringId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.ConnectedScheduleId);

            entity.HasIndex(e => e.ExtScoringSourceId);

            entity.HasIndex(e => e.ParentScoringId);

            entity.HasIndex(e => new { e.LeagueId, e.SeasonId });

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.ShowResults)
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.ConnectedSchedule)
                .WithMany(p => p.Scorings)
                .HasForeignKey(d => new { d.LeagueId, d.ConnectedScheduleId });

            entity.HasOne(d => d.ExtScoringSource)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.ExtScoringSourceId });

            entity.HasOne(d => d.ParentScoring)
                .WithMany(p => p.DependendScorings)
                .HasForeignKey(d => new { d.LeagueId, d.ParentScoringId });

            entity.HasOne(d => d.Season)
                .WithMany(p => p.Scorings)
                .HasForeignKey(d => new { d.LeagueId, d.SeasonId });

            entity.HasMany(d => d.Sessions)
                .WithMany(p => p.Scorings)
                .UsingEntity<ScoringsSessions>(
                    left => left.HasOne(e => e.SessionRef)
                        .WithMany().HasForeignKey(e => new { e.LeagueId, e.SessionRefId }),
                    right => right.HasOne(e => e.ScoringRef)
                        .WithMany().HasForeignKey(e => new { e.LeagueId, e.ScoringRefId }));
        }
    }
}
