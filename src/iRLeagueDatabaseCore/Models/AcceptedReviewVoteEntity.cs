using iRLeagueApiCore.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class AcceptedReviewVoteEntity
    {
        public AcceptedReviewVoteEntity()
        {
            ReviewPenaltys = new HashSet<ReviewPenaltyEntity>();
        }

        public long ReviewVoteId { get; set; }
        public long LeagueId { get; set; }
        public long ReviewId { get; set; }
        public long? MemberAtFaultId { get; set; }
        public long? VoteCategoryId { get; set; }
        public string Description { get; set; }

        public virtual VoteCategoryEntity VoteCategory { get; set; }
        public virtual MemberEntity MemberAtFault { get; set; }
        public virtual IncidentReviewEntity Review { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenaltys { get; set; }
    }

    public class AcceptedReviewVoteEntityConfiguration : IEntityTypeConfiguration<AcceptedReviewVoteEntity>
    {
        public void Configure(EntityTypeBuilder<AcceptedReviewVoteEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ReviewVoteId });

            entity.HasAlternateKey(e => e.ReviewVoteId);

            entity.Property(e => e.ReviewVoteId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.VoteCategoryId);

            entity.HasIndex(e => e.MemberAtFaultId);

            entity.HasIndex(e => e.ReviewId);

            entity.HasOne(d => d.VoteCategory)
                .WithMany(p => p.AcceptedReviewVotes)
                .HasForeignKey(d => d.VoteCategoryId);

            entity.HasOne(d => d.MemberAtFault)
                .WithMany(p => p.AcceptedReviewVotes)
                .HasForeignKey(d => d.MemberAtFaultId);

            entity.HasOne(d => d.Review)
                .WithMany(p => p.AcceptedReviewVotes)
                .HasForeignKey(d => new { d.LeagueId, d.ReviewId });
        }
    }


}
