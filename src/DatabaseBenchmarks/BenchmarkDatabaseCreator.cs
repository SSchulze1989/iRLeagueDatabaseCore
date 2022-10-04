using iRLeagueApiCore.Common.Enums;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private static readonly int leagueCount = 1; // 5
        private static readonly int seasonCount = 1; // 10
        private static readonly int memberCount = 1000;
        private static readonly int trackCount = 100;
        private static readonly int scheduleCount = 2;
        private static readonly int resultConfigCount = 2;
        private static readonly int eventCount = 4; // 12
        private static readonly int sessionCount = 2;

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
            optionsBuilder
                //.UseLoggerFactory(LoggerFactory.Create(builder =>
                //{
                //    builder.AddConsole();
                //}))
                //.EnableDetailedErrors(true)
                //.EnableSensitiveDataLogging(true)
                .UseMySQL(connectionString);

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

                Console.Write("- Creating Schedules & ResultConfigs ... ");
                foreach (var season in seasons)
                {
                    //var scheduleCount = random.Next(3) + 1;
                    for (int i = 0; i < scheduleCount; i++)
                    {
                        season.Schedules.Add(CreateRandomSchedule(random));
                    }
                }
                for (int i = 0; i < resultConfigCount; i++)
                {
                    league.ResultConfigs.Add(CreateRandomResultConfig(random));
                }
                await context.SaveChangesAsync();
                List<ScheduleEntity> schedules = context.Schedules.Where(x => x.LeagueId == leagueId).ToList();
                List<ResultConfigurationEntity> resultConfigs = context.ResultConfigurations
                    .Where(x => x.LeagueId == leagueId).ToList();
                
                
                Console.Write("Finished!\n");

                Console.Write("- Creating Sessions ... ");
                List<EventEntity> events;
                foreach (var schedule in schedules)
                {
                    //var sessionCount = random.Next(10) + 5;
                    for (int i = 0; i < eventCount; i++)
                    {
                        var @event = CreateRandomEvent(random, trackConfigs);
                        for (int j = 0; j < sessionCount; j++)
                        {
                            var session = CreateRandomSession(random);
                            session.Name = $"Race {j + 1}";
                            session.SessionType = SessionType.Race;
                            @event.Sessions.Add(session);
                        }
                        schedule.Events.Add(@event);
                    }
                }
                await context.SaveChangesAsync();
                events = context.Events.Where(x => x.LeagueId == leagueId).ToList();
                Console.Write("Finished!\n");

                Console.Write("- Creating Results ... ");
                List<EventResultEntity> results;
                // create raw results for each session
                foreach (var @event in events)
                {
                    var result = CreateRandomResult(random);
                    @event.EventResult = result;
                }
                await context.SaveChangesAsync();
                results = context.EventResults.Where(x => x.LeagueId == leagueId).ToList();

                foreach (var result in results)
                {
                    var details = CreateRandomSessionDetails(random);
                    details.Event = result.Event;
                    foreach (var session in result.Event.Sessions)
                    {
                        var subResult = CreateRandomSubResult(random, members, details);
                        result.SessionResults.Add(subResult);
                        session.SessionResult = subResult;
                    }
                }
                await context.SaveChangesAsync();
                Console.Write("Finished!\n");

                Console.Write("- Creating ScoredResults ... ");
                List<ScoredEventResultEntity> scoredResults;
                // create scored result for each scoring + attached schedule session
                foreach (var resultConfig in resultConfigs)
                {
                    foreach (var season in seasons)
                    {
                        var seasonEvents = season.Schedules.SelectMany(x => x.Events);
                        foreach (var @event in seasonEvents)
                        {
                            if (@event.EventResult == null)
                            {
                                continue;
                            }

                            var scoredResult = CreateRandomScoredResult(random, @event.EventResult);
                            scoredResult.Name = resultConfig.DisplayName;
                            @event.ScoredEventResults.Add(scoredResult);
                        }
                    }
                }

                // save that motherfucker
                await context.SaveChangesAsync();
                scoredResults = context.ScoredEventResults.Where(x => x.LeagueId == leagueId).ToList();
                Console.Write("Finished!\n");
                Console.WriteLine("");

                Console.WriteLine("- Creating Reviews ... ");
                var categories = new[]
                {
                    new VoteCategoryEntity() { Index = 0, Text = "Cat 1", DefaultPenalty = 1},
                    new VoteCategoryEntity() { Index = 1, Text = "Cat 2", DefaultPenalty = 2},
                    new VoteCategoryEntity() { Index = 2, Text = "Cat 3", DefaultPenalty = 3},
                    new VoteCategoryEntity() { Index = 3, Text = "Cat 4", DefaultPenalty = 5},
                    new VoteCategoryEntity() { Index = 4, Text = "Cat 5", DefaultPenalty = 10},
                };
                foreach(var category in categories)
                {
                    context.VoteCategories.Add(category);
                }

                List<IncidentReviewEntity> reviews;
                foreach(var session in events.SelectMany(x => x.Sessions))
                {
                    var nReviews = random.Next(1, 10);
                    for (int i = 0; i < nReviews; i++)
                    {
                        var review = CreateRandomReview(random, members, categories);
                        session.IncidentReviews.Add(review);
                    }
                }
                await context.SaveChangesAsync();
                reviews = context.IncidentReviews.Where(x => x.LeagueId == leagueId).ToList();
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
                    $"- {context.Events.Count()} events with" +
                    $"-   {context.Sessions.Count()} sessions\n" +
                    $"- {context.EventResults.Count()} results with\n" +
                    $"-   {context.SessionResults.Count()} session results\n" +
                    $"-   {context.ResultRows.Count()} result rows\n" +
                    $"- {context.ScoredEventResults.Count()} scored results with\n" +
                    $"-   {context.ScoredResultRows.Count()} scored result rows\n" +
                    $"- {context.IncidentReviews.Count()} incident reviews\n");
            }
        }

        private static ResultConfigurationEntity CreateRandomResultConfig(Random random)
        {
            return new ResultConfigurationEntity()
            {
                Name = GetRandomName(random),
                DisplayName = GetRandomName(random),
            };
        }

        private static EventEntity CreateRandomEvent(Random random, List<TrackConfigEntity> tracks)
        {
            return new EventEntity()
            {
                Name = GetRandomName(random),
                Date = GetRandomDateTime(random),
                Duration = TimeSpan.FromHours(1),
                Track = tracks.ElementAt(random.Next(tracks.Count())),
            };
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

        private static SessionEntity CreateRandomSession(Random random)
        {
            var session = new SessionEntity()
            {
                Name = GetRandomName(random),
                Duration = TimeSpan.FromHours(0.5),
            };
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

        private static EventResultEntity CreateRandomResult(Random random)
        {
            var result = new EventResultEntity();
            return result;
        }

        private static SessionResultEntity CreateRandomSubResult(Random random, IEnumerable<MemberEntity> members, IRSimSessionDetailsEntity details)
        {
            var subResult = new SessionResultEntity();
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

        private static ScoredEventResultEntity CreateRandomScoredResult(Random random, EventResultEntity result)
        {
            var scoredResult = new ScoredEventResultEntity()
            {
                Event = result.Event
            };
            foreach (var subResult in result.SessionResults.Where(x => x != null))
            {
                var scoredSessionResult = CreateRandomScoredSessionResult(random, subResult);
                scoredResult.ScoredSessionResults.Add(scoredSessionResult);
            }
            return scoredResult;
        }

        private static ScoredSessionResultEntity CreateRandomScoredSessionResult(Random random, SessionResultEntity sessionResult)
        {
            var rowsCount = sessionResult.ResultRows.Count();
            var scoredSessionResult = new ScoredSessionResultEntity()
            {
                Name = sessionResult.Session.Name,
            };
            for (int i=0; i<rowsCount; i++)
            {
                var resultRow = sessionResult.ResultRows.ElementAt(i);
                var scoredResultRow = CreateRandomScoredResultRow(random, resultRow);
                scoredSessionResult.ScoredResultRows.Add(scoredResultRow);
            }
            return scoredSessionResult;
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

        private static IncidentReviewEntity CreateRandomReview(Random random, IEnumerable<MemberEntity> drivers, IEnumerable<VoteCategoryEntity> voteCategories)
        {
            var review = new IncidentReviewEntity()
            {
                AuthorName = GetRandomName(random),
                AuthorUserId = Guid.NewGuid().ToString(),
                FullDescription = GetRandomText(random),
                ResultLongText = GetRandomText(random),
                Corner = random.Next(3, 15).ToString(),
                OnLap = random.Next(1, 20).ToString(),
                IncidentKind = GetRandomIncidentKind(random),
            };

            // add members involved
            var nInvolvedMembers = random.Next(1, 5);
            for (int i=0; i<nInvolvedMembers; i++)
            {
                var index = random.Next(drivers.Count());
                review.InvolvedMembers.Add(drivers.ElementAt(index));
            }
            // add comments
            var nComments = random.Next(1, 5);
            for (int i=0; i<nComments; i++)
            {
                review.Comments.Add(CreateRandomComment(random, review.InvolvedMembers, voteCategories));
            }
            // add vote results
            var commentVotes = review.Comments.SelectMany(x => x.ReviewCommentVotes).ToArray();
            var acceptedCommentVote = commentVotes.ElementAt(random.Next(commentVotes.Count()));
            review.AcceptedReviewVotes.Add(new AcceptedReviewVoteEntity()
            {
                MemberAtFault = acceptedCommentVote.MemberAtFault,
                Description = acceptedCommentVote.Description,
                VoteCategory = acceptedCommentVote.VoteCategory,
            });
            return review;
        }

        private static ReviewCommentEntity CreateRandomComment(Random random, IEnumerable<MemberEntity> involved, IEnumerable<VoteCategoryEntity> voteCategories)
        {
            var comment = new ReviewCommentEntity()
            {
                AuthorName = GetRandomName(random),
                AuthorUserId = Guid.NewGuid().ToString(),
                Text = GetRandomText(random),
                Date = DateTime.UtcNow,
            };
            // add votes
            var nVotes = random.Next(1, involved.Count());
            for (int i=0; i<nVotes; i++)
            {
                var vote = new ReviewCommentVoteEntity()
                {
                    MemberAtFault = involved.ElementAt(random.Next(involved.Count())),
                    Description = GetRandomText(random, 50),
                    VoteCategory = voteCategories.ElementAt(random.Next(voteCategories.Count())),
                };
                comment.ReviewCommentVotes.Add(vote);
            }
            return comment;
        }

        private static string GetRandomIncidentKind(Random random)
        {
            var incidentKinds = new[]
            {
                "Contact in Corner",
                "Contact on straight",
                "Blocking",
                "Intentional Crashing",
            };
            return incidentKinds[random.Next(incidentKinds.Length)];
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

        private static string GetRandomText(Random random, int maxLength = 5000)
        {
            var text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation 
ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint 
occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            return text.Substring(0, random.Next(Math.Min(maxLength, text.Length)));
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
