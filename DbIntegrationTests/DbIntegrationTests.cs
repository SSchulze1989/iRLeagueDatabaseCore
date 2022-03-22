using System;
using System.Linq;
using System.Transactions;
using Xunit;
using Xunit.Abstractions;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbIntegrationTests
{
    public class DbIntegrationTests
    {
        static IConfiguration _config;

        private static readonly int Seed = 12345;

        public DbIntegrationTests(ITestOutputHelper output)
        {
            output.WriteLine($"Randomizer seed: {Seed}");
        }

        static DbIntegrationTests()
        {
            var random = new Random(Seed);
            _config = ((IConfigurationBuilder)(new ConfigurationBuilder()))
                .AddUserSecrets<DbIntegrationTests>()
                .Build();

            // Setup database
            using (var dbContext = GetTestDatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                PopulateTestDatabase.Populate(dbContext, random);
                dbContext.SaveChanges();
            }
        }

        public static LeagueDbContext  GetTestDatabaseContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
            optionsBuilder.UseMySQL(_config.GetConnectionString("ModelDb"))
                .UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            var dbContext = new LeagueDbContext(optionsBuilder.Options);
            return dbContext;
        }

        [Fact]
        public async void TestPopulate()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                var league = dbContext.Leagues.FirstOrDefault();
                Assert.NotNull(league);
                Assert.Equal("TestLeague", league.Name);
                Assert.Equal(2, league.Seasons.Count());

                // validate structure
                foreach(var season in league.Seasons)
                {
                    Assert.Equal(league, season.League);
                    Assert.Equal(league.LeagueId, season.LeagueId);
                }

                var seasonSchedules = league.Seasons.SelectMany(x => x.Schedules.Select(y => (x, y)));
                foreach((var season, var schedule) in seasonSchedules)
                {
                    Assert.Equal(season, schedule.Season);
                    Assert.Equal(league.LeagueId, schedule.LeagueId);
                }

                // check for result rows
                var scoredResultRow = await dbContext.ScoredResultRows
                    .Include(x => x.ResultRow)
                        .ThenInclude(x => x.Member)
                    .Include(x => x.ResultRow)
                        .ThenInclude(x => x.Result)
                            .ThenInclude(x => x.Session)
                    .FirstOrDefaultAsync();

                Assert.NotNull(scoredResultRow);
                Assert.NotNull(scoredResultRow.ResultRow);
                Assert.NotNull(scoredResultRow.ResultRow.Member);
                Assert.NotNull(scoredResultRow.ResultRow.Result);
                Assert.NotNull(scoredResultRow.ResultRow.Result.Session);
            }
        }

        [Fact]
        public void TestCreateLeague()
        {
            //    using (var tx = new TransactionScope())
            //    {
            using (var dbContext = GetTestDatabaseContext())
                {
                    var league = new LeagueEntity()
                    {
                        Name = "TestLeague2",
                        NameFull = "2nd League for unit testing"
                    };
                    dbContext.Leagues.Add(league);
                    var season = new SeasonEntity()
                    {
                        SeasonName = "TestSeason",
                        CreatedOn = DateTime.Now,
                        CreatedByUserName = "TestUser",
                        CreatedByUserId = "1"
                    };
                    league.Seasons.Add(season);

                    dbContext.SaveChanges();
                }

                using (var dbContext = GetTestDatabaseContext())
                {
                    Assert.Equal(2, dbContext.Leagues.Count());
                    var league = dbContext.Leagues.OrderBy(x => x.LeagueId).Last();
                    Assert.Equal("TestLeague2", league.Name);
                    Assert.Equal(1, league.Seasons.Count);
                    Assert.Equal(league, league.Seasons.First().League);
                }
            //}

            //// clean up after testing

            //using (var dbContext = GetTestDatabaseContext())
            //{
            //    Assert.Equal(1, dbContext.Leagues.Count());
            //}
        }

        [Fact]
        public void TestLazyLoading()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                var league = dbContext.Leagues.First();
                Assert.NotNull(league);
                Assert.Equal(2, league.Seasons.Count);
            }
        }

        [Fact]
        public void TestLazyLoadingDisabled()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                dbContext.ChangeTracker.LazyLoadingEnabled = false;
                var league = dbContext.Leagues.First();
                Assert.NotNull(league);
                Assert.Equal(0, league.Seasons.Count);
            }
        }

        [Fact]
        public void TestEagerLoading()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                dbContext.ChangeTracker.LazyLoadingEnabled = false;
                var league = dbContext.Leagues
                    .Include(e => e.Seasons)
                    .First();
                Assert.NotNull(league);
                Assert.Equal(2, league.Seasons.Count);
            }

            using (var dbContext = GetTestDatabaseContext())
            {
                dbContext.ChangeTracker.LazyLoadingEnabled = false;
                var league = dbContext.Leagues.First();
                dbContext.Seasons.Load();

                Assert.NotNull(league);
                Assert.Equal(2, league.Seasons.Count);
            }
        }

        [Fact]
        public async void TestAddScoring()
        {
            using (var tx = new TransactionScope())
            using (var dbContext = GetTestDatabaseContext())
            {
                var scoring = new ScoringEntity()
                {
                    Name = "TestScoring",
                    ShowResults = true
                };

                var season = await dbContext.Seasons.FirstAsync();
                season.Scorings.Add(scoring);

                await dbContext.SaveChangesAsync();

                Assert.Equal(2, dbContext.Scorings.Count());
                Assert.Contains(dbContext.Scorings, x => x.Name == "TestScoring");
            }
        }

        [Fact]
        public async void TestAddResult()
        {
            using (var tx = new TransactionScope())
            using (var context = GetTestDatabaseContext())
            {
                const int testSessionId = 2;

                var testSession = await context.Sessions
                    .Include(x => x.Result)
                    .SingleAsync(x => x.SessionId == testSessionId);

                var result = new ResultEntity();
                testSession.Result = result;

                await context.SaveChangesAsync();

                var dbResult = await context.Results
                    .Include(x => x.Session)
                    .SingleOrDefaultAsync(x => x.ResultId == testSessionId);

                Assert.NotNull(dbResult);
                Assert.Equal(testSession, dbResult.Session);
            }
        }
    }
}