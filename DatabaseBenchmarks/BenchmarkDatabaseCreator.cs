using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBenchmarks
{
    public class BenchmarkDatabaseCreator
    {
        private static IConfiguration Configuration { get; }
        private static readonly int Seed = 12345;

        static BenchmarkDatabaseCreator()
        {
            Configuration = ((IConfigurationBuilder)new ConfigurationBuilder())
                .AddUserSecrets<BenchmarkDatabaseCreator>()
                .Build();

            var random = new Random(Seed);
        }

        public static LeagueDbContext CreateStaticDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
            var connectionString = Configuration.GetConnectionString("BenchmarkDb");

            // use in memory database when no connection string present
            optionsBuilder.UseMySQL(connectionString);

            var dbContext = new LeagueDbContext(optionsBuilder.Options);
            return dbContext;
        }

        public static async Task PopulateBenchmarkDatabase(LeagueDbContext context, Random random = null)
        {
            if (random == null)
            {
                random = new Random(Seed);
            }

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // generate members and tracks first
            var tracks = new List<TrackGroupEntity>();
            var tracksCount = 100;
            for (int i=0; i<tracksCount; i++)
            {
                tracks.Add(CreateRandomTrackGroup(random));
            }
            var trackConfigs = tracks.SelectMany(x => x.TrackConfigs);

            context.TrackGroups.AddRange(tracks);
            await context.SaveChangesAsync();
            
            var members = new List<MemberEntity>();
            var membersCount = 1000;
            for(int i=0; i<membersCount; i++)
            {
                members.Add(CreateRandomMember(random));
            }

            context.Members.AddRange(members);
            await context.SaveChangesAsync();

            // generate leagues, seasons, schedules and scorings
            var leaguesList = new List<LeagueEntity>();
            var leagueCount = 10;
            for(int i=0; i<leagueCount; i++)
            {
                leaguesList.Add(CreateRandomLeague(random));
            }
            context.Leagues.AddRange(leaguesList);
            await context.SaveChangesAsync();
            var leagues = context.Leagues;

            foreach (var league in leagues)
            {
                //var seasonsCount = random.Next(9) + 1;
                var seasonsCount = 5;
                for (int i=0; i<seasonsCount; i++)
                {
                    league.Seasons.Add(CreateRandomSeason(random));
                }
            }
            await context.SaveChangesAsync();
            var seasons = context.Seasons;

            foreach(var season in seasons)
            {
                //var scheduleCount = random.Next(3) + 1;
                var scheduleCount = 1;
                for (int i=0; i<scheduleCount; i++)
                {
                    season.Schedules.Add(CreateRandomSchedule(random));
                }
                //var scoringCount = random.Next(3) + 1;
                var scoringCount = 1;
                for (int i=0; i<scoringCount; i++)
                {
                    season.Scorings.Add(CreateRandomScoring(random));
                }
                //randomly assign scorings to schedules
                foreach(var schedule in season.Schedules)
                {
                    foreach(var scoring in season.Scorings)
                    {
                        //if (random.Next(3) != 0)
                        //{
                            schedule.Scorings.Add(scoring);
                            scoring.ConnectedSchedule = schedule;
                        //}
                    }
                }
            }
            await context.SaveChangesAsync();
            var schedules = context.Schedules;
            var scorings = context.Scorings;

            foreach(var schedule in schedules)
            {
                //var sessionCount = random.Next(10) + 5;
                var sessionCount = 10;
                for (int i=0; i<sessionCount; i++)
                {
                    var session = CreateRandomSession(random, trackConfigs);
                    schedule.Sessions.Add(session);
                    session.Schedule = schedule;
                }
            }
            await context.SaveChangesAsync();
            var sessions = context.Sessions;

            // create raw results for each session
            foreach(var session in sessions)
            {
                session.Result = CreateRandomResult(random, members);
            }
            var resultsDebug = sessions.SelectMany(x => x.Result.ResultRows).ToList();
            await context.SaveChangesAsync();
            var results = context.Results;

            // create scored result for each scoring + attached schedule session
            foreach(var scoring in scorings.Where(x => x.ConnectedSchedule != null))
            {
                var scheduleSessions = scoring.ConnectedSchedule.Sessions;
                foreach(var session in scheduleSessions)
                {
                    session.Scorings.Add(scoring);
                    scoring.Sessions.Add(session);
                    if (session.Result == null)
                    {
                        continue;
                    }

                    var scoredResult = CreateRandomScoredResult(random, session.Result, scoring);
                    scoring.ScoredResults.Add(scoredResult);
                    session.Result.ScoredResults.Add(scoredResult);
                }
            }

            // save that motherfucker
            await context.SaveChangesAsync();
        }

        private static MemberEntity CreateRandomMember(Random random)
        {
            return new MemberEntity()
            {
                Firstname = GetRandomName(random),
                Lastname = GetRandomName(random),
                IRacingId = GetRandomIracingId(random)
            };
        }

        private static LeagueEntity CreateRandomLeague(Random random)
        {
            return new LeagueEntity()
            {
                Name = GetRandomName(random),
                NameFull = GetRandomName(random)
            };
        }

        private static SeasonEntity CreateRandomSeason(Random random)
        {
            return new SeasonEntity()
            {
                SeasonName = GetRandomName(random)
            };
        }

        private static ScheduleEntity CreateRandomSchedule(Random random)
        {
            return new ScheduleEntity()
            {
                Name = GetRandomName(random)
            };
        }

        private static SessionEntity CreateRandomSession(Random random, IEnumerable<TrackConfigEntity> tracks)
        {
            return new SessionEntity()
            {
                Name = GetRandomName(random),
                SessionTitle = GetRandomName(random),
                Date = GetRandomDateTime(random),
                Duration = TimeSpan.FromHours(1),
                Track = tracks.ElementAt(random.Next(tracks.Count())),
            };
        }

        private static TrackConfigEntity CreateRandomTrackConfig(Random random)
        {
            return new TrackConfigEntity()
            {
                ConfigName = GetRandomName(random),
                ConfigType = GetRandomEnumValue<ConfigType>(random),
                LengthKm = random.NextDouble() * 3.0 + 1.0
            };
        }

        private static TrackGroupEntity CreateRandomTrackGroup(Random random)
        {
            var group = new TrackGroupEntity()
            {
                TrackName = GetRandomName(random),
                Location = GetRandomName(random),
            };
            var configsCount = random.Next(4) + 1;
            for (int i=0; i<4; i++)
            {
                group.TrackConfigs.Add(CreateRandomTrackConfig(random));
            }
            return group;
        }

        private static ScoringEntity CreateRandomScoring(Random random)
        {
            return new ScoringEntity()
            {
                Name = GetRandomName(random),
                ShowResults = true
            };
        }

        private static ResultEntity CreateRandomResult(Random random, IEnumerable<MemberEntity> members)
        {
            var result = new ResultEntity();
            result.IRSimSessionDetails = CreateRandomSessionDetails(random);
            var rowsCount = random.Next(40) + 10;
            var membersArray = members.ToArray();
            random.Shuffle(membersArray);
            for(int i=0; i<rowsCount; i++)
            {
                result.ResultRows.Add(CreateRandomResultRow(random, membersArray.ElementAt(i)));
            }
            return result;
        }

        private static ResultRowEntity CreateRandomResultRow(Random random, MemberEntity member)
        {
            return new ResultRowEntity()
            {
                Member = member,
                PositionChange = random.Next(50),
                StartPosition = random.Next(50),
                FinishPosition = random.Next(50),
            };
        }

        private static ScoredResultEntity CreateRandomScoredResult(Random random, ResultEntity result, ScoringEntity scoring)
        {
            var scoredResult = new ScoredResultEntity();
            var rowsCount = result.ResultRows.Count();
            for (int i=0; i<rowsCount; i++)
            {
                var resultRow = result.ResultRows.ElementAt(i);
                var scoredResultRow = CreateRandomScoredResultRow(random, resultRow);
                scoredResult.ScoredResultRows.Add(scoredResultRow);
                resultRow.ScoredResultRows.Add(scoredResultRow);
            }
            return scoredResult;
        }

        private static ScoredResultRowEntity CreateRandomScoredResultRow(Random random, ResultRowEntity resultRow)
        {
            return new ScoredResultRowEntity()
            {
                ResultRow = resultRow,
                FinalPosition = random.Next(50),
                RacePoints = random.Next(50),
                BonusPoints = random.Next(10),
                FinalPositionChange = random.Next(50)
            };
        }

        private static IRSimSessionDetailsEntity CreateRandomSessionDetails(Random random)
        {
            return new IRSimSessionDetailsEntity();
        }

        private static string GetRandomName(Random random)
        {
            var minLen = 3;
            var len = random.Next(10) + minLen;
            char[] name = new char[len];
            char[] characters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ßÄÜÖäüö".ToCharArray();

            for (int i = 0; i < len; i++)
            {
                var offset = random.Next(characters.Length);
                name[i] = characters[offset];
            }
            return new string(name);
        }

        private static string GetRandomIracingId(Random random)
        {
            var len = 6;
            char[] id = new char[len];
            for (int i = 0; i < len; i++)
            {
                id[i] = (char)('0' + random.Next(10));
            }
            return new string(id);
        }

        private static DateTime GetRandomDateTime(Random random)
        {
            var baseDate = DateTime.Today;
            var addDays = random.Next(365);
            var addHours = random.Next(24);
            var addMinutes = random.Next(60);
            return baseDate
                .AddDays(addDays)
                .AddHours(addHours)
                .AddMinutes(addMinutes);
        }

        private static T GetRandomEnumValue<T>(Random random) where T : Enum
        {
            var values = (IEnumerable<T>)Enum.GetValues(typeof(T));
            return values.ElementAt(random.Next(values.Count()));
        }
    }

    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
