using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultEntity : IVersionEntity
    {
        public ResultEntity()
        {
            SubResults = new HashSet<SubResultEntity>();
            ScoredResults = new HashSet<ScoredResultEntity>();
        }

        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public bool RequiresRecalculation { get; set; }

        public virtual SessionEntity Session { get; set; }
        public virtual ICollection<SubResultEntity> SubResults { get; set; }
        public virtual ICollection<ScoredResultEntity> ScoredResults { get; set; }
    }

    public class ResultEntityConfiguration : IEntityTypeConfiguration<ResultEntity>
    {
        public void Configure(EntityTypeBuilder<ResultEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.SessionId });

            entity.HasIndex(e => e.SessionId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Session)
                .WithOne(p => p.Result)
                .HasForeignKey<ResultEntity>(d => new { d.LeagueId, d.SessionId });
        }
    }
}
