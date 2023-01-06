using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace iRLeagueDatabaseCore
{
    internal class MigrationLeagueDbContextFactory : IDesignTimeDbContextFactory<LeagueDbContext>
    {
        public LeagueDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("EFCORETOOLSDB")
                ?? throw new InvalidOperationException("No connection string for migration provided. Please set $env:EFCORETOOLSDB");
            var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
            optionsBuilder.UseMySQL(connectionString);

            var dbContext = new LeagueDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}
