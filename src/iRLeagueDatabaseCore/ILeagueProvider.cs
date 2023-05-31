using Org.BouncyCastle.Cms;

namespace iRLeagueDatabaseCore;
public interface ILeagueProvider
{
    public long LeagueId { get; }
    public bool HasLeagueName { get; }
    public string LeagueName { get; }
}
