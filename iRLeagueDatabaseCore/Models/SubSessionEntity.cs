using iRLeagueApiCore.Communication.Enums;
using System;

namespace iRLeagueDatabaseCore.Models
{
    public partial class SubSessionEntity : IVersionEntity
    {
        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public int SubSessionNr { get; set; }
        public string Name { get; set; }
        public SessionType SessionType { get; set; }
        public TimeSpan StartOffset { get; set; }
        public TimeSpan Duration { get; set; }
        public int Laps { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

        public virtual SessionEntity ParentSession { get; set; }
        public virtual SubResultEntity SubResult { get; set; }
    }
}
