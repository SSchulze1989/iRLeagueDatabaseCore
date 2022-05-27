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
        private static readonly int leagueCount = 10;
        private static readonly int seasonCount = 5;
        private static readonly int memberCount = 1000;
        private static readonly int trackCount = 100;
        private static readonly int scheduleCount = 2;
        private static readonly int scoringCount = 2;
        private static readonly int sessionCount = 12;

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

        public static async Task PopulateBenchmarkDatabase(Random random = null)
        {
            if (random == null)
            {
                random = new Random(Seed);
            }

            using (var context = CreateStaticDbContext())
            {
                Console.Write("Creating database ... ");
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                Console.Write("Finished\n");

                Console.Write("Creating Tracks ... ");
                // generate members and tracks first
                List<TrackConfigEntity> trackConfigs;
                List<MemberEntity> members;
                var tracks = new List<TrackGroupEntity>();
                for (int i = 0; i < trackCount; i++)
                {
                    tracks.Add(CreateRandomTrackGroup(random));
                }
                trackConfigs = tracks.SelectMany(x => x.TrackConfigs)
                    .ToList();

                context.TrackGroups.AddRange(tracks);
                await context.SaveChangesAsync();

                members = new List<MemberEntity>();
                for (int i = 0; i < memberCount; i++)
                {
                    members.Add(CreateRandomMember(random));
                }

                context.Members.AddRange(members);
                await context.SaveChangesAsync();
                Console.Write("Finished!\n");
            }

            for (int k=0; k<leagueCount; k++)
            {
                using var context = CreateStaticDbContext();
                var trackConfigs = context.TrackConfigs.ToList();
                var members = context.Members.ToList();

                Console.Write($"Creating League #{k}\n");
                var league = CreateRandomLeague(random);
                
                List<SeasonEntity> seasons;
                context.Leagues.Add(league);
                await context.SaveChangesAsync();

                Console.Write("- Creating seasons ... ");
                var leagueId = league.Id;
                for (int i = 0; i < seasonCount; i++)
                {
                    league.Seasons.Add(CreateRandomSeason(random));
                }
                await context.SaveChangesAsync();
                seasons = context.Seasons.Where(x => x.LeagueId == leagueId).ToList();
                Console.Write("Finished!\n");

                Console.Write("- Creating Schedules&Scorings ... ");
                List<ScheduleEntity> schedules;
                List<ScoringEntity> scorings;
                foreach (var season in seasons)
                {
                    //var scheduleCount = random.Next(3) + 1;
                    for (int i = 0; i < scheduleCount; i++)
                    {
                        season.Schedules.Add(CreateRandomSchedule(random));
                    }
                    //var scoringCount = random.Next(3) + 1;
                    for (int i = 0; i < scoringCount; i++)
                    {
                        season.Scorings.Add(CreateRandomScoring(random));
                    }
                    //randomly assign scorings to schedules
                    foreach (var schedule in season.Schedules)
                    {
                        foreach (var scoring in season.Scorings)
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
                schedules = context.Schedules.Where(x => x.LeagueId == leagueId).ToList();
                scorings = context.Scorings.Where(x => x.LeagueId == leagueId).ToList();
                Console.Write("Finished!\n");

                Console.Write("- Creating Sessions ... ");
                List<SessionEntity> sessions;
                foreach (var schedule in schedules)
                {
                    //var sessionCount = random.Next(10) + 5;
                    for (int i = 0; i < sessionCount; i++)
                    {
                        var session = CreateRandomSession(random, trackConfigs);
                        schedule.Sessions.Add(session);
                        session.Schedule = schedule;
                    }
                }
                await context.SaveChangesAsync();
                sessions = context.Sessions.Where(x => x.LeagueId == leagueId).ToList();
                Console.Write("Finished!\n");

                Console.Write("- Creating Results ... ");
                List<ResultEntity> results;
                // create raw results for each session
                foreach (var session in sessions)
                {
                    var result = CreateRandomResult(random);
                    session.Result = result;
                }
                await context.SaveChangesAsync();
                results = context.Results.Where(x => x.LeagueId == leagueId).ToList();

                foreach (var result in results)
                {
                    var details = CreateRandomSessionDetails(random);
                    details.Session = result.Session;
                    foreach (var subSession in result.Session.SubSessions)
                    {
                        var subResult = CreateRandomSubResult(random, members, details);
                        result.SubResults.Add(subResult);
                        subSession.SubResult = subResult;
                    }
                }
                await context.SaveChangesAsync();
                Console.Write("Finished!\n");

                Console.Write("- Creating ScoredResults ... ");
                List<ScoredResultEntity> scoredResults;
                // create scored result for each scoring + attached schedule session
                foreach (var scoring in scorings.Where(x => x.ConnectedSchedule != null))
                {
                    var scheduleSessions = scoring.ConnectedSchedule.Sessions;
                    foreach (var session in scheduleSessions)
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
                scoredResults = context.ScoredResults.Where(x => x.LeagueId == leagueId).ToList();
                Console.Write("Finished!\n");
                Console.WriteLine("");
            }

            using (var context = CreateStaticDbContext())
            {
                Console.Write($"Database created with\n" +
                    $"- {leagueCount} leagues\n" +
                    $"- {memberCount} members" +
                    $"- {context.TrackConfigs.Count()} track configs" +
                    $"- {context.Seasons.Count()} seasons\n" +
                    $"- {context.Schedules.Count()} schedules\n" +
                    $"- {context.Scorings.Count()} scorings\n" +
                    $"- {context.Sessions.Count()} sessions with\n" +
                    $"-   {context.SubSessions.Count()} subsessions\n" +
                    $"- {context.Results.Count()} results with\n" +
                    $"-   {context.SubResults.Count()} subresults\n" +
                    $"-   {context.ResultRows.Count()} result rows\n" +
                    $"- {context.ScoredResults.Count()} scored results with\n" +
                    $"-   {context.ScoredResultRows.Count()} scored result rows\n");
            }
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
            var subSession = new SubSessionEntity()
            {
                Name = "Race",
            };
            var session = new SessionEntity()
            {
                Name = GetRandomName(random),
                Date = GetRandomDateTime(random),
                Duration = TimeSpan.FromHours(1),
                Track = tracks.ElementAt(random.Next(tracks.Count())),
            };
            session.SubSessions.Add(subSession);
            return session;
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

        private static ResultEntity CreateRandomResult(Random random)
        {
            var result = new ResultEntity();
            return result;
        }

        private static SubResultEntity CreateRandomSubResult(Random random, IEnumerable<MemberEntity> members, IRSimSessionDetailsEntity details)
        {
            var subResult = new SubResultEntity();
            var rowsCount = random.Next(40) + 10;
            var membersArray = members.ToArray();
            random.Shuffle(membersArray);
            for (int i = 0; i < rowsCount; i++)
            {
                subResult.ResultRows.Add(CreateRandomResultRow(random, membersArray.ElementAt(i)));
            }
            subResult.IRSimSessionDetails = details;
            return subResult;
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
            var subResult = result.SubResults.FirstOrDefault();
            if (subResult == null)
            {
                return scoredResult;
            }
            var rowsCount = subResult.ResultRows.Count();
            for (int i=0; i<rowsCount; i++)
            {
                var resultRow = subResult.ResultRows.ElementAt(i);
                var scoredResultRow = CreateRandomScoredResultRow(random, resultRow);
                scoredResult.ScoredResultRows.Add(scoredResultRow);
            }
            return scoredResult;
        }

        private static ScoredResultRowEntity CreateRandomScoredResultRow(Random random, ResultRowEntity resultRow)
        {
            return new ScoredResultRowEntity(resultRow)
            {
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
