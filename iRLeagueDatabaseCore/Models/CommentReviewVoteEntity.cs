using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class CommentReviewVoteEntity
    {
        public long ReviewVoteId { get; set; }
        public long CommentId { get; set; }
        public long LeagueId { get; set; }
        public long? MemberAtFaultId { get; set; }
        public long? VoteCategoryId { get; set; }
        public string Description { get; set; }

        public virtual CommentBaseEntity Comment { get; set; }
        public virtual VoteCategoryEntity VoteCategory { get; set; }
        public virtual MemberEntity MemberAtFault { get; set; }
    }

    public class CommentReviewVoteEntityConfiguration : IEntityTypeConfiguration<CommentReviewVoteEntity>
    {
        public void Configure(EntityTypeBuilder<CommentReviewVoteEntity> entity)
        {
            entity.HasKey(e => e.ReviewVoteId);

            entity.HasIndex(e => e.CommentId);

            entity.HasIndex(e => e.VoteCategoryId);

            entity.HasIndex(e => e.MemberAtFaultId);

            entity.HasOne(d => d.Comment)
                .WithMany(p => p.CommentReviewVotes)
                .HasForeignKey(d => d.CommentId);

            entity.HasOne(d => d.VoteCategory)
                .WithMany(p => p.CommentReviewVotes)
                .HasForeignKey(d => d.VoteCategoryId);

            entity.HasOne(d => d.MemberAtFault)
                .WithMany(p => p.CommentReviewVotes)
                .HasForeignKey(d => d.MemberAtFaultId);
        }
    }
}
