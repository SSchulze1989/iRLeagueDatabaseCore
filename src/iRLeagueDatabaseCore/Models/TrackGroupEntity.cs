using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class TrackGroupEntity
    {
        public TrackGroupEntity()
        {
            TrackConfigs = new HashSet<TrackConfigEntity>();
        }

        public long TrackGroupId { get; set; }
        public string TrackName { get; set; }
        public string Location { get; set; }

        public virtual ICollection<TrackConfigEntity> TrackConfigs { get; set; }
    }

    public class TrackGroupEntityConfiguration : IEntityTypeConfiguration<TrackGroupEntity>
    {
        public void Configure(EntityTypeBuilder<TrackGroupEntity> entity)
        {
            entity.HasKey(e => e.TrackGroupId);

            entity.HasMany(d => d.TrackConfigs)
                .WithOne(p => p.TrackGroup)
                .HasForeignKey(d => d.TrackGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
