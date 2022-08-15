using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class ResultTabEntity : IVersionEntity
    {
        public ResultTabEntity()
        {
            ScoredEventResults = new HashSet<ScoredEventResultEntity>();
            ResultConfigurations = new HashSet<ResultConfigurationEntity>();
        }

        public long LeagueId { get; set; }
        public long ResultTabId { get; set; }
        public long SeasonId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual SeasonEntity Season { get; set; }
        public virtual ICollection<ScoredEventResultEntity> ScoredEventResults { get; set; }
        public virtual ICollection<ResultConfigurationEntity> ResultConfigurations { get; set; }

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

    public class ResultTabEntityConfiguration : IEntityTypeConfiguration<ResultTabEntity>
    {
        public void Configure(EntityTypeBuilder<ResultTabEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ResultTabId });

            entity.HasAlternateKey(e => e.ResultTabId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.ResultTabId)
                .ValueGeneratedOnAdd();
        }
    }
}
