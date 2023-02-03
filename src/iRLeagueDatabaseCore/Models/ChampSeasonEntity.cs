using iRLeagueApiCore.Common.Models.Reviews;

namespace iRLeagueDatabaseCore.Models;
public partial class ChampSeasonEntity
{
    public ChampSeasonEntity()
    {
        ResultConfigurations = new HashSet<ResultConfigurationEntity>();
        EventResults = new HashSet<ScoredEventResultEntity>();
        Standings = new HashSet<StandingEntity>();
    }

    public long LeagueId { get; set; }
    public long ChampSeasonId { get; set; }
    public long ChampionshipId { get; set; }
    public long SeasonId { get; set; }
    public long? StandingConfigId { get; set; }

    public virtual ChampionshipEntity Championship { get; set; }
    public virtual SeasonEntity Season { get; set; }
    public virtual ICollection<ResultConfigurationEntity> ResultConfigurations { get; set; }
    public virtual StandingConfigurationEntity StandingConfiguration { get; set; }
    public virtual IEnumerable<ScoredEventResultEntity> EventResults { get; set; }
    public virtual IEnumerable<StandingEntity> Standings { get; set; }
}

public sealed class ChampSeasonEntityConfiguration : IEntityTypeConfiguration<ChampSeasonEntity>
{
    public void Configure(EntityTypeBuilder<ChampSeasonEntity> entity)
    {
        entity.ToTable("ChampSeasons");

        entity.HasKey(e => new { e.LeagueId, e.ChampSeasonId });

        entity.HasAlternateKey(e => e.ChampSeasonId);

        entity.Property(e => e.ChampSeasonId)
            .ValueGeneratedOnAdd();

        entity.HasOne(d => d.Championship)
            .WithMany(p => p.ChampSeasons)
            .HasForeignKey(d => new { d.LeagueId, d.ChampionshipId })
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.Season)
            .WithMany(p => p.ChampSeasons)
            .HasForeignKey(d => new { d.LeagueId, d.SeasonId })
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.StandingConfiguration)
            .WithMany(p => p.ChampSeasons)
            .HasForeignKey(d => new { d.LeagueId, d.StandingConfigId })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasMany(d => d.ResultConfigurations)
            .WithMany(p => p.ChampSeasons)
            .UsingEntity<ChampSeasons_ResultConfigurations>(
                right => right.HasOne(e => e.ResultConfig)
                    .WithMany()
                    .HasForeignKey(e => new { e.LeagueId, e.ResultConfigId }),
                left => left.HasOne(e => e.ChampSeason)
                    .WithMany()
                    .HasForeignKey(e => new { e.LeagueId, e.ChampSeasonId }))
            .ToTable("ChampSeasons_ResultConfigs");

        entity.HasMany(p => p.EventResults)
            .WithOne(d => d.ChampSeason)
            .HasForeignKey(d => new { d.LeagueId, d.ChampSeasonId })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasMany(p => p.Standings)
            .WithOne(d => d.ChampSeason)
            .HasForeignKey(d => new { d.LeagueId, d.ChampSeasonId })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
