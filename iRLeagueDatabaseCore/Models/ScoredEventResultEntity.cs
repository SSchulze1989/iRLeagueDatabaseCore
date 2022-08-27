using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class ScoredEventResultEntity : IVersionEntity
    {
        public ScoredEventResultEntity()
        {
            ScoredSessionResults = new HashSet<ScoredSessionResultEntity>();
        }

        public long LeagueId { get; set; }
        public long EventId { get; set; }
        public long ResultTabId { get; set; }
        public long? ResultConfigId { get; set; }
        public long? ScoringId { get; set; }

        public virtual EventEntity Event { get; set; }
        public virtual ResultTabEntity ResultTab { get; set; }
        public virtual ICollection<ScoredSessionResultEntity> ScoredSessionResults { get; set; }
        public virtual ResultConfigurationEntity ResultConfig { get; set; }
        public virtual EventResultEntity RawResult { get; set; }
        public virtual ScoringEntity Scoring { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

    }

    public class ScoredEventResultEntityConfiguration : IEntityTypeConfiguration<ScoredEventResultEntity>
    {
        public void Configure(EntityTypeBuilder<ScoredEventResultEntity> entity)
        {
            entity.HasKey(e => new {e.LeagueId, e.EventId, e.ResultTabId});

            entity.HasAlternateKey(e => new { e.EventId, e.ResultTabId });

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Event)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.EventId })
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ResultTab)
                .WithMany(p => p.ScoredEventResults)
                .HasForeignKey(d => new { d.LeagueId, d.ResultTabId })
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.RawResult)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.EventId })
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ResultConfig)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.ResultConfigId })
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Scoring)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.ScoringId })
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
