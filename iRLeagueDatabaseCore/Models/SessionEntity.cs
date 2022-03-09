using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class SessionEntity
    {
        public SessionEntity()
        {
            DriverStatisticRowEntityFirstRaces = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityFirstSessions = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityLastRaces = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityLastSessions = new HashSet<DriverStatisticRowEntity>();
            IncidentReviewEntities = new HashSet<IncidentReviewEntity>();
            InverseParentSession = new HashSet<SessionEntity>();
            Scorings = new HashSet<ScoringEntity>();
        }

        public long SessionId { get; set; }
        public string SessionTitle { get; set; }
        public int SessionType { get; set; }
        public DateTime? Date { get; set; }
        public long? TrackId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long? RaceId { get; set; }
        public int? Laps { get; set; }
        public TimeSpan? PracticeLength { get; set; }
        public TimeSpan? QualyLength { get; set; }
        public TimeSpan? RaceLength { get; set; }
        public string IrSessionId { get; set; }
        public string IrResultLink { get; set; }
        public bool? QualyAttached { get; set; }
        public bool? PracticeAttached { get; set; }
        public long? ScheduleId { get; set; }
        public string Name { get; set; }
        public long? ParentSessionId { get; set; }
        public int SubSessionNr { get; set; }

        public virtual SessionEntity ParentSession { get; set; }
        public virtual ScheduleEntity Schedule { get; set; }
        public virtual ResultEntity ResultEntity { get; set; }
        public virtual TrackConfigEntity Track { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityFirstRaces { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityFirstSessions { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityLastRaces { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityLastSessions { get; set; }
        public virtual ICollection<IncidentReviewEntity> IncidentReviewEntities { get; set; }
        public virtual ICollection<SessionEntity> InverseParentSession { get; set; }
        public virtual ICollection<ScoringEntity> Scorings { get; set; }
    }
}
