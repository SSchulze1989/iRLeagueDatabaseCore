using System;
using System.Linq;
using iRLeagueApiCore.Communication.Enums;
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
            var league = new LeagueEntity()
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
                League = league
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
                League = league
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
                    Name = $"S1 Session {i}",
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
                    SessionTitle = $"S1 Session {i}",
                    SessionType = (SessionTypeEnum)i + 1
                };
                schedule1.Sessions.Add(session);
            }
            context.Leagues.Add(league);
            league.Seasons.Add(season1);
            league.Seasons.Add(season2);
            season1.Schedules.Add(schedule1);
            season2.Schedules.Add(schedule2);
            season2.Schedules.Add(schedule3);

            GenerateMembers(context, random);

            // assign members to league
            //foreach (var member in context.Members.Local)
            //{
            //    var leagueMember = new LeagueMemberEntity()
            //    {
            //        Member = member,
            //        League = league
            //    };
            //    context.Set<LeagueMemberEntity>().Add(leagueMember);
            //}

            // create result
            var scoredResult = new ScoredResultEntity();
            var scoring = new ScoringEntity()
            {
                Name = "Testscoring",
                ShowResults = true
            };
            season1.Scorings.Add(scoring);
            scoring.ScoredResults.Add(scoredResult);
            scoredResult.Scoring = scoring;
            var result = new ResultEntity();
            result.ScoredResults.Add(scoredResult);
            for (int i = 0; i < 10; i++)
            {
                var resultRow = new ResultRowEntity()
                {
                    StartPosition = i + 1,
                    FinishPosition = i + 1,
                    Member = context.Members.Local.ElementAt(i),
                };
                result.ResultRows.Add(resultRow);
                var scoredResultRow = new ScoredResultRowEntity()
                {
                    FinalPosition = i + 1,
                    RacePoints = 10 - i,
                    TotalPoints = 10 - i
                };
                scoredResult.ScoredResultRows.Add(scoredResultRow);
                resultRow.ScoredResultRows.Add(scoredResultRow);
            }
            schedule1.Sessions
                .First()
                .Result = result;
            result.Session = schedule1.Sessions.First();
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
}