using iRLeagueApiCore.Common.Enums;
using iRLeagueDatabaseCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ResultsFilterOptionEntity : IVersionEntity
    {
        public ResultsFilterOptionEntity()
        {
            FilterValues = new HashSet<string>();
        }

        public long LeagueId { get; set; }
        public long ResultsFilterId { get; set; }
        public long? ScoringId { get; set; }
        public long? PointRuleId { get; set; }
        public string ResultsFilterType { get; set; }
        public string ColumnPropertyName { get; set; }
        public ComparatorType Comparator { get; set; }
        public bool Include { get; set; }
        public virtual ICollection<string> FilterValues { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

        public virtual ScoringEntity Scoring { get; set; }
        public virtual PointRuleEntity PointRule { get; set; }
    }

    public class ResultsFilterOptionEntityConfiguration : IEntityTypeConfiguration<ResultsFilterOptionEntity>
    {
        public void Configure(EntityTypeBuilder<ResultsFilterOptionEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ResultsFilterId });

            entity.HasAlternateKey(e => e.ResultsFilterId);

            entity.Property(e => e.ResultsFilterId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.ScoringId);

            entity.HasIndex(e => e.PointRuleId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.FilterValues)
                .HasConversion(new CollectionToStringConverter<string>(), new ValueComparer<ICollection<string>>(true));

            entity.HasOne(d => d.Scoring)
                .WithMany(p => p.ResultsFilterOptions)
                .HasForeignKey(d => new { d.LeagueId, d.ScoringId })
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasOne(d => d.PointRule)
                .WithMany(p => p.ResultsFilters)
                .HasForeignKey(d => new { d.LeagueId, d.PointRuleId })
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
