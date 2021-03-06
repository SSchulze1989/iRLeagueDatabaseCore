using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class MemberEntity
    {
        public MemberEntity()
        {
            AcceptedReviewVotes = new HashSet<AcceptedReviewVoteEntity>();
            CommentReviewVotes = new HashSet<CommentReviewVoteEntity>();
            DriverStatisticRows = new HashSet<DriverStatisticRowEntity>();
            InvolvedReviews = new HashSet<IncidentReviewEntity>();
            ResultRows = new HashSet<ResultRowEntity>();
            CleanestDriverResults = new HashSet<ScoredResultEntity>();
            FastestAvgLapResults = new HashSet<ScoredResultEntity>();
            FastestLapResults = new HashSet<ScoredResultEntity>();
            FastestQualyLapResults = new HashSet<ScoredResultEntity>();
            HardChargerResults = new HashSet<ScoredResultEntity>();
            StatisticSets = new HashSet<StatisticSetEntity>();
        }

        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string IRacingId { get; set; }
        public string DanLisaId { get; set; }
        public string DiscordId { get; set; }

        public virtual ICollection<AcceptedReviewVoteEntity> AcceptedReviewVotes { get; set; }
        public virtual ICollection<CommentReviewVoteEntity> CommentReviewVotes { get; set; }
        public virtual ICollection<DriverStatisticRowEntity> DriverStatisticRows { get; set; }
        public virtual ICollection<IncidentReviewEntity> InvolvedReviews { get; set; }
        public virtual ICollection<ResultRowEntity> ResultRows { get; set; }
        public virtual ICollection<ScoredResultEntity> CleanestDriverResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestAvgLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> FastestQualyLapResults { get; set; }
        public virtual ICollection<ScoredResultEntity> HardChargerResults { get; set; }
        public virtual ICollection<StatisticSetEntity> StatisticSets { get; set; }
    }

    public class MemberEntityConfiguration : IEntityTypeConfiguration<MemberEntity>
    {
        public void Configure(EntityTypeBuilder<MemberEntity> entity)
        {
            entity.HasKey(e => e.Id);
        }
    }
}
