using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class IncidentReviewEntity : IVersionEntity
    {
        public IncidentReviewEntity()
        {
            AcceptedReviewVotes = new HashSet<AcceptedReviewVoteEntity>();
            Comments = new HashSet<CommentBaseEntity>();
            InvolvedMembers = new HashSet<MemberEntity>();
            ReviewPenaltys = new HashSet<ReviewPenaltyEntity>();
        }

        public long ReviewId { get; set; }
        public long LeagueId { get; set; }
        public long SessionId { get; set; }
        public string AuthorUserId { get; set; }
        public string AuthorName { get; set; }
        public string IncidentKind { get; set; }
        public string FullDescription { get; set; }
        public string OnLap { get; set; }
        public string Corner { get; set; }
        public TimeSpan TimeStamp { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public string ResultLongText { get; set; }
        public string IncidentNr { get; set; }

        public virtual SessionEntity Session { get; set; }
        public virtual ICollection<AcceptedReviewVoteEntity> AcceptedReviewVotes { get; set; }
        public virtual ICollection<CommentBaseEntity> Comments { get; set; }
        public virtual ICollection<MemberEntity> InvolvedMembers { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenaltys { get; set; }
    }

    public class IncidentReviewEntityConfiguration : IEntityTypeConfiguration<IncidentReviewEntity>
    {
        public void Configure(EntityTypeBuilder<IncidentReviewEntity> entity)
        {
            entity.HasKey(e => e.ReviewId);

            entity.HasIndex(e => e.SessionId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.TimeStamp).HasConversion(new TimeSpanToTicksConverter());

            entity.HasOne(d => d.Session)
                .WithMany(p => p.IncidentReviews)
                .HasForeignKey(d => new { d.LeagueId, d.SessionId });

            entity.HasMany(d => d.InvolvedMembers)
                .WithMany(p => p.InvolvedReviews)
                .UsingEntity(e => e.ToTable("IncidentReviewsInvolvedMembers"));
        }
    }
}
