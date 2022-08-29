using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class SeasonEntity : Revision, IVersionEntity
    {
        public SeasonEntity()
        {
            Results = new HashSet<EventResultEntity>();
            ResultTabs = new HashSet<ResultTabEntity>();
            Schedules = new HashSet<ScheduleEntity>();
            Standings = new HashSet<StandingEntity>();
            StatisticSets = new HashSet<StatisticSetEntity>();
        }

        public long SeasonId { get; set; }
        public long LeagueId { get; set; }
        public string SeasonName { get; set; }
        public long? MainScoringScoringId { get; set; }
        public bool HideCommentsBeforeVoted { get; set; }
        public bool Finished { get; set; }
        public DateTime? SeasonStart { get; set; }
        public DateTime? SeasonEnd { get; set; }

        public virtual LeagueEntity League { get; set; }
        public virtual ScoringEntity MainScoring { get; set; }
        public virtual ICollection<EventResultEntity> Results { get; set; }
        public virtual ICollection<ScheduleEntity> Schedules { get; set; }
        public virtual ICollection<StandingEntity> Standings { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSets { get; set; }
        public virtual ICollection<ResultTabEntity> ResultTabs { get; set; }
    }

    public class SeasonEntityConfiguration : IEntityTypeConfiguration<SeasonEntity>
    {
        public void Configure(EntityTypeBuilder<SeasonEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.SeasonId });

            entity.HasAlternateKey(e => e.SeasonId);

            entity.Property(e => e.SeasonId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => new { e.LeagueId, e.MainScoringScoringId });

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.SeasonEnd).HasColumnType("datetime");

            entity.Property(e => e.SeasonStart).HasColumnType("datetime");

            entity.HasOne(e => e.League)
                .WithMany(p => p.Seasons)
                .HasForeignKey(e => e.LeagueId);
        }
    }
}
