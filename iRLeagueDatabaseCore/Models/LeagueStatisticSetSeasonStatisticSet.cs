using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class LeagueStatisticSetSeasonStatisticSet
    {
        public long LeagueStatisticSetRefId { get; set; }
        public long SeasonStatisticSetRefId { get; set; }

        public virtual StatisticSetEntity LeagueStatisticSetRef { get; set; }
        public virtual StatisticSetEntity SeasonStatisticSetRef { get; set; }
    }
}
