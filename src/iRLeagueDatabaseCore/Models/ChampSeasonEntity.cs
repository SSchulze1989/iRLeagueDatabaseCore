namespace iRLeagueDatabaseCore.Models;
public partial class ChampSeasonEntity
{
    public long LeagueId { get; set; }
    public long ChampionShipId { get; set; }
    public long SeasonId { get; set; }

    public virtual ICollection<ResultConfigurationEntity> ResultConfigurations { get; set; }
    public virtual StandingConfigurationEntity StandingConfiguration { get; set; }
}
