using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace iRLeagueDatabaseCore.Models
{
    public class StandingEntity : IVersionEntity
    {
        public StandingEntity()
        {
            StandingRows = new HashSet<StandingRowEntity>();
        }

        public long LeagueId { get; set; }
        public long StandingId { get; set; }
        public long SeasonId { get; set; }
        public long? StandingConfigId { get; set; }
        public long EventId { get; set; }

        public string Name { get; set; }
        public bool IsTeamStanding { get; set; }

        public virtual SeasonEntity Season { get; set; }
        public virtual EventEntity Event { get; set; }
        public virtual ICollection<StandingRowEntity> StandingRows { get; set; }

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

    public class SeasonStandingEntityConfiguration : IEntityTypeConfiguration<StandingEntity>
    {
        public void Configure(EntityTypeBuilder<StandingEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.StandingId });

            entity.HasAlternateKey(e => e.StandingId);

            entity.Property(e => e.StandingId)
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Season)
                .WithMany(p => p.Standings)
                .HasForeignKey(d => new { d.LeagueId, d.SeasonId });

            entity.HasOne(d => d.Event)
                .WithMany()
                .HasForeignKey(d => new { d.LeagueId, d.EventId });
        }
    }
}
