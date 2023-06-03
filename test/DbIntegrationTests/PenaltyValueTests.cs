using FluentAssertions;
using iRLeagueApiCore.Common.Enums;
using iRLeagueDatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace DbIntegrationTests;
public sealed class PenaltyValueTests : DatabaseTestBase
{
    [Fact]
    public async Task ShouldStorePointsPenaltyData()
    {
        //using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var resultRow = await DbContext.ScoredResultRows.FirstAsync();
        var addPenalty = new AddPenaltyEntity()
        {
            LeagueId = resultRow.LeagueId,
            ScoredResultRow = resultRow,
            Value = new() { Type = PenaltyType.Points, Points = 10 }
        };
        resultRow.AddPenalties.Add(addPenalty);
        await DbContext.SaveChangesAsync();

        var test = await DbContext.AddPenaltys.FirstAsync(x => x.AddPenaltyId == addPenalty.AddPenaltyId);
        test.Value.Type.Should().Be(addPenalty.Value.Type);
        test.Value.Points.Should().Be(addPenalty.Value.Points);
    }

    [Fact]
    public async Task ShouldStorePositionsPenaltyData()
    {
        //using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var resultRow = await DbContext.ScoredResultRows.FirstAsync();
        var addPenalty = new AddPenaltyEntity()
        {
            LeagueId = resultRow.LeagueId,
            ScoredResultRow = resultRow,
            Value = new() { Type = PenaltyType.Position, Positions = 1 }
        };
        resultRow.AddPenalties.Add(addPenalty);
        await DbContext.SaveChangesAsync();

        var test = await DbContext.AddPenaltys.FirstAsync(x => x.AddPenaltyId == addPenalty.AddPenaltyId);
        test.Value.Type.Should().Be(addPenalty.Value.Type);
        test.Value.Points.Should().Be(addPenalty.Value.Points);
    }

    [Fact]
    public async Task ShouldStoreTimePenaltyData()
    {
        //using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var resultRow = await DbContext.ScoredResultRows.FirstAsync();
        var addPenalty = new AddPenaltyEntity()
        {
            LeagueId = resultRow.LeagueId,
            ScoredResultRow = resultRow,
            Value = new() { Type = PenaltyType.Time, Time = TimeSpan.FromSeconds(10) }
        };
        resultRow.AddPenalties.Add(addPenalty);
        await DbContext.SaveChangesAsync();

        var test = await DbContext.AddPenaltys.FirstAsync(x => x.AddPenaltyId == addPenalty.AddPenaltyId);
        test.Value.Type.Should().Be(addPenalty.Value.Type);
        test.Value.Points.Should().Be(addPenalty.Value.Points);
    }
}
