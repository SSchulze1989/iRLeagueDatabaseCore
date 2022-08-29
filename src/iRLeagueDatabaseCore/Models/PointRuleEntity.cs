using iRLeagueDatabaseCore.Converters;
using Microsoft.EntityFrameworkCore;
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
        public long PointsRuleId { get; set; }

        public string Name { get; set; }
        public ICollection<int> PointsPerPlace { get; set; }
        public IDictionary<string, int> BonusPoints { get; set; }
        public int MaxPoints { get; set; }
        public int PointDropOff { get; set; }

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
            entity.HasKey(e => new { e.LeagueId, e.PointsRuleId });

            entity.HasAlternateKey(e => e.PointsRuleId);

            entity.Property(e => e.PointsRuleId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.PointsPerPlace).HasConversion(new CollectionToStringConverter<int>());

            entity.Property(e => e.BonusPoints).HasConversion(new DictionaryToStringConverter<string, int>());
        }
    }
}
