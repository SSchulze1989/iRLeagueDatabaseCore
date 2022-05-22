using iRLeagueApiCore.Communication.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace iRLeagueDatabaseCore.Models
{
    public partial class SubResultEntity : IVersionEntity
    {
        public SubResultEntity()
        {
            ResultRows = new HashSet<ResultRowEntity>();
        }

        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public int SubSessionNr { get; set; }
        public SimSessionType SimSessionType { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

        public virtual ResultEntity Result { get; set; }
        public virtual SubSessionEntity SubSession { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRows { get; set; }
    }

    public class SubResultEntityConfiguration : IEntityTypeConfiguration<SubResultEntity>
    {
        public void Configure(EntityTypeBuilder<SubResultEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.SessionId, e.SubSessionNr });

            entity.HasIndex(e => new { e.SessionId, e.SubSessionNr });

            entity.HasOne(d => d.Result)
                .WithMany(p => p.SubResults)
                .HasForeignKey(d => new { d.LeagueId, d.SessionId });

            entity.HasOne(d => d.SubSession)
                .WithOne(p => p.SubResult)
                .HasForeignKey<SubResultEntity>(d => new { d.LeagueId, d.SessionId, d.SubSessionNr })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}