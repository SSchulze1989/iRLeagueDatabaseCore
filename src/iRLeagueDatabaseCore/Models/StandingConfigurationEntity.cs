namespace iRLeagueDatabaseCore.Models;

public partial class StandingConfigurationEntity : IVersionEntity
{
    public long LeagueId { get; set; }
    public long StandingConfigId { get; set; }

    public string Name { get; set; }
    public ResultKind ResultKind { get; set; }
    public bool UseCombinedResult { get; set; }
    public int WeeksCounted { get; set; }

    public virtual ICollection<ResultConfigurationEntity> ResultConfigurations { get; set; }
    public virtual IEnumerable<StandingEntity> Standings { get; set; }

    #region version
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int Version { get; set; }
    public string CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; }
    public string LastModifiedByUserId { get; set; }
    public string LastModifiedByUserName { get; set; }
    #endregion
}

public sealed class StandingConfigurationEntityConfiguration : IEntityTypeConfiguration<StandingConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<StandingConfigurationEntity> entity)
    {
        entity.HasKey(e => new { e.LeagueId, e.StandingConfigId });

        entity.HasAlternateKey(e => e.StandingConfigId);

        entity.Property(e => e.StandingConfigId)
            .ValueGeneratedOnAdd();

        entity.HasMany(p => p.ResultConfigurations)
            .WithMany(d => d.StandingConfigurations)
            .UsingEntity<StandingConfigs_ResultConfigs>(
                right => right.HasOne(e => e.ResultConfig)
                    .WithMany()
                    .HasForeignKey(e => new { e.LeagueId, e.ResultConfigId }),
                left => left.HasOne(e => e.StandingConfig)
                    .WithMany()
                    .HasForeignKey(e => new { e.LeagueId, e.StandingConfigId }));
    }
}
