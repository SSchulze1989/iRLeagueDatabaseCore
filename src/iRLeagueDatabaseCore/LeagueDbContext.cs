﻿#nullable disable

namespace iRLeagueDatabaseCore.Models;

public partial class LeagueDbContext : DbContext, ILeagueDbContext
{
    private readonly ILeagueProvider leagueProvider;

    public LeagueDbContext(DbContextOptions<LeagueDbContext> options, ILeagueProvider leagueProvider)
        : base(options)
    {
        this.leagueProvider = leagueProvider;
    }

    public virtual DbSet<AcceptedReviewVoteEntity> AcceptedReviewVotes { get; set; }
    public virtual DbSet<AddPenaltyEntity> AddPenaltys { get; set; }
    public virtual DbSet<ReviewCommentEntity> ReviewComments { get; set; }
    public virtual DbSet<ReviewCommentVoteEntity> ReviewCommentVotes { get; set; }
    public virtual DbSet<CustomIncidentEntity> CustomIncidents { get; set; }
    public virtual DbSet<ChampionshipEntity> Championships { get; set; }
    public virtual DbSet<ChampSeasonEntity> ChampSeasons { get; set; }
    public virtual DbSet<DriverStatisticRowEntity> DriverStatisticRows { get; set; }
    public virtual DbSet<EventEntity> Events { get; set; }
    public virtual DbSet<IncidentReviewEntity> IncidentReviews { get; set; }
    public virtual DbSet<LeagueEntity> Leagues { get; set; }
    public virtual DbSet<MemberEntity> Members { get; set; }
    public virtual DbSet<LeagueMemberEntity> LeagueMembers { get; set; }
    public virtual DbSet<PointRuleEntity> PointRules { get; set; }
    public virtual DbSet<ProtestEntity> Protests { get; set; }
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
    public virtual DbSet<StandingConfigurationEntity> StandingConfigurations { get; set; }
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

        ConfigureMultiTenancy(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    private void ConfigureMultiTenancy(ModelBuilder builder)
    {
        builder.Entity<AcceptedReviewVoteEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<AddPenaltyEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ReviewCommentEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ReviewCommentVoteEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<CustomIncidentEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ChampionshipEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ChampSeasonEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<DriverStatisticRowEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<EventEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<IncidentReviewEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<LeagueMemberEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<PointRuleEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ProtestEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<EventResultEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ResultConfigurationEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ResultRowEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<FilterOptionEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<FilterConditionEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ReviewPenaltyEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ScheduleEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ScoredEventResultEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ScoredResultRowEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ScoredSessionResultEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ScoringEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<StandingEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<StandingConfigurationEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<SeasonEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<SessionEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<StatisticSetEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<TeamEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<VoteCategoryEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<IRSimSessionDetailsEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<SessionResultEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
        builder.Entity<ResultConfigurationEntity>()
            .HasQueryFilter(mt => mt.LeagueId == leagueProvider.LeagueId);
    }
}
