using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultRowEntity : ResultRowBase
    {
        public ResultRowEntity()
        {
        }

        public long ResultRowId { get; set; }
        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public long SubSessionId { get; set; }
        public long MemberId { get; set; }
        public long? TeamId { get; set; }
        public bool PointsEligible { get; set; }


        public virtual MemberEntity Member { get; set; }
        public virtual SubResultEntity SubResult { get; set; }
        public virtual TeamEntity Team { get; set; }
    }

    public class ResultRowEntityConfiguration : IEntityTypeConfiguration<ResultRowEntity>
    {
        public void Configure(EntityTypeBuilder<ResultRowEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ResultRowId });
            entity.HasAlternateKey(e => e.ResultRowId);

            entity.Property(e => e.ResultRowId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.MemberId);

            entity.HasIndex(e => new { e.LeagueId, e.SessionId, e.SubSessionId });

            entity.HasIndex(e => e.TeamId);

            entity.Property(e => e.QualifyingTimeAt).HasColumnType("datetime");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.ResultRows)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SubResult)
                .WithMany(p => p.ResultRows)
                .HasForeignKey(d => new { d.LeagueId, d.SessionId, d.SubSessionId });

            entity.HasOne(d => d.Team)
                .WithMany(p => p.ResultRows)
                .HasForeignKey(d => d.TeamId);
        }
    }
}
