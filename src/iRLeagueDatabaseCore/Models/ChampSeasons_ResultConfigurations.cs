namespace iRLeagueDatabaseCore.Models;
public partial class ChampSeasons_ResultConfigurations
{
    public long LeagueId { get; set; }
    public long ChampSeasonId { get; set; }
    public long ResultConfigId { get; set; }

    public virtual ChampSeasonEntity ChampSeason { get; set; }
    public virtual ResultConfigurationEntity ResultConfig { get; set; }
}
