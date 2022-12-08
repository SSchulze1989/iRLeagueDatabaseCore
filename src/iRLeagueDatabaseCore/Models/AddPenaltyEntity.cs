using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class AddPenaltyEntity
    {
        public long LeagueId { get; set; }
        public long ScoredResultRowId { get; set; }
        public int PenaltyPoints { get; set; }

        public virtual ScoredResultRowEntity ScoredResultRow { get; set; }
    }

    public class AddPenaltyEntityConfiguration : IEntityTypeConfiguration<AddPenaltyEntity>
    {
        public void Configure(EntityTypeBuilder<AddPenaltyEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ScoredResultRowId });

            entity.HasIndex(e => new { e.LeagueId, e.ScoredResultRowId });

            entity.HasOne(d => d.ScoredResultRow)
                .WithOne(p => p.AddPenalty)
                .HasForeignKey<AddPenaltyEntity>(d => new { d.LeagueId, d.ScoredResultRowId })
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
