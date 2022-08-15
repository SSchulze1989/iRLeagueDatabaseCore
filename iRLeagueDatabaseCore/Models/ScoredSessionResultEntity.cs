using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredSessionResultEntity : IVersionEntity
    {
        public ScoredSessionResultEntity()
        {
            CleanestDrivers = new HashSet<MemberEntity>();
            HardChargers = new HashSet<MemberEntity>();
            ScoredResultRows = new HashSet<ScoredResultRowEntity>();
            ScoredTeamResultRows = new HashSet<ScoredTeamResultRowEntity>();
        }

        public long LeagueId { get; set; }
        public long EventId { get; set; }
        public long ResultTabId { get; set; }
        public long? ScoringId { get; set; }

        public long FastestLap { get; set; }
        public long FastestQualyLap { get; set; }
        public long FastestAvgLap { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public string Discriminator { get; set; }
        public long? FastestAvgLapDriverMemberId { get; set; }
        public long? FastestLapDriverMemberId { get; set; }
        public long? FastestQualyLapDriverMemberId { get; set; }

        public virtual MemberEntity FastestAvgLapDriver { get; set; }
        public virtual MemberEntity FastestLapDriver { get; set; }
        public virtual MemberEntity FastestQualyLapDriver { get; set; }
        public virtual ScoredEventResultEntity ScoredEventResult { get; set; }
        public virtual ScoringEntity Scoring { get; set; }
        public virtual ICollection<MemberEntity> CleanestDrivers { get; set; }
        public virtual ICollection<MemberEntity> HardChargers { get; set; }
        public virtual ICollection<ScoredResultRowEntity> ScoredResultRows { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRows { get; set; }
    }

    public class ScoredSessionResultEntityConfiguration : IEntityTypeConfiguration<ScoredSessionResultEntity>
    {
        public void Configure(EntityTypeBuilder<ScoredSessionResultEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.EventId, e.ResultTabId });

            entity.HasIndex(e => e.FastestAvgLapDriverMemberId);

            entity.HasIndex(e => e.FastestLapDriverMemberId);

            entity.HasIndex(e => e.FastestQualyLapDriverMemberId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.FastestAvgLapDriver)
                .WithMany(p => p.FastestAvgLapResults)
                .HasForeignKey(d => d.FastestAvgLapDriverMemberId);

            entity.HasOne(d => d.FastestLapDriver)
                .WithMany(p => p.FastestLapResults)
                .HasForeignKey(d => d.FastestLapDriverMemberId);

            entity.HasOne(d => d.FastestQualyLapDriver)
                .WithMany(p => p.FastestQualyLapResults)
                .HasForeignKey(d => d.FastestQualyLapDriverMemberId);

            entity.HasOne(d => d.ScoredEventResult)
                .WithMany(p => p.ScoredSessionResults)
                .HasForeignKey(d => new { d.LeagueId, d.EventId, d.ResultTabId });

            entity.HasMany(d => d.CleanestDrivers)
                .WithMany(p => p.CleanestDriverResults)
                .UsingEntity(e => e.ToTable("ScoredResultsCleanestDrivers"));

            entity.HasMany(d => d.HardChargers)
                .WithMany(p => p.HardChargerResults)
                .UsingEntity(e => e.ToTable("ScoredResultsHardChargers"));

            entity.HasOne(d => d.Scoring)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.ScoringId })
                .IsRequired(false);
        }
    }
}
