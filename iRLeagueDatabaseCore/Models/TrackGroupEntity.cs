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
}
