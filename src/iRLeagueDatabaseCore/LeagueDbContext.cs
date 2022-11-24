using System;
using System.ComponentModel.DataAnnotations.Schema;
using iRLeagueApiCore.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class LeagueDbContext : DbContext, ILeagueDbContext
    {
        //public LeagueDbContext()
        //{
        //}

        public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
            : base(options)
        {   
        }

        public virtual DbSet<AcceptedReviewVoteEntity> AcceptedReviewVotes { get; set; }
        public virtual DbSet<AddPenaltyEntity> AddPenaltys { get; set; }
        public virtual DbSet<ReviewCommentEntity> ReviewComments { get; set; }
        public virtual DbSet<ReviewCommentVoteEntity> ReviewCommentVotes { get; set; }
        public virtual DbSet<CustomIncidentEntity> CustomIncidents { get; set; }
        public virtual DbSet<DriverStatisticRowEntity> DriverStatisticRows { get; set; }
        public virtual DbSet<EventEntity> Events { get; set; }
        public virtual DbSet<IncidentReviewEntity> IncidentReviews { get; set; }
        public virtual DbSet<LeagueEntity> Leagues { get; set; }
        public virtual DbSet<MemberEntity> Members { get; set; }
        public virtual DbSet<LeagueMemberEntity> LeagueMembers { get; set; }
        public virtual DbSet<PointRuleEntity> PointRules { get; set; }
        public virtual DbSet<EventResultEntity> EventResults { get; set; }
        public virtual DbSet<ResultConfigurationEntity> ResultConfigurations { get; set; }
        public virtual DbSet<ResultRowEntity> ResultRows { get; set; }
        public virtual DbSet<FilterOptionEntity> FilterOptions { get; set; }
        public virtual DbSet<FilterConditionEntity> FilterConditions { get; set; }
        public virtual DbSet<ReviewPenaltyEntity> ReviewPenaltys { get; set; }
        public virtual DbSet<ScheduleEntity> Schedules { get; set; }
        public virtual DbSet<ScoredEventResultEntity> ScoredEventResults { get; set; }
        public virtual DbSet<ScoredResultRowEntity> ScoredResultRows { get; set; }
        public virtual DbSet<ScoredSessionResultEntity> ScoredSessionResults { get; set; }
        public virtual DbSet<ScoringEntity> Scorings { get; set; }
        public virtual DbSet<StandingEntity> Standings { get; set; }
        public virtual DbSet<SeasonEntity> Seasons { get; set; }
        public virtual DbSet<SessionEntity> Sessions { get; set; }
        public virtual DbSet<StatisticSetEntity> StatisticSets { get; set; }
        public virtual DbSet<TeamEntity> Teams { get; set; }
        public virtual DbSet<VoteCategoryEntity> VoteCategories { get; set; }
        public virtual DbSet<TrackGroupEntity> TrackGroups { get; set; }
        public virtual DbSet<IRSimSessionDetailsEntity> IRSimSessionDetails { get; set; }
        public virtual DbSet<TrackConfigEntity> TrackConfigs { get; set; }
        public virtual DbSet<SessionResultEntity> SessionResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeagueDbContext).Assembly);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
