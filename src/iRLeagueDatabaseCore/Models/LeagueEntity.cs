using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class LeagueEntity : Revision, IVersionEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public LeagueEntity()
        {
            Seasons = new HashSet<SeasonEntity>();
            ResultConfigs = new HashSet<ResultConfigurationEntity>();
            PointRules = new HashSet<PointRuleEntity>();
            LeagueMembers = new HashSet<LeagueMemberEntity>();
            Teams = new HashSet<TeamEntity>();
        }

        public virtual ICollection<SeasonEntity> Seasons { get; set; }
        public virtual ICollection<ResultConfigurationEntity> ResultConfigs { get; set; }
        public virtual ICollection<PointRuleEntity> PointRules { get; set; }
        public virtual IEnumerable<ScoringEntity> Scorings { get; set; }
        public virtual ICollection<LeagueMemberEntity> LeagueMembers { get; set; }
        public virtual ICollection<TeamEntity> Teams { get; set; }
    }

    public class LeagueEntityConfiguration : IEntityTypeConfiguration<LeagueEntity>
    {
        public void Configure(EntityTypeBuilder<LeagueEntity> entity)
        {
            entity.HasKey(e => e.Id);

            entity.HasAlternateKey(e => e.Name);

            entity.Property(e => e.Name)
                .HasMaxLength(85);

            entity.HasMany(d => d.Scorings)
                .WithOne()
                .HasForeignKey(p => p.LeagueId);
        }
    }
}
