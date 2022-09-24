using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using iRLeagueApiCore.Common.Models;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatabaseBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

#if !DEBUG_CREATEDB
            if (args.Contains("--build-db"))
            {
#endif
                Console.WriteLine("Creating benchmark database...");
                CreateDatabase().GetAwaiter().GetResult();
                Console.Write("Finished creating");
#if !DEBUG_CREATEDB
        }
#endif

#if DEBUG
            var qb = new QuerySeasonResultsBenchmarks();
            var stopWatch = new Stopwatch();
            int loops = 100;

            stopWatch.Start();
            Console.WriteLine("Test normal for loop...");
            for (int i=0; i<loops; i++)
            {
                qb.TestSeparateQuery().Wait();
                qb.TestDirectQuery().Wait();
            }
            stopWatch.Stop();
            Console.WriteLine("Elapsed: {0} s", (stopWatch.ElapsedMilliseconds / 1000).ToString());

            stopWatch.Restart();
            Console.WriteLine("Test parallel for loop...");
            Parallel.For(0, loops, async i =>
            {
                try
                {
                    await qb.TestSeparateQuery();
                    await qb.TestDirectQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
            stopWatch.Stop();
            Console.WriteLine("Elapsed: {0} s", stopWatch.ElapsedMilliseconds/1000);
#else
            var summary = BenchmarkRunner.Run<QuerySeasonResultsBenchmarks>();
#endif
        }

        static async Task CreateDatabase()
        {
            using var context = BenchmarkDatabaseCreator.CreateStaticDbContext();
            await BenchmarkDatabaseCreator.PopulateBenchmarkDatabase();
        }
    }

    [MemoryDiagnoser]
    public class QuerySeasonResultsBenchmarks
    {
        private readonly Random random = new Random();
        private readonly long[] seasonIds;
        private readonly long[] resultIds;
        private long seasonId => seasonIds.ElementAt(random.Next(seasonIds.Count()));
        private long resultId => resultIds.ElementAt(random.Next(resultIds.Count()));


        public QuerySeasonResultsBenchmarks()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                seasonIds = dbContext.Seasons.Select(x => x.SeasonId).ToArray();
                resultIds = dbContext.ScoredEventResults.Select(x => x.ResultId).ToArray();
            }
        }

        [Benchmark]
        public async Task TestIncludeQuery()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredEventResults
                    .Include(x => x.RawResult)
                        .ThenInclude(x => x.Event)
                            .ThenInclude(x => x.Schedule)
                    .Include(x => x.ScoredSessionResults)
                        .ThenInclude(x => x.ScoredResultRows)        
                            .ThenInclude(x => x.Member)
                    .Include(x => x.ScoredSessionResults)
                        .ThenInclude(x => x.ScoredResultRows)
                            .ThenInclude(x => x.Team)
                    .Where(x => x.Event.Schedule.SeasonId == seasonId)
                    .ToListAsync();
                return;
            }
        }

        [Benchmark]
        public async Task TestSeparateQuery()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredEventResults
                    .Include(x => x.Event)
                        .ThenInclude(x => x.Schedule)
                    .Include(x => x.ScoredSessionResults)
                    .Where(x => x.Event.Schedule.SeasonId == seasonId)
                    .ToListAsync();

                var seasonResultsIds = seasonResults
                    .SelectMany(x => x.ScoredSessionResults)
                    .Select(x => x.SessionResultId).Distinct();

                await dbContext.ScoredResultRows
                    .Where(x => seasonResultsIds.Contains(x.SessionResultId))
                    .LoadAsync();

                Debug.Assert(seasonResults
                    .SelectMany(x => x.ScoredSessionResults)
                    .SelectMany(x => x.ScoredResultRows)
                    .Any(x => x != null));
                return;
            }
        }

        [Benchmark]
        public async Task TestDirectQuery()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResultsQuery = dbContext.ScoredEventResults
                    .Where(x => x.Event.Schedule.SeasonId == seasonId)
                    .Select(MapToResultModelExpression);
                var seasonResults = await seasonResultsQuery.ToListAsync();
                return;
            }
        }

        [Benchmark]
        public async Task<EventResultModel> SingleEventQuery()
        {
            using var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext();
            var eventResults = await dbContext.ScoredEventResults
                .Select(MapToResultModelExpression)
                .SingleOrDefaultAsync(x => x.ResultId == resultId);
            return eventResults;
        }

        private static Expression<Func<ScoredEventResultEntity, EventResultModel>> MapToResultModelExpression => result => new EventResultModel()
        {
            EventId = result.EventId,
            LeagueId = result.LeagueId,
            EventName = result.Event.Name,
            DisplayName = result.Name,
            ResultId = result.ResultId,
            Date = result.Event.Date.GetValueOrDefault(),
            TrackId = result.Event.TrackId.GetValueOrDefault(),
            SessionResults = result.ScoredSessionResults.Select(sessionResult => new ResultModel()
            {
                LeagueId = sessionResult.LeagueId,
                ScoringId = sessionResult.ScoringId,
                SessionId = default,
                SessionName = sessionResult.Name,
                ResultRows = sessionResult.ScoredResultRows.Select(row => new ResultRowModel()
                {
                    MemberId = row.MemberId,
                    Interval = new TimeSpan(row.Interval),
                    FastestLapTime = new TimeSpan(row.FastestLapTime),
                    AvgLapTime = new TimeSpan(row.AvgLapTime),
                    Firstname = row.Member.Firstname,
                    Lastname = row.Member.Lastname,
                    TeamName = row.Team.Name,
                    StartPosition = row.StartPosition,
                    FinishPosition = row.FinishPosition,
                    FinalPosition = row.FinalPosition,
                    RacePoints = row.RacePoints,
                    PenaltyPoints = row.PenaltyPoints,
                    BonusPoints = row.BonusPoints,
                    TotalPoints = row.TotalPoints,
                    Car = row.Car,
                    CarClass = row.CarClass,
                    CarId = row.CarId,
                    CarNumber = row.CarNumber,
                    ClassId = row.ClassId,
                    CompletedLaps = row.CompletedLaps,
                    CompletedPct = row.CompletedPct,
                    Division = row.Division,
                    FastLapNr = row.FastLapNr,
                    FinalPositionChange = row.FinalPositionChange,
                    Incidents = row.Incidents,
                    LeadLaps = row.LeadLaps,
                    License = row.License,
                    NewIrating = row.NewIRating,
                    NewLicenseLevel = row.NewLicenseLevel,
                    NewSafetyRating = row.NewSafetyRating,
                    OldIrating = row.OldIRating,
                    OldLicenseLevel = row.OldLicenseLevel,
                    OldSafetyRating = row.OldSafetyRating,
                    PositionChange = row.PositionChange,
                    QualifyingTime = new TimeSpan(row.QualifyingTime),
                    SeasonStartIrating = row.SeasonStartIRating,
                    Status = row.Status,
                    TeamId = row.TeamId
                }),
                CreatedOn = sessionResult.CreatedOn,
                LastModifiedOn = sessionResult.LastModifiedOn,
            }),
        };
    }
}
