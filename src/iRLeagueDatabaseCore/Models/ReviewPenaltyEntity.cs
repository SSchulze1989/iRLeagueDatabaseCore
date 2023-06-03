using iRLeagueApiCore.Common.Converters;
using System.Text.Json;

namespace iRLeagueDatabaseCore.Models;

public partial class ReviewPenaltyEntity
{
    public long LeagueId { get; set; }
    public long ResultRowId { get; set; }
    public long ReviewId { get; set; }
    public PenaltyValue Value { get; set; }
    public long? ReviewVoteId { get; set; }

    public virtual ScoredResultRowEntity ResultRow { get; set; }
    public virtual IncidentReviewEntity Review { get; set; }
    public virtual AcceptedReviewVoteEntity ReviewVote { get; set; }
}

public class ReviewPenaltyEntityConfiguration : IEntityTypeConfiguration<ReviewPenaltyEntity>
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        Converters = { new JsonTimeSpanToTicksConverter() }
    };

    public void Configure(EntityTypeBuilder<ReviewPenaltyEntity> entity)
    {
        entity.HasKey(e => new { e.LeagueId, e.ResultRowId, e.ReviewId });

        entity.HasIndex(e => new { e.LeagueId, e.ResultRowId });

        entity.HasIndex(e => e.ReviewId);

        entity.HasIndex(e => e.ReviewVoteId);

        entity.Property(e => e.Value)
            .HasColumnType("json")
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<PenaltyValue>(v, jsonOptions));

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
