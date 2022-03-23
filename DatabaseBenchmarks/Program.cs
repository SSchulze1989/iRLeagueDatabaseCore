﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
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
                using (var context = BenchmarkDatabaseCreator.CreateStaticDbContext())
                {
                    BenchmarkDatabaseCreator.PopulateBenchmarkDatabase(context).Wait();
                }
                Console.Write("Finished creating");
            }

#if DEBUG
            var qb = new QueryBenchmarks();
            var stopWatch = new Stopwatch();
            int loops = 50;

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
            var summary = BenchmarkRunner.Run<QueryBenchmarks>();
#endif
        }
    }

    [MemoryDiagnoser]
    public class QueryBenchmarks
    {
        private readonly long[] seasonIds;
        private readonly long seasonId = 10;

        public QueryBenchmarks()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                seasonIds = dbContext.Seasons.Select(x => x.SeasonId).ToArray();
            }
        }

        [Benchmark]
        public async Task TestIncludeQueryWithoutRows()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredResults
                    .Include(x => x.Result)
                        .ThenInclude(x => x.Session)
                            .ThenInclude(x => x.Schedule)
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToListAsync();
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
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToListAsync();
            }
        }

        [Benchmark]
        public async Task TestIncludeQueryFullResult()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredResults
                    .Include(x => x.Result)
                        .ThenInclude(x => x.Session)
                            .ThenInclude(x => x.Schedule)
                    .Include(x => x.ScoredResultRows)
                        .ThenInclude(x => x.ResultRow)
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
                    .Include(x => x.ScoredResultRows)
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToListAsync();

                var seasonResultsIds = seasonResults.Select(x => x.ResultId).Distinct();
                var seasonScoringIds = seasonResults.Select(x => x.ScoringId).Distinct();

                await dbContext.ScoredResultRows
                    .Where(x => seasonResultsIds.Contains(x.ResultId) && seasonScoringIds.Contains(x.ScoringId))
                    .LoadAsync();

                Debug.Assert(seasonResults.SelectMany(x => x.ScoredResultRows).Count() > 0);
            }
        }

        [Benchmark]
        public async Task TestDirectQuery()
        {
            var seasonId = 10;
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = await dbContext.ScoredResults
                    .Select(result => new
                    {
                        LeagueId = result.LeagueId,
                        SeasonId = result.Result.Session.Schedule.SeasonId,
                        SessionId = result.ResultId,
                        SessionDetails = new 
                        {
                            TrackName = result.Result.IRSimSessionDetails.TrackName
                        },
                        ResultRows = result.ScoredResultRows
                            .Select(row => new
                            {
                                MemberId = row.ResultRow.MemberId,
                                Interval = row.ResultRow.Interval,
                                FastestLap = row.ResultRow.FastestLapTime,
                                AverageLap = row.ResultRow.AvgLapTime,
                                Firstname = row.ResultRow.Member.Firstname,
                                Lastname = row.ResultRow.Member.Lastname,
                                TeamName = row.Team.Name,
                                StartPosition = row.ResultRow.StartPosition,
                                FinishPosition = row.ResultRow.FinishPosition,
                                FinalPosition = row.FinalPosition,
                                RacePoints = row.RacePoints,
                                PenaltyPoints = row.PenaltyPoints,
                                BonusPoints = row.BonusPoints,
                                TotalPoints = row.TotalPoints
                            }).ToArray(),
                    })
                    .Where(x => x.SeasonId == seasonId)
                    .ToListAsync();
            }
        }
    }
}