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

        }

        public long LeagueId { get; set; }
        public long EventId { get; set; }
        public long ResultTabId { get; set; }

        public virtual EventEntity Event { get; set; }
        public virtual EventResultEntity EventResult { get; set; }
        public virtual ResultTabEntity ResultTab { get; set; }
        public virtual ICollection<ScoredSessionResultEntity> ScoredSessionResults { get; set; }

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
                .HasForeignKey(d => new { d.LeagueId, d.EventId });

            entity.HasOne(d => d.EventResult)
                .WithMany(p => p.ScoredResults)
                .HasForeignKey(d => new { d.LeagueId, d.EventId });

            entity.HasOne(d => d.ResultTab)
                .WithMany(p => p.ScoredEventResults)
                .HasForeignKey(d => new { d.LeagueId, d.ResultTabId });
        }
    }
}
