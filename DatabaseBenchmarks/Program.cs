using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            Console.WriteLine("Creating benchmark database...");
            using (var context = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                BenchmarkDatabaseCreator.PopulateBenchmarkDatabase(context).Wait();
            }
            Console.Write("Finished creating");

            //var qb = new QueryBenchmarks();
            //qb.TestQueryWithDep();
            //qb.TestQueryWithoutDep();

            var summary = BenchmarkRunner.Run<QueryBenchmarks>();
        }
    }

    [MemoryDiagnoser]
    public class QueryBenchmarks
    {
        private readonly long[] seasonIds;

        public QueryBenchmarks()
        {
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                seasonIds = dbContext.Seasons.Select(x => x.SeasonId).ToArray();
            }
        }


        [Benchmark]
        public void TestQueryWithoutDep()
        {
            var seasonId = 10;
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = dbContext.ScoredResults
                    .Include(x => x.Result)
                        .ThenInclude(x => x.Session)
                            .ThenInclude(x => x.Schedule)
                    .Include(x => x.ScoredResultRows)
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToList();
            }
        }

        [Benchmark]
        public void TestQueryFullResult()
        {
            var seasonId = 10;
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = dbContext.ScoredResults
                    .Include(x => x.Result)
                        .ThenInclude(x => x.Session)
                            .ThenInclude(x => x.Schedule)
                    .Include(x => x.ScoredResultRows)
                        .ThenInclude(x => x.ResultRow)
                            .ThenInclude(x => x.Member)
                    .Include(x => x.ScoredResultRows)
                        .ThenInclude(x => x.Team)
                    .Where(x => x.Result.Session.Schedule.SeasonId == seasonId)
                    .ToList();
            }
        }

        [Benchmark]
        public void TestQueryWithDep()
        {
            var seasonId = 10;
            using (var dbContext = BenchmarkDatabaseCreator.CreateStaticDbContext())
            {
                var seasonResults = dbContext.ScoredResults
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
                    .ToList();
            }
        }
    }
}
