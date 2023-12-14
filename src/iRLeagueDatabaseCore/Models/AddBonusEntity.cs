
namespace iRLeagueDatabaseCore.Models;
public partial class AddBonusEntity
{
    public long LeagueId { get; set; }
    public long AddBonusId { get; set; }
    public long ScoredResultRowId { get; set; }

    public string Reason { get; set; }
    public double BonusPoints { get; set; }

    public virtual ScoredResultRowEntity ScoredResultRow { get; set; }
}

public sealed class AddBonusEntityConfiguration : IEntityTypeConfiguration<AddBonusEntity>
{
    public void Configure(EntityTypeBuilder<AddBonusEntity> entity)
    {
        entity.HasKey(x => new {x.LeagueId, x.AddBonusId});

        entity.HasAlternateKey(x => x.AddBonusId);

        entity.Property(x => x.AddBonusId)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Reason)
            .HasMaxLength(2048);

        entity.HasOne(x => x.ScoredResultRow)
            .WithMany(x => x.AddBonuses)
            .HasForeignKey(d => new { d.LeagueId, d.ScoredResultRowId })
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
