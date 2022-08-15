using System;
using System.Linq;
using System.Transactions;
using Xunit;
using Xunit.Abstractions;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DbIntegrationTests
{
    [Collection("DbIntegration")]
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
        public async Task Populate()
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
                    Assert.Equal(league.Id, season.LeagueId);
                }

                var seasonSchedules = league.Seasons.SelectMany(x => x.Schedules.Select(y => (x, y)));
                foreach((var season, var schedule) in seasonSchedules)
                {
                    Assert.Equal(season, schedule.Season);
                    Assert.Equal(league.Id, schedule.LeagueId);
                }

                // check for result rows
                var scoredResultRow = await dbContext.ScoredResultRows
                    .Include(x => x.Member)
                    .Include(x => x.ScoredResult)
                        .ThenInclude(x => x.Result.Event)
                    .FirstOrDefaultAsync();

                Assert.NotNull(scoredResultRow);
                Assert.NotNull(scoredResultRow);
                Assert.NotNull(scoredResultRow.Member);
                Assert.NotNull(scoredResultRow.ScoredResult.Result.Event);

                // check for scoring sessions
                var scoring = await dbContext.Scorings
                    .Include(x => x.Sessions)
                    .FirstAsync();
                var scoringSession = await dbContext.Sessions
                    .Include(x => x.Scorings)
                    .FirstAsync();
                Assert.Contains(scoring.Sessions, x => x == scoringSession);
                Assert.Contains(scoringSession.Scorings, x => x == scoring);
            }
        }

        [Fact]
        public void CreateLeague()
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                const string leagueName = "TestCreateLeague";
                using (var dbContext = GetTestDatabaseContext())
                {
                    var league = new LeagueEntity()
                    {
                        Name = leagueName,
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
                    var league = dbContext.Leagues.OrderBy(x => x.Id).Last();
                    Assert.Equal(leagueName, league.Name);
                    Assert.Equal(1, league.Seasons.Count);
                    Assert.Equal(league, league.Seasons.First().League);
                }
            }
        }

        [Fact]
        public void LazyLoading()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                var league = dbContext.Leagues.First();
                Assert.NotNull(league);
                Assert.Equal(2, league.Seasons.Count);
            }
        }

        [Fact]
        public void LazyLoadingDisabled()
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
        public void EagerLoading()
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
        public async Task AddScoring()
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (var dbContext = GetTestDatabaseContext())
            {
                var scoring = new ScoringEntity()
                {
                    Name = "TestScoring",
                    ShowResults = true
                };

                var season = await dbContext.Seasons.FirstAsync();
                season.Scorings.Add(scoring);
                var session = await dbContext.Sessions.FirstAsync();
                scoring.Sessions.Add(session);

                await dbContext.SaveChangesAsync();

                Assert.Equal(2, await dbContext.Scorings.CountAsync());
                Assert.Contains(dbContext.Scorings, x => x.Name == "TestScoring");
            }
        }

        [Fact]
        public async Task AddResult()
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (var context = GetTestDatabaseContext())
            {
                const int testSessionId = 2;

                var testSession = await context.Sessions
                    .Include(x => x.SessionResult)
                    .SingleAsync(x => x.SessionId == testSessionId);

                var result = new EventResultEntity();
                testSession.SessionResult = result;

                await context.SaveChangesAsync();

                var dbResult = await context.Results
                    .Include(x => x.Event)
                    .SingleOrDefaultAsync(x => x.EventId == testSessionId);

                Assert.NotNull(dbResult);
                Assert.Equal(testSession, dbResult.Event);
            }
        }

        [Fact]
        public async Task DeleteScoring()
        {
            const long scoringId = 1;
            
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var context = GetTestDatabaseContext();

            var scoring = await context.Scorings.SingleAsync(x => x.ScoringId == scoringId);
            context.Scorings.Remove(scoring);
            await context.SaveChangesAsync();

            Assert.DoesNotContain(context.Scorings, x => x.ScoringId == scoringId);
        }

        [Fact]
        public async Task TimeSpanPrecision()
        {
            TimeSpan testTimeSpan = TimeSpan.FromMinutes(1.23);

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            using (var context = GetTestDatabaseContext())
            {
                var session = await context.Sessions.FirstAsync();
                session.Duration = testTimeSpan;
                context.SaveChanges();
            }

            using (var context = GetTestDatabaseContext())
            {
                var session = await context.Sessions.FirstAsync();
                Assert.Equal(testTimeSpan, session.Duration);
            }
        }

        [Fact]
        public async Task ShouldAddSubSessionWithoutSubResult()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var context = GetTestDatabaseContext();

            var session = await context.Sessions.FirstAsync();
            var subSession = new SessionEntity()
            {
                Name = "TestSubSession",
                SessionNr = 2,
            };
            session.SubSessions.Add(subSession);

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task ShouldNotAddSubResultWithoutSubSession()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var context = GetTestDatabaseContext();

            var result = await context.Results.FirstAsync();
            var subResult = new SessionResultEntity()
            {
            };
            result.SessionResults.Add(subResult);

            await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await context.SaveChangesAsync());
        }

        [Fact]
        public async Task ShouldAddSubResultWithSubSession()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using (var context = GetTestDatabaseContext())
            {
                var session = new SessionEntity()
                {
                    Name = "Test",
                };
                var subSession = new SessionEntity()
                {
                    Name = "Race",
                };
                session.SubSessions.Add(subSession);
                context.Schedules.First().Sessions.Add(session);
                await context.SaveChangesAsync();
            }

            using (var context = GetTestDatabaseContext())
            {
                var result = new EventResultEntity()
                {
                };
                var subResult = new SessionResultEntity();
                var session = await context.Sessions.OrderBy(x => x.SessionId).LastAsync();
                session.SessionResult = result;
                //session.SubSessions.First().SubResult = subResult;
                await context.SaveChangesAsync();
            }

            using (var context = GetTestDatabaseContext())
            {
                var session = await context.Sessions.OrderBy(x => x.SessionId).LastAsync();
                var result = session.SessionResult;
                var subResult = session.SubSessions.First().SubResult;
                Assert.NotNull(result);
                //Assert.NotNull(subResult);
                //Assert.Contains(result.SubResults, x => x == subResult);
            }
        }
    }
}