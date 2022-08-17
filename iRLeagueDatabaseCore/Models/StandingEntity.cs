using iRLeagueApiCore.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

    public class StandingEntityConfiguration : IEntityTypeConfiguration<StandingEntity>
    {
        public void Configure(EntityTypeBuilder<StandingEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.StandingId });

            entity.HasIndex(e => new { e.LeagueId, e.SeasonId });

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Season)
                .WithMany(p => p.Standings)
                .HasForeignKey(d => new { d.LeagueId, d.SeasonId });

            entity.HasMany(d => d.Scorings)
                .WithMany(p => p.Standings)
                .UsingEntity<StandingsScorings>(
                    left => left.HasOne(d => d.ScoringRef)
                        .WithMany().HasForeignKey(e => new { e.LeagueId, e.ScoringRefId }),
                    right => right.HasOne(d => d.StandingRef)
                        .WithMany().HasForeignKey(e => new { e.LeagueId, e.StandingRefId }));
        }
    }
}
