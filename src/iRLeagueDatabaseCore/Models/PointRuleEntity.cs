using iRLeagueApiCore.Common.Enums;
using iRLeagueDatabaseCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class PointRuleEntity : IVersionEntity
    {
        public long LeagueId { get; set; }
        public long PointRuleId { get; set; }

        public string Name { get; set; }
        public ICollection<int> PointsPerPlace { get; set; }
        public IDictionary<string, int> BonusPoints { get; set; }
        public int MaxPoints { get; set; }
        public int PointDropOff { get; set; }
        public ICollection<SortOptions> PointsSortOptions { get; set; }
        public ICollection<SortOptions> FinalSortOptions { get; set; }

        public virtual LeagueEntity League { get; set; }
        public virtual ICollection<ResultsFilterOptionEntity> ResultsFilters { get; set; }

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

    public class PointsRuleEntityConfiguration : IEntityTypeConfiguration<PointRuleEntity>
    {
        public void Configure(EntityTypeBuilder<PointRuleEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.PointRuleId });

            entity.HasAlternateKey(e => e.PointRuleId);

            entity.Property(e => e.PointRuleId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn)
                .HasColumnType("datetime");

            entity.Property(e => e.PointsPerPlace)
                .HasConversion(new CollectionToStringConverter<int>(), new ValueComparer<ICollection<int>>(true));

            entity.Property(e => e.BonusPoints)
                .HasConversion(new DictionaryToStringConverter<string, int>(), new ValueComparer<IDictionary<string,int>>(true));

            entity.Property(e => e.PointsSortOptions)
                .HasConversion(new CollectionToStringConverter<SortOptions>(), new ValueComparer<ICollection<SortOptions>>(true));

            entity.Property(e => e.FinalSortOptions)
                .HasConversion(new CollectionToStringConverter<SortOptions>(), new ValueComparer<ICollection<SortOptions>>(true));

            entity.HasOne(d => d.League)
                .WithMany(p => p.PointRules)
                .HasForeignKey(d => d.LeagueId);
        }
    }
}
