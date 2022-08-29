using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class CommentBaseEntity : IVersionEntity
    {
        public CommentBaseEntity()
        {
            CommentReviewVotes = new HashSet<CommentReviewVoteEntity>();
            InverseReplyToComment = new HashSet<CommentBaseEntity>();
        }

        public long CommentId { get; set; }
        public long LeagueId { get; set; }
        public DateTime? Date { get; set; }
        public string AuthorUserId { get; set; }
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public long? ReplyToCommentId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public long? ReviewId { get; set; }

        public virtual CommentBaseEntity ReplyToComment { get; set; }
        public virtual IncidentReviewEntity Review { get; set; }
        public virtual ICollection<CommentReviewVoteEntity> CommentReviewVotes { get; set; }
        public virtual ICollection<CommentBaseEntity> InverseReplyToComment { get; set; }
    }

    public class CommentBaseEntityConfiguation : IEntityTypeConfiguration<CommentBaseEntity>
    {
        public void Configure(EntityTypeBuilder<CommentBaseEntity> entity)
        {
            entity.HasKey(e => e.CommentId);

            entity.HasIndex(e => e.ReplyToCommentId);

            entity.HasIndex(e => e.ReviewId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ReplyToComment)
                .WithMany(p => p.InverseReplyToComment)
                .HasForeignKey(d => d.ReplyToCommentId);

            entity.HasOne(d => d.Review)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
