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

        public long ScoredResultRowId { get; set; }
        public long LeagueId { get; set; }
        public long ScoredResultId { get; set; }
        public long ScoringId { get; set; }
        public long TeamId { get; set; }
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

        public virtual ScoredSessionResultEntity ScoredResult { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
    }

    public class ScoredTeamResultRowEntityConfiguration : IEntityTypeConfiguration<ScoredTeamResultRowEntity>
    {
        public void Configure(EntityTypeBuilder<ScoredTeamResultRowEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ScoredResultRowId });

            entity.HasIndex(e => e.ScoredResultRowId);

            entity.HasIndex(e => new { e.ScoredResultId, e.ScoringId });

            entity.HasIndex(e => e.TeamId);

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Team)
                .WithMany(p => p.ScoredTeamResultRows)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ScoredResult)
                .WithMany(p => p.ScoredTeamResultRows)
                .HasForeignKey(d => new { d.LeagueId, d.ScoredResultId, d.ScoringId });

            entity.HasMany(d => d.ScoredResultRows)
                .WithMany(p => p.ScoredTeamResultRows)
                .UsingEntity(e => e.ToTable("ScoredTeamResultRowsResultRows"));
        }
    }
}
