using System;
using System.Collections.Generic;
using System.Linq;
using iRLeagueApiCore.Common.Enums;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DbIntegrationTests
{
    public static class PopulateTestDatabase
    {
        public static string ClientUserName => "TestClient";
        public static string ClientGuid => "6a6a6e09-f4b7-4ccb-a8ae-f2fc85d897dd";

        public static void Populate(LeagueDbContext context, Random random)
        {
            // Populate Tracks
            for (int i = 0; i < 2; i++)
            {
                var group = new TrackGroupEntity()
                {
                    TrackName = $"Group{i}",
                    Location = "Testlocation"
                };
                for (int j = 0; j < 3; j++)
                {
                    var config = new TrackConfigEntity()
                    {
                        ConfigName = $"Config{i}",
                        ConfigType = ConfigType.RoadCourse,
                        Turns = j * 3,
                        LengthKm = j * 1.0,
                        MapImageSrc = null,
                        HasNigtLigthing = false
                    };
                    group.TrackConfigs.Add(config);
                }
                context.TrackGroups.Add(group);
            }

            // create models
            var league1 = new LeagueEntity()
            {
                Name = "TestLeague",
                NameFull = "League for unit testing"
            };
            var season1 = new SeasonEntity()
            {
                SeasonName = "Season One",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid,
                League = league1
            };
            var season2 = new SeasonEntity()
            {
                SeasonName = "Season Two",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid,
                League = league1
            };
            var schedule1 = new ScheduleEntity()
            {
                Name = "S1 Schedule",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid
            };
            var schedule2 = new ScheduleEntity()
            {
                Name = "S2 Schedule 1",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid
            };
            var schedule3 = new ScheduleEntity()
            {
                Name = "S2 Schedule 2",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid
            };
            // Create sessions on schedule1
            for (int i = 0; i < 5; i++)
            {
                var session = new SessionEntity()
                {
                    Name = $"S1 Session {i + 1}",
                    CreatedOn = DateTime.Now,
                    CreatedByUserName = ClientUserName,
                    CreatedByUserId = ClientGuid,
                    LastModifiedOn = DateTime.Now,
                    LastModifiedByUserName = ClientUserName,
                    LastModifiedByUserId = ClientGuid,
                    Track = context.TrackGroups
                        .SelectMany(x => x.TrackConfigs)
                        .Skip(i)
                        .FirstOrDefault(),
                    //SessionType = (SessionType)i + 1
                };
                var subSession = new SubSessionEntity()
                {
                    Name = "Race",
                    SessionType = SimSessionType.Race,
                };
                session.SubSessions.Add(subSession);
                schedule1.Sessions.Add(session);
            }
            for (int i = 0; i < 2; i++)
            {
                var session = new SessionEntity()
                {
                    Name = $"S2 Session {i + 1}",
                    CreatedOn = DateTime.Now,
                    CreatedByUserName = ClientUserName,
                    CreatedByUserId = ClientGuid,
                    LastModifiedOn = DateTime.Now,
                    LastModifiedByUserName = ClientUserName,
                    LastModifiedByUserId = ClientGuid,
                    Track = context.TrackGroups
                        .SelectMany(x => x.TrackConfigs)
                        .Skip(i)
                        .FirstOrDefault(),
                    //SessionType = (SessionType)i + 1
                };
                var subSession = new SubSessionEntity()
                {
                    Name = "Race",
                    SessionType = SimSessionType.Race,
                };
                session.SubSessions.Add(subSession);
                schedule2.Sessions.Add(session);
            }
            context.Leagues.Add(league1);
            league1.Seasons.Add(season1);
            league1.Seasons.Add(season2);
            season1.Schedules.Add(schedule1);
            season2.Schedules.Add(schedule2);
            season2.Schedules.Add(schedule3);

            // create league2
            var league2 = new LeagueEntity()
            {
                Name = "TestLeague2",
                NameFull = "Second League for unit testing"
            };
            var l2season1 = new SeasonEntity()
            {
                SeasonName = "L2 Season One",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid,
                League = league1
            };
            var l2schedule1 = new ScheduleEntity()
            {
                Name = "L2S1 Schedule 1",
                CreatedOn = DateTime.Now,
                CreatedByUserName = ClientUserName,
                CreatedByUserId = ClientGuid,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserName = ClientUserName,
                LastModifiedByUserId = ClientGuid
            };

            context.Leagues.Add(league2);
            league2.Seasons.Add(l2season1);
            l2season1.Schedules.Add(l2schedule1);

            GenerateMembers(context, random);

            // assign members to league
            foreach (var member in context.Members)
            {
                var leagueMember = new LeagueMemberEntity()
                {
                    Member = member,
                    League = league1
                };
                context.Set<LeagueMemberEntity>().Add(leagueMember);
            }

            var members = context.Members
                .Local
                .ToList();

            // create results
            var scoring = new ScoringEntity()
            {
                Name = "Testscoring",
                ShowResults = true
            };
            season1.Scorings.Add(scoring);

            foreach (var session in league1.Seasons.SelectMany(x => x.Schedules).SelectMany(x => x.Sessions))
            {
                var scoredResult = new ScoredResultEntity();
                scoring.ScoredResults.Add(scoredResult);
                var result = new ResultEntity();
                var subResult = new SubResultEntity();
                result.SubResults.Add(subResult);
                result.ScoredResults.Add(scoredResult);
                var resultMembers = members.ToList();
                for (int i = 0; i < 10; i++)
                {
                    var resultRow = new ResultRowEntity()
                    {
                        StartPosition = i + 1,
                        FinishPosition = i + 1,
                        Member = resultMembers.PopRandom(random),
                        QualifyingTime = GetTimeSpan(random).Ticks,
                        FastestLapTime = GetTimeSpan(random).Ticks,
                        AvgLapTime = GetTimeSpan(random).Ticks,
                        Interval = GetTimeSpan(random).Ticks
                    };
                    subResult.ResultRows.Add(resultRow);
                    subResult.SubSession = session.SubSessions.First();
                    var scoredResultRow = new ScoredResultRowEntity(resultRow)
                    {
                        FinalPosition = i + 1,
                        RacePoints = 10 - i,
                        TotalPoints = 10 - i
                    };
                    scoredResult.ScoredResultRows.Add(scoredResultRow);
                }
                scoring.Sessions.Add(session);
                session.Result = result;
            }
        }

        private static TimeSpan GetTimeSpan(Random random)
        {
            return new TimeSpan(0, 1, 2, 34, 567);
        }

        private static void GenerateMembers(LeagueDbContext context, Random random)
        {
            var minMemberCount = 50;
            var maxMemberCount = 100;

            var memberCount = random.Next(maxMemberCount - minMemberCount + 1) + minMemberCount;
            var members = context.Set<MemberEntity>();

            for (int i = 0; i < memberCount; i++)
            {
                var member = new MemberEntity()
                {
                    Firstname = GetRandomName(random),
                    Lastname = GetRandomName(random),
                    IRacingId = GetRandomIracingId(random)
                };
                members.Add(member);
            }
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
    }

    public static class PopulateDatabaseExtensions
    {
        /// <summary>
        /// Returns a random entry from the list and removes it from the list at the same time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">List to pop item from</param>
        /// <param name="random">Initialized random number generator</param>
        /// <returns></returns>
        public static T PopRandom<T>(this ICollection<T> collection, Random random = null)
        {
            random ??= new Random();
            var randomIndex = random.Next(collection.Count());
            var pop = collection.ElementAt(randomIndex);
            collection.Remove(pop);
            return pop;
        }
    }
}