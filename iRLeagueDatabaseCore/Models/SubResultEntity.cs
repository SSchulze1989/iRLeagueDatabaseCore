using iRLeagueApiCore.Communication.Enums;
using System;
using System.Collections.Generic;

namespace iRLeagueDatabaseCore.Models
{
    public partial class SubResultEntity : IVersionEntity
    {
        public SubResultEntity()
        {
            ResultRows = new HashSet<ResultRowEntity>();
        }

        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public int SubSessionNr { get; set; }
        public SimSessionType SimSessionType { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

        public virtual ResultEntity Result { get; set; }
        public virtual SubSessionEntity SubSession { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRows { get; set; }
    }
}