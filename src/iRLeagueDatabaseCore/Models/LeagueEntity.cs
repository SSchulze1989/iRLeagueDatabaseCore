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
        }

        public virtual ICollection<SeasonEntity> Seasons { get; set; }
    }

    public class LeagueEntityConfiguration : IEntityTypeConfiguration<LeagueEntity>
    {
        public void Configure(EntityTypeBuilder<LeagueEntity> entity)
        {
            entity.HasKey(e => e.Id);

            entity.HasAlternateKey(e => e.Name);

            entity.Property(e => e.Name)
                .HasMaxLength(85);
        }
    }
}
