using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class SessionBaseEntity
    {
        public SessionBaseEntity()
        {
            DriverStatisticRowEntityFirstRaces = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityFirstSessions = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityLastRaces = new HashSet<DriverStatisticRowEntity>();
            DriverStatisticRowEntityLastSessions = new HashSet<DriverStatisticRowEntity>();
            IncidentReviewEntities = new HashSet<IncidentReviewEntity>();
            InverseParentSession = new HashSet<SessionBaseEntity>();
            ScoringSessions = new HashSet<ScoringSession>();
        }

        public long SessionId { get; set; }
        public string SessionTitle { get; set; }
        public int SessionType { get; set; }
        public DateTime? Date { get; set; }
        public string LocationId { get; set; }
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
        public string Discriminator { get; set; }
        public long? ScheduleId { get; set; }
        public string Name { get; set; }
        public long? ParentSessionId { get; set; }
        public int SubSessionNr { get; set; }

        public virtual SessionBaseEntity ParentSession { get; set; }
        public virtual ScheduleEntity Schedule { get; set; }
        public virtual ResultEntity ResultEntity { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityFirstRaces { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityFirstSessions { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityLastRaces { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRowEntityLastSessions { get; set; }
        public virtual ICollection<IncidentReviewEntity> IncidentReviewEntities { get; set; }
        public virtual ICollection<SessionBaseEntity> InverseParentSession { get; set; }
        public virtual ICollection<ScoringSession> ScoringSessions { get; set; }
    }
}
