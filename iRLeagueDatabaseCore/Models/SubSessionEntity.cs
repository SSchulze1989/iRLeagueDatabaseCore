using iRLeagueApiCore.Communication.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace iRLeagueDatabaseCore.Models
{
    public partial class SubSessionEntity : IVersionEntity
    {
        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public int SubSessionNr { get; set; }
        public string Name { get; set; }
        public SessionType SessionType { get; set; }
        public TimeSpan StartOffset { get; set; }
        public TimeSpan Duration { get; set; }
        public int Laps { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

        public virtual SessionEntity ParentSession { get; set; }
        public virtual SubResultEntity SubResult { get; set; }
    }

    public class SubSessionEntityConfiguration : IEntityTypeConfiguration<SubSessionEntity>
    {
        public void Configure(EntityTypeBuilder<SubSessionEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.SessionId, e.SubSessionNr });

            entity.HasIndex(e => new { e.SessionId, e.SubSessionNr });

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.StartOffset).HasConversion(new TimeSpanToTicksConverter());

            entity.Property(e => e.Duration).HasConversion(new TimeSpanToTicksConverter());

            entity.Property(e => e.SessionType)
                .HasConversion<string>();

            entity.HasOne(d => d.ParentSession)
                .WithMany(p => p.SubSessions)
                .HasForeignKey(d => new { d.LeagueId, d.SessionId })
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
