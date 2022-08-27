using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using iRLeagueApiCore.Common.Models;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            if (args.Contains("--build-db"))
            {
                Console.WriteLine("Creating benchmark database...");
                CreateDatabase().GetAwaiter().GetResult();
                Console.Write("Finished creating");
            }

#if DEBUG
            var qb = new QuerySeasonResultsBenchmarks();
            var stopWatch = new Stopwatch();
            int loops = 10;

            stopWatch.Start();
            Console.WriteLine("Test normal for loop...");
            Console.WriteLine("Elapsed: {0} s", stopWatch.ElapsedMilliseconds / 1000);
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
        private long seasonId => seasonIds.ElementAt(random.Next(seasonIds.Count()));

        public QuerySeasonResultsBenchmarks()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                seasonIds = dbContext.Seasons.Select(x => x.SeasonId).ToArray();
            }
        }

        [Benchmark]
        public async Task TestIncludeQuery()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredResults
                    .Include(x => x.Result)
                        .ThenInclude(x => x.Session)
                            .ThenInclude(x => x.Schedule)
                    .Include(x => x.ScoredResultRows)
                            .ThenInclude(x => x.Member)
                    .Include(x => x.ScoredResultRows)
                        .ThenInclude(x => x.Team)
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToListAsync();
            }
        }

        [Benchmark]
        public async Task TestSeparateQuery()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredResults
                    .Include(x => x.Result)
                        .ThenInclude(x => x.Session)
                            .ThenInclude(x => x.Schedule)
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToListAsync();

                var seasonResultsIds = seasonResults.Select(x => x.ResultId).Distinct();
                var seasonScoringIds = seasonResults.Select(x => x.ScoringId).Distinct();

                await dbContext.ScoredResultRows
                    .Where(x => seasonResultsIds.Contains(x.ResultId) && seasonScoringIds.Contains(x.ScoringId))
                    .LoadAsync();

                Debug.Assert(seasonResults.SelectMany(x => x.ScoredResultRows).Where(x => x != null).Count() > 0);
            }
        }

        [Benchmark]
        public async Task TestDirectQuery()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredResults
                    .Select(result => new ResultModel
                    {
                        LeagueId = result.LeagueId,
                        SeasonId = result.Result.Session.Schedule.SeasonId,
                        SessionId = result.ResultId,
                        ResultRows = result.ScoredResultRows
                            .Select(row => new ResultRowModel
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
                                SeasonStartIrating = row.SeasonStartIRating,
                                Status = row.Status,
                                TeamId = row.TeamId
                            }).ToArray(),
                    })
                    .Where(x => x.SeasonId == seasonId)
                    .ToListAsync();
            }
        }
    }
}
