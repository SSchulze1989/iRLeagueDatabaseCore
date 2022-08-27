using iRLeagueApiCore.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class EventEntity : IVersionEntity
    {
        public EventEntity()
        {
            Sessions = new HashSet<SessionEntity>();
            //ScoredEventResults = new HashSet<ScoredEventResultEntity>();
        }

        public long EventId { get; set; }
        public long LeagueId { get; set; }
        public long ScheduleId { get; set; }
        public long? TrackId { get; set; }

        public EventType EventType { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public string IrSessionId { get; set; }
        public string IrResultLink { get; set; }

        public virtual ScheduleEntity Schedule { get; set; }
        public virtual TrackConfigEntity Track { get; set; }
        public virtual EventResultEntity EventResult { get; set; }
        public virtual ICollection<SessionEntity> Sessions { get; set; }
        //public virtual ICollection<ScoredEventResultEntity> ScoredEventResults { get; set; }

        #region Version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion
    }

    public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> entity)
        {
            entity.HasKey(e => new {e.LeagueId, e.EventId});

            entity.HasAlternateKey(e => e.EventId);

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.Duration).HasConversion(new TimeSpanToTicksConverter());

            entity.Property(e => e.EventType)
                .HasConversion<string>();

            entity.Property(e => e.EventId)
                .ValueGeneratedOnAdd();

            entity.HasOne(e => e.Schedule)
                .WithMany(p => p.Events)
                .HasForeignKey(e => new { e.LeagueId, e.ScheduleId });

            entity.HasOne(d => d.Track)
                .WithMany()
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}
