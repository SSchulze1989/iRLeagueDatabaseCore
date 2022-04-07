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
}
