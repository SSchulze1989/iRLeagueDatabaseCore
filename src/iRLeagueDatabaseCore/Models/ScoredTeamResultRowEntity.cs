using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredTeamResultRowEntity
    {
        public ScoredTeamResultRowEntity()
        {
            ScoredResultRows = new HashSet<ScoredResultRowEntity>();
        }

        public long ScoredTeamResultRowId { get; set; }
        public long LeagueId { get; set; }
        public long ScoredSessionResultId { get; set; }
        public long TeamId { get; set; }
        /// <summary>
        /// Imported Id from old database
        /// Will be deleted after imports have finished
        /// </summary>
        public long? ImportId { get; set; }

        public DateTime? Date { get; set; }
        public int ClassId { get; set; }
        public string CarClass { get; set; }
        public double RacePoints { get; set; }
        public double BonusPoints { get; set; }
        public double PenaltyPoints { get; set; }
        public int FinalPosition { get; set; }
        public int FinalPositionChange { get; set; }
        public double TotalPoints { get; set; }
        public long AvgLapTime { get; set; }
        public long FastestLapTime { get; set; }

        public virtual ScoredSessionResultEntity ScoredSessionResult { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
    }

    public class ScoredTeamResultRowEntityConfiguration : IEntityTypeConfiguration<ScoredTeamResultRowEntity>
    {
        public void Configure(EntityTypeBuilder<ScoredTeamResultRowEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ScoredTeamResultRowId });

            entity.HasAlternateKey(e => e.ScoredTeamResultRowId);

            entity.Property(e => e.ScoredTeamResultRowId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.TeamId);

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Team)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.TeamId })
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ScoredSessionResult)
                .WithMany(p => p.ScoredTeamResultRows)
                .HasForeignKey(d => new { d.LeagueId, d.ScoredSessionResultId });

            entity.HasMany(d => d.ScoredResultRows)
                .WithMany(p => p.ScoredTeamResultRows)
                .UsingEntity<ScoredTeamResultRowsResultRows>(
                    right => right.HasOne(e => e.ResultRow)
                        .WithMany()
                        .HasForeignKey(e => new {e.LeagueId, e.ResultRowRefId}),
                    left => left.HasOne(e => e.TeamResultRow)
                        .WithMany()
                        .HasForeignKey(e => new {e.LeagueId, e.TeamResultRowRefId}));
        }
    }
}
