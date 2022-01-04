﻿using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class CustomIncidentEntity
    {
        public long IncidentId { get; set; }
        public long LeagueId { get; set; }
        public virtual LeagueEntity League { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }
}
