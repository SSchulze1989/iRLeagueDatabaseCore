namespace iRLeagueDatabaseCore.Models;

public partial class StandingConfigs_ResultConfigs
{
    public long LeagueId { get; set; }
    public long StandingConfigId { get; set; }
    public long ResultConfigId { get; set; }

    public virtual StandingConfigurationEntity StandingConfig { get; set; }
    public virtual ResultConfigurationEntity ResultConfig { get; set; }
}
