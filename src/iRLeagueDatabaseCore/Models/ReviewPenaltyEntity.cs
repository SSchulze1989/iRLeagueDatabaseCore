namespace iRLeagueDatabaseCore.Models;

public partial class ReviewPenaltyEntity
{
    public long LeagueId { get; set; }
    public long ResultRowId { get; set; }
    public long ReviewId { get; set; }
    //public PenaltyValue Value { get; set; }
    public long? ReviewVoteId { get; set; }

    public virtual ScoredResultRowEntity ResultRow { get; set; }
    public virtual IncidentReviewEntity Review { get; set; }
    public virtual AcceptedReviewVoteEntity ReviewVote { get; set; }
}

public class ReviewPenaltyEntityConfiguration : IEntityTypeConfiguration<ReviewPenaltyEntity>
{
    public void Configure(EntityTypeBuilder<ReviewPenaltyEntity> entity)
    {
        entity.HasKey(e => new { e.LeagueId, e.ResultRowId, e.ReviewId });

        entity.HasIndex(e => new { e.LeagueId, e.ResultRowId });

        entity.HasIndex(e => e.ReviewId);

        entity.HasIndex(e => e.ReviewVoteId);

        //entity.OwnsOne(e => e.Value).ToJson();

        entity.HasOne(d => d.ResultRow)
            .WithMany(p => p.ReviewPenalties)
            .HasForeignKey(d => new { d.LeagueId, d.ResultRowId });

        entity.HasOne(d => d.Review)
            .WithMany(p => p.ReviewPenaltys)
            .HasForeignKey(d => new { d.LeagueId, d.ReviewId })
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.ReviewVote)
            .WithMany(p => p.ReviewPenaltys)
            .HasForeignKey(d => new { d.LeagueId, d.ReviewVoteId })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
