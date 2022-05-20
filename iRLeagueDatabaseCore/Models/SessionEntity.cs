﻿using iRLeagueApiCore.Communication.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class SessionEntity : IVersionEntity
    {
        public SessionEntity()
        {
            IncidentReviews = new HashSet<IncidentReviewEntity>();
            Scorings = new HashSet<ScoringEntity>();
        }

        public long SessionId { get; set; }
        public long LeagueId { get; set; }
        public string Name { get; set; }
        public SessionType SessionType { get; set; }
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
        
        public long? PracticeSubSessionNr { get; set; }
        public long? QualfiyingSubSessionNr { get; set; }
        public long? RaceSubSessionNr { get; set; }
        public string IrSessionId { get; set; }
        public string IrResultLink { get; set; }
        public long ScheduleId { get; set; }

        public virtual ScheduleEntity Schedule { get; set; }
        public virtual ResultEntity Result { get; set; }
        public virtual TrackConfigEntity Track { get; set; }
        public virtual SubSessionEntity Practice { get; set; }
        public virtual SubSessionEntity Qualfiying { get; set; }
        public virtual SubSessionEntity Race { get; set; }
        public virtual ICollection<IncidentReviewEntity> IncidentReviews { get; set; }
        public virtual ICollection<SubSessionEntity> SubSessions { get; set; }
        public virtual ICollection<ScoringEntity> Scorings { get; set; }
    }
}
