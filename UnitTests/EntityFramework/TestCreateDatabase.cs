using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.EntityFramework
{
    public class TestCreateDatabase : IDisposable
    {


        public LeagueDbContext  GetTestDatabaseContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
            var dbContext = new LeagueDbContext(optionsBuilder.Options);

            return dbContext;
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public void TestCreateLeague()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                dbContext.Leagues.Add(new LeagueEntity()
                {
                    Name = "TestLeague",
                    NameFull = "League for unit testing"
                });
                dbContext.SaveChanges();
            }

            using (var dbContext = GetTestDatabaseContext())
            {
                Assert.Equal(1, dbContext.Leagues.Count());
                Assert.Equal("TestLeague", dbContext.Leagues.First().Name);
            }
        }

        [Fact]
        public void TestAgain()
        {
            using (var dbContext = GetTestDatabaseContext())
            {
                Assert.Equal(1, dbContext.Leagues.Count());
                Assert.Equal("TestLeague", dbContext.Leagues.First().Name);
            }
        }

        [Fact]
        public void TestJustThisOneThing()
        {
            Assert.True(true);
        }
    }
}
