using iRLeagueApiCore.Communication.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class SessionEntity : IVersionEntity
    {
        public SessionEntity()
        {
            IncidentReviews = new HashSet<IncidentReviewEntity>();
            Scorings = new HashSet<ScoringEntity>();
            SubSessions = new HashSet<SubSessionEntity>();
        }

        public long SessionId { get; set; }
        public long LeagueId { get; set; }
        public string Name { get; set; }
        public SessionType SessionType { get; set; }
        public DateTime? Date { get; set; }
        public long? TrackId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        
        public long? PracticeSubSessionId { get; set; }
        public long? QualifyingSubSessionId { get; set; }
        public long? RaceSubSessionId { get; set; }
        public string IrSessionId { get; set; }
        public string IrResultLink { get; set; }
        public long ScheduleId { get; set; }

        public virtual ScheduleEntity Schedule { get; set; }
        public virtual ResultEntity Result { get; set; }
        public virtual TrackConfigEntity Track { get; set; }
        public virtual ICollection<IncidentReviewEntity> IncidentReviews { get; set; }
        public virtual ICollection<SubSessionEntity> SubSessions { get; set; }
        public virtual ICollection<ScoringEntity> Scorings { get; set; }
    }

    public class SessionEntityConfiguration : IEntityTypeConfiguration<SessionEntity>
    {
        public void Configure(EntityTypeBuilder<SessionEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.SessionId });

            entity.HasAlternateKey(e => e.SessionId);

            entity.Property(e => e.SessionId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => new { e.LeagueId, e.ScheduleId });

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.Duration).HasConversion(new TimeSpanToTicksConverter());

            entity.Property(e => e.SessionType)
                .HasConversion<string>();

            entity.HasOne(d => d.Schedule)
                .WithMany(p => p.Sessions)
                .HasForeignKey(d => new { d.LeagueId, d.ScheduleId })
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Track)
                .WithMany()
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}
