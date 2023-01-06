using FluentAssertions;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;
using Xunit.Abstractions;

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
                dbContext.Database.Migrate();

                PopulateTestDatabase.Populate(dbContext, random);
                dbContext.SaveChanges();
            }
        }

        public static LeagueDbContext GetTestDatabaseContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
            optionsBuilder.UseMySQL(_config.GetConnectionString("ModelDb"))
                .UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            var dbContext = new LeagueDbContext(optionsBuilder.Options);
            return dbContext;
        }

        [Fact]
        public void Populate()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                var league = dbContext.Leagues.FirstOrDefault();
                Assert.NotNull(league);
                Assert.Equal("TestLeague", league.Name);
                Assert.Equal(2, league.Seasons.Count());

                // validate structure
                foreach (var season in league.Seasons)
                {
                    Assert.Equal(league, season.League);
                    Assert.Equal(league.Id, season.LeagueId);
                }

                var seasonSchedules = league.Seasons.SelectMany(x => x.Schedules.Select(y => (x, y)));
                foreach ((var season, var schedule) in seasonSchedules)
                {
                    Assert.Equal(season, schedule.Season);
                    Assert.Equal(league.Id, schedule.LeagueId);
                }
            }
        }

        [Fact]
        public async Task DeleteLeage()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var dbContext = GetTestDatabaseContext();

            var league = await dbContext.Leagues
                .FirstAsync();
            dbContext.Leagues.Remove(league);
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
        public async Task LeagueShouldHaveScorings()
        {
            using var context = GetTestDatabaseContext();

            var league = await context.Leagues
                .Include(x => x.Scorings)
                .FirstAsync();
            Assert.NotEmpty(league.Scorings);
        }

        [Fact]
        public async Task ShouldAddFilterToConfiguration()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using (var context = GetTestDatabaseContext())
            {
                var filterOption = new FilterOptionEntity();
                var config = await context.ResultConfigurations.FirstAsync();
                config.PointFilters.Add(filterOption);
                await context.SaveChangesAsync();
            }

            using (var context = GetTestDatabaseContext())
            {
                var config = await context.ResultConfigurations.FirstAsync();
                Assert.NotEmpty(config.PointFilters);
            }
        }

        [Fact]
        public async Task ShouldCascadeDeleteFilter()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var context = GetTestDatabaseContext();

            var filterOption = new FilterOptionEntity();
            var config = await context.ResultConfigurations.FirstAsync();
            config.PointFilters.Add(filterOption);
            await context.SaveChangesAsync();

            config.PointFilters.Remove(filterOption);
            var filterOptionEntry = context.Entry(filterOption);
            await context.SaveChangesAsync();

            Assert.Equal(EntityState.Detached, filterOptionEntry.State);
        }

        [Fact]
        public async Task ShouldSetFilterValues()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var context = GetTestDatabaseContext();

            var filterOption = new FilterOptionEntity()
            {
                Conditions = new[] {
                    new FilterConditionEntity() { FilterValues = new[] { "Value1", "Value2" } },
                },
            };
            var config = await context.ResultConfigurations.FirstAsync();
            config.PointFilters.Add(filterOption);
            await context.SaveChangesAsync();
            var testFilterOption = await context.FilterOptions
                .SingleAsync(x => x.FilterOptionId == filterOption.FilterOptionId);

            testFilterOption.Conditions.First().FilterValues.Should().BeEquivalentTo(filterOption.Conditions.First().FilterValues);
        }
    }
}