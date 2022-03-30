using System;
using System.ComponentModel.DataAnnotations.Schema;
using iRLeagueApiCore.Communication.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class LeagueDbContext : DbContext
    {
        public LeagueDbContext()
        {
        }

        public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcceptedReviewVoteEntity> AcceptedReviewVotes { get; set; }
        public virtual DbSet<AddPenaltyEntity> AddPenaltys { get; set; }
        public virtual DbSet<CommentBaseEntity> CommentBases { get; set; }
        public virtual DbSet<CommentReviewVoteEntity> CommentReviewVotes { get; set; }
        public virtual DbSet<CustomIncidentEntity> CustomIncidents { get; set; }
        public virtual DbSet<DriverStatisticRowEntity> DriverStatisticRows { get; set; }
        public virtual DbSet<IncidentReviewEntity> IncidentReviews { get; set; }
        public virtual DbSet<LeagueEntity> Leagues { get; set; }
        public virtual DbSet<MemberEntity> Members { get; set; }
        public virtual DbSet<ResultEntity> Results { get; set; }
        public virtual DbSet<ResultRowEntity> ResultRows { get; set; }
        public virtual DbSet<ResultsFilterOptionEntity> ResultsFilterOptions { get; set; }
        public virtual DbSet<ReviewPenaltyEntity> ReviewPenaltys { get; set; }
        public virtual DbSet<ScheduleEntity> Schedules { get; set; }
        public virtual DbSet<ScoredResultEntity> ScoredResults { get; set; }
        public virtual DbSet<ScoredResultRowEntity> ScoredResultRows { get; set; }
        public virtual DbSet<ScoredTeamResultRowEntity> ScoredTeamResultRows { get; set; }
        public virtual DbSet<ScoringEntity> Scorings { get; set; }
        public virtual DbSet<StandingEntity> Standings { get; set; }
        public virtual DbSet<SeasonEntity> Seasons { get; set; }
        public virtual DbSet<SessionEntity> Sessions { get; set; }
        public virtual DbSet<StatisticSetEntity> StatisticSets { get; set; }
        public virtual DbSet<TeamEntity> Teams { get; set; }
        public virtual DbSet<VoteCategoryEntity> VoteCategorys { get; set; }
        public virtual DbSet<TrackGroupEntity> TrackGroups { get; set; }
        public virtual DbSet<IRSimSessionDetailsEntity> IRSimSessionDetails { get; set; }
        public virtual DbSet<TrackConfigEntity> TrackConfigs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AcceptedReviewVoteEntity>(entity =>
            {
                entity.HasKey(e => e.ReviewVoteId)
                    .HasName("PK_dbo.AcceptedReviewVotes");

                entity.HasIndex(e => e.VoteCategoryId, "IX_CustomVoteCatId");

                entity.HasIndex(e => e.MemberAtFaultId, "IX_MemberAtFaultId");

                entity.HasIndex(e => e.ReviewId, "IX_ReviewId");

                entity.HasOne(d => d.VoteCategory)
                    .WithMany(p => p.AcceptedReviewVotes)
                    .HasForeignKey(d => d.VoteCategoryId)
                    .HasConstraintName("FK_dbo.AcceptedReviewVotes_dbo.VoteCategorys_CustomVoteCatId");

                entity.HasOne(d => d.MemberAtFault)
                    .WithMany(p => p.AcceptedReviewVotes)
                    .HasForeignKey(d => d.MemberAtFaultId)
                    .HasConstraintName("FK_dbo.AcceptedReviewVotes_dbo.LeagueMembers_MemberAtFaultId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.AcceptedReviewVotes)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_dbo.AcceptedReviewVotes_dbo.IncidentReviews_ReviewId");
            });

            modelBuilder.Entity<AddPenaltyEntity>(entity =>
            {
                entity.HasKey(e => e.ScoredResultRowId)
                    .HasName("PK_dbo.AddPenaltys");

                entity.HasIndex(e => e.ScoredResultRowId, "IX_ScoredResultRowId");

                entity.Property(e => e.ScoredResultRowId).ValueGeneratedNever();

                entity.HasOne(d => d.ScoredResultRow)
                    .WithOne(p => p.AddPenalty)
                    .HasForeignKey<AddPenaltyEntity>(d => d.ScoredResultRowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.AddPenaltys_dbo.ScoredResultRows_ScoredResultRowId");
            });

            modelBuilder.Entity<CommentBaseEntity>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_dbo.CommentBases");

                entity.HasIndex(e => e.ReplyToCommentId, "IX_ReplyToCommentId");

                entity.HasIndex(e => e.ReviewId, "IX_ReviewId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ReplyToComment)
                    .WithMany(p => p.InverseReplyToComment)
                    .HasForeignKey(d => d.ReplyToCommentId)
                    .HasConstraintName("FK_dbo.CommentBases_dbo.CommentBases_ReplyToCommentId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.CommentBases_dbo.IncidentReviews_ReviewId");
            });

            modelBuilder.Entity<CommentReviewVoteEntity>(entity =>
            {
                entity.HasKey(e => e.ReviewVoteId)
                    .HasName("PK_dbo.CommentReviewVotes");

                entity.HasIndex(e => e.CommentId, "IX_CommentId");

                entity.HasIndex(e => e.VoteCategoryId, "IX_CustomVoteCatId");

                entity.HasIndex(e => e.MemberAtFaultId, "IX_MemberAtFaultId");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReviewVotes)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_dbo.CommentReviewVotes_dbo.CommentBases_CommentId");

                entity.HasOne(d => d.VoteCategory)
                    .WithMany(p => p.CommentReviewVotes)
                    .HasForeignKey(d => d.VoteCategoryId)
                    .HasConstraintName("FK_dbo.CommentReviewVotes_dbo.VoteCategorys_CustomVoteCatId");

                entity.HasOne(d => d.MemberAtFault)
                    .WithMany(p => p.CommentReviewVotes)
                    .HasForeignKey(d => d.MemberAtFaultId)
                    .HasConstraintName("FK_dbo.CommentReviewVotes_dbo.LeagueMembers_MemberAtFaultId");
            });

            modelBuilder.Entity<CustomIncidentEntity>(entity =>
            {
                entity.HasKey(e => e.IncidentId)
                    .HasName("PK_dbo.CustomIncidents");
            });

            modelBuilder.Entity<DriverStatisticRowEntity>(entity =>
            {
                entity.HasKey(e => new { e.StatisticSetId, e.MemberId })
                    .HasName("PK_dbo.DriverStatisticRows");

                entity.HasIndex(e => e.FirstRaceId, "IX_FirstRaceId");

                entity.HasIndex(e => e.FirstResultRowId, "IX_FirstResultRowId");

                entity.HasIndex(e => e.FirstSessionId, "IX_FirstSessionId");

                entity.HasIndex(e => e.LastRaceId, "IX_LastRaceId");

                entity.HasIndex(e => e.LastResultRowId, "IX_LastResultRowId");

                entity.HasIndex(e => e.LastSessionId, "IX_LastSessionId");

                entity.HasIndex(e => e.MemberId, "IX_MemberId");

                entity.HasIndex(e => e.StatisticSetId, "IX_StatisticSetId");

                entity.Property(e => e.AvgIrating).HasColumnName("AvgIRating");

                entity.Property(e => e.AvgSrating).HasColumnName("AvgSRating");

                entity.Property(e => e.EndIrating).HasColumnName("EndIRating");

                entity.Property(e => e.EndSrating).HasColumnName("EndSRating");

                entity.Property(e => e.FirstRaceDate).HasColumnType("datetime");

                entity.Property(e => e.FirstSessionDate).HasColumnType("datetime");

                entity.Property(e => e.LastRaceDate).HasColumnType("datetime");

                entity.Property(e => e.LastSessionDate).HasColumnType("datetime");

                entity.Property(e => e.StartIrating).HasColumnName("StartIRating");

                entity.Property(e => e.StartSrating).HasColumnName("StartSRating");

                entity.HasOne(d => d.FirstRace)
                    .WithMany()
                    .HasForeignKey(d => new { d.LeagueId, d.FirstRaceId })
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.Sessions_FirstRaceId");

                entity.HasOne(d => d.FirstResultRow)
                    .WithMany()
                    .HasForeignKey(d => d.FirstResultRowId)
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.ScoredResultRows_FirstResultRowId");

                entity.HasOne(d => d.FirstSession)
                    .WithMany()
                    .HasForeignKey(d => new { d.LeagueId, d.FirstSessionId })
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.Sessions_FirstSessionId");

                entity.HasOne(d => d.LastRace)
                    .WithMany()
                    .HasForeignKey(d => new { d.LeagueId, d.LastRaceId })
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.Sessions_LastRaceId");

                entity.HasOne(d => d.LastResultRow)
                    .WithMany()
                    .HasForeignKey(d => d.LastResultRowId)
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.ScoredResultRows_LastResultRowId");

                entity.HasOne(d => d.LastSession)
                    .WithMany()
                    .HasForeignKey(d => new { d.LeagueId, d.LastSessionId })
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.Sessions_LastSessionId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.DriverStatisticRows)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.LeagueMembers_MemberId");

                entity.HasOne(d => d.StatisticSet)
                    .WithMany(p => p.DriverStatisticRows)
                    .HasForeignKey(d => d.StatisticSetId)
                    .HasConstraintName("FK_dbo.DriverStatisticRows_dbo.StatisticSets_StatisticSetId");
            });

            modelBuilder.Entity<IncidentReviewEntity>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK_dbo.IncidentReviews");

                entity.HasIndex(e => e.SessionId, "IX_SessionId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.IncidentReviews)
                    .HasForeignKey(d => new { d.LeagueId, d.SessionId })
                    .HasConstraintName("FK_dbo.IncidentReviews_dbo.Sessions_SessionId");

                entity.HasMany(d => d.InvolvedMembers)
                    .WithMany(p => p.InvolvedReviews)
                    .UsingEntity(e => e.ToTable("IncidentReviewsInvolvedMembers"));
            });

            modelBuilder.Entity<MemberEntity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_dbo.LeagueMembers");

            });

            modelBuilder.Entity<ResultEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.ResultId })
                    .HasName("PK_dbo.Results");

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.HasIndex(e => new { e.LeagueId, e.SeasonId }, "IX_SeasonId");

                entity.Property(e => e.ResultId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithOne(p => p.Result)
                    .HasForeignKey<ResultEntity>(d => new { d.LeagueId, d.ResultId })
                    .HasConstraintName("FK_dbo.Results_dbo.Sessions_ResultId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => new { d.LeagueId, d.SeasonId })
                    .HasConstraintName("FK_dbo.Results_dbo.Seasons_SeasonId");

                entity.HasOne(d => d.IRSimSessionDetails)
                    .WithOne(p => p.Result);
            });

            modelBuilder.Entity<IRSimSessionDetailsEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.ResultId });

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.Navigation(d => d.Result).UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SimStartUtcOffset).HasColumnName("SimStartUTCOffset");

                entity.Property(e => e.SimStartUtcTime)
                    .HasColumnType("datetime")
                    .HasColumnName("SimStartUTCTime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(e => e.Result)
                    .WithOne(p => p.IRSimSessionDetails)
                    .HasForeignKey<IRSimSessionDetailsEntity>(d => new { d.LeagueId, d.ResultId })
                    .IsRequired()
                    .HasConstraintName("FK_dbo.IRSimSessionDetails_dbo.Results_ResultId");
            });

            modelBuilder.Entity<ResultRowEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.ResultRowId })
                    .HasName("PK_dbo.ResultRows");
                entity.HasAlternateKey(e => e.ResultRowId)
                    .HasName("Ak_dbo.ResultRows");

                entity.Property(e => e.ResultRowId)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.ResultRowId, "IX_ResultRowId");

                entity.HasIndex(e => e.MemberId, "IX_MemberId");

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.HasIndex(e => e.TeamId, "IX_TeamId");

                entity.Property(e => e.IracingId).HasColumnName("IRacingId");

                entity.Property(e => e.NewIrating).HasColumnName("NewIRating");

                entity.Property(e => e.OldIrating).HasColumnName("OldIRating");

                entity.Property(e => e.QualifyingTimeAt).HasColumnType("datetime");

                entity.Property(e => e.SeasonStartIrating).HasColumnName("SeasonStartIRating");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ResultRows)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ResultRows_dbo.LeagueMembers_MemberId");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ResultRows)
                    .HasForeignKey(d => new { d.LeagueId, d.ResultId })
                    .HasConstraintName("FK_dbo.ResultRows_dbo.Results_ResultId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ResultRows)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_dbo.ResultRows_dbo.Teams_TeamId");
            });

            modelBuilder.Entity<ResultsFilterOptionEntity>(entity =>
            {
                entity.HasKey(e => e.ResultsFilterId)
                    .HasName("PK_dbo.ResultsFilterOptions");

                entity.HasIndex(e => e.ScoringId, "IX_ScoringId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Scoring)
                    .WithMany(p => p.ResultsFilterOptions)
                    .HasForeignKey(d => new { d.LeagueId, d.ScoringId })
                    .HasConstraintName("FK_dbo.ResultsFilterOptions_dbo.Scorings_ScoringId");
            });

            modelBuilder.Entity<ReviewPenaltyEntity>(entity =>
            {
                entity.HasKey(e => new { e.ResultRowId, e.ReviewId })
                    .HasName("PK_dbo.ReviewPenaltys");

                entity.HasIndex(e => e.ResultRowId, "IX_ResultRowId");

                entity.HasIndex(e => e.ReviewId, "IX_ReviewId");

                entity.HasIndex(e => e.ReviewVoteId, "IX_ReviewVoteId");

                entity.HasOne(d => d.ResultRow)
                    .WithMany(p => p.ReviewPenalty)
                    .HasForeignKey(d => d.ResultRowId)
                    .HasConstraintName("FK_dbo.ReviewPenaltys_dbo.ScoredResultRows_ResultRowId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewPenaltys)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ReviewPenaltys_dbo.IncidentReviews_ReviewId");

                entity.HasOne(d => d.ReviewVote)
                    .WithMany(p => p.ReviewPenaltys)
                    .HasForeignKey(d => d.ReviewVoteId)
                    .HasConstraintName("FK_dbo.ReviewPenaltys_dbo.AcceptedReviewVotes_ReviewVoteId");
            });

            modelBuilder.Entity<ScheduleEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.ScheduleId })
                    .HasName("PK_dbo.Schedules");

                entity.HasAlternateKey(e => e.ScheduleId)
                    .HasName("AK_dbo.Schedules");

                entity.Property(e => e.ScheduleId)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => new { e.LeagueId, e.SeasonId }, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SeasonId).HasColumnName("SeasonId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => new { d.LeagueId, d.SeasonId })
                    .HasConstraintName("FK_dbo.Schedules_dbo.Seasons_SeasonId");
            });

            modelBuilder.Entity<ScoredResultEntity>(entity =>
            {
                entity.HasKey(e => new {e.LeagueId,  e.ResultId, e.ScoringId })
                    .HasName("PK_dbo.ScoredResults");

                entity.HasIndex(e => e.FastestAvgLapDriverMemberId, "IX_FastestAvgLapDriver_MemberId");

                entity.HasIndex(e => e.FastestLapDriverMemberId, "IX_FastestLapDriver_MemberId");

                entity.HasIndex(e => e.FastestQualyLapDriverMemberId, "IX_FastestQualyLapDriver_MemberId");

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.HasIndex(e => e.ScoringId, "IX_ScoringId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FastestAvgLapDriverMemberId).HasColumnName("FastestAvgLapDriver_MemberId");

                entity.Property(e => e.FastestLapDriverMemberId).HasColumnName("FastestLapDriver_MemberId");

                entity.Property(e => e.FastestQualyLapDriverMemberId).HasColumnName("FastestQualyLapDriver_MemberId");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.FastestAvgLapDriver)
                    .WithMany(p => p.FastestAvgLapResults)
                    .HasForeignKey(d => d.FastestAvgLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResults_dbo.LeagueMembers_FAvgLapDriver_MemberId");

                entity.HasOne(d => d.FastestLapDriver)
                    .WithMany(p => p.FastestLapResults)
                    .HasForeignKey(d => d.FastestLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResults_dbo.LeagueMembers_FLapDriver_MemberId");

                entity.HasOne(d => d.FastestQualyLapDriver)
                    .WithMany(p => p.FastestQualyLapResults)
                    .HasForeignKey(d => d.FastestQualyLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResults_dbo.LeagueMembers_QLapDriver_MemberId");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ScoredResults)
                    .HasForeignKey(d => new { d.LeagueId, d.ResultId })
                    .HasConstraintName("FK_dbo.ScoredResults_dbo.Results_ResultId");

                entity.HasOne(d => d.Scoring)
                    .WithMany(p => p.ScoredResults)
                    .HasForeignKey(d => new { d.LeagueId, d.ScoringId })
                    .HasConstraintName("FK_dbo.ScoredResults_dbo.Scorings_ScoringId");

                entity.HasMany(d => d.CleanestDrivers)
                    .WithMany(p => p.CleanestDriverResults)
                    .UsingEntity(e => e.ToTable("ScoredResultsCleanestDrivers"));

                entity.HasMany(d => d.HardChargers)
                    .WithMany(p => p.HardChargerResults)
                    .UsingEntity(e => e.ToTable("ScoredResultsHardChargers"));
            });
            
            modelBuilder.Entity<ScoredResultRowEntity>(entity =>
            {
                entity.HasKey(e => new { e.ScoredResultRowId })
                    .HasName("PK_dbo.ScoredResultRows");

                //entity.HasAlternateKey(e => e.ScoredResultRowId)
                //    .HasName("AK_dbo.ScoredResultRows");

                entity.HasIndex(e => e.ResultRowId, "IX_ResultRowId");

                entity.HasIndex(e => new { e.ResultId, e.ScoringId }, "IX_ScoredResultId_ScoringId");

                entity.HasIndex(e => e.TeamId, "IX_TeamId");

                entity.HasOne(d => d.ResultRow)
                    .WithMany(p => p.ScoredResultRows)
                    .HasForeignKey(d => new { d.LeagueId, d.ResultRowId })
                    .HasConstraintName("FK_dbo.ScoredResultRows_dbo.ResultRows_ResultRowId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ScoredResultRows)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_dbo.ScoredResultRows_dbo.Teams_TeamId");

                entity.HasOne(d => d.Scoring)
                    .WithMany(p => p.ScoredResultRows)
                    .HasForeignKey(d => new {d.LeagueId, d.ResultId, d.ScoringId })
                    .HasConstraintName("FK_dbo.ScrResultRows_dbo.ScrResults_ScrResultId_ScoringId");
            });

            modelBuilder.Entity<ScoredTeamResultRowEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.ScoredResultRowId })
                    .HasName("PK_dbo.ScoredTeamResultRows");

                entity.HasIndex(e => e.ScoredResultRowId, "IX_ScoredResultRowdId");

                entity.HasIndex(e => new { e.ScoredResultId, e.ScoringId }, "IX_ScoredResultId_ScoringId");

                entity.HasIndex(e => e.TeamId, "IX_TeamId");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ScoredTeamResultRows)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredTeamResultRows_dbo.Teams_TeamId");

                entity.HasOne(d => d.ScoredResult)
                    .WithMany(p => p.ScoredTeamResultRows)
                    .HasForeignKey(d => new {d.LeagueId, d.ScoredResultId, d.ScoringId })
                    .HasConstraintName("FK_dbo.ScrTeamResultRows_dbo.ScrResults_ScrResultId_ScoringId");

                entity.HasMany(d => d.ScoredResultRows)
                    .WithMany(p => p.ScoredTeamResultRows)
                    .UsingEntity(e => e.ToTable("ScoredTeamResultRowsResultRows"));
            });

            modelBuilder.Entity<ScoringEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.ScoringId })
                    .HasName("PK_dbo.Scorings");

                entity.HasAlternateKey(e => e.ScoringId)
                    .HasName("AK_dbo.Scorings");

                entity.Property(e => e.ScoringId)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.ConnectedScheduleId, "IX_ConnectedScheduleId");

                entity.HasIndex(e => e.ExtScoringSourceId, "IX_ExtScoringSourceId");

                entity.HasIndex(e => e.ParentScoringId, "IX_ParentScoringId");

                entity.HasIndex(e => new { e.LeagueId, e.SeasonId}, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ShowResults)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ConnectedSchedule)
                    .WithMany(p => p.Scorings)
                    .HasForeignKey(d => new { d.LeagueId, d.ConnectedScheduleId })
                    .HasConstraintName("FK_dbo.Scorings_dbo.Schedules_ConnectedSchedule_ScheduleId");

                entity.HasOne(d => d.ExtScoringSource)
                    .WithMany()
                    .HasForeignKey(d => new { d.LeagueId, d.ExtScoringSourceId })
                    .HasConstraintName("FK_dbo.Scorings_dbo.Scorings_ExtScoringSourceId");

                entity.HasOne(d => d.ParentScoring)
                    .WithMany(p => p.DependendScorings)
                    .HasForeignKey(d => new { d.LeagueId, d.ParentScoringId })
                    .HasConstraintName("FK_dbo.Scorings_dbo.Scorings_ParentScoringId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Scorings)
                    .HasForeignKey(d => new { d.LeagueId, d.SeasonId })
                    .HasConstraintName("FK_dbo.Scorings_dbo.Seasons_Season_SeasonId");

                entity.HasMany(d => d.Sessions)
                    .WithMany(p => p.Scorings)
                    .UsingEntity<ScoringsSessions>(
                        left => left.HasOne(e => e.SessionRef)
                            .WithMany().HasForeignKey(e => new { e.LeagueId, e.SessionRefId }),
                        right => right.HasOne(e => e.ScoringRef)
                            .WithMany().HasForeignKey(e => new { e.LeagueId, e.ScoringRefId }));
            });

            modelBuilder.Entity<StandingEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.StandingId })
                    .HasName("PK_dbo.Standings");

                entity.HasIndex(e => new { e.LeagueId, e.SeasonId }, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Standings)
                    .HasForeignKey(d => new { d.LeagueId, d.SeasonId })
                    .HasConstraintName("FK_dbo.Standings_dbo.Seasons_Season_SeasonId");

                entity.HasMany(d => d.Scorings)
                    .WithMany(p => p.Standings)
                    .UsingEntity<StandingsScorings>(
                        left => left.HasOne(d => d.ScoringRef)
                            .WithMany().HasForeignKey(e => new { e.LeagueId, e.ScoringRefId })
                            .HasConstraintName("FK_dbStandingsScorings_ScoringRefId"),
                        right => right.HasOne(d => d.StandingRef)
                            .WithMany().HasForeignKey(e => new { e.LeagueId, e.StandingRefId })
                            .HasConstraintName("FK_dbStandingsScorings_StandingRefId"));
            });

            modelBuilder.Entity<LeagueEntity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_dbo.Leagues");

                entity.HasAlternateKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(85);
            });

            modelBuilder.Entity<SeasonEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.SeasonId } )
                    .HasName("PK_dbo.Seasons");

                entity.HasAlternateKey(e => e.SeasonId)
                    .HasName("AK_dbo.Seasons");

                entity.Property(e => e.SeasonId)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.MainScoringScoringId, "IX_MainScoring_ScoringId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.MainScoringScoringId).HasColumnName("MainScoring_ScoringId");

                entity.Property(e => e.SeasonEnd).HasColumnType("datetime");

                entity.Property(e => e.SeasonStart).HasColumnType("datetime");

                entity.HasOne(e => e.League)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(e => e.LeagueId);

                entity.HasOne(d => d.MainScoring)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(d => new { d.LeagueId, d.MainScoringScoringId })
                    .HasConstraintName("FK_dbo.Seasons_dbo.Scorings_MainScoring_ScoringId");
            });

            modelBuilder.Entity<SessionEntity>(entity =>
            {
                entity.HasKey(e => new { e.LeagueId, e.SessionId })
                    .HasName("PK_dbo.Sessions");

                entity.HasAlternateKey(e => e.SessionId)
                    .HasName("AK_dbo.Sessions");

                entity.Property(e => e.SessionId)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.ParentSessionId, "IX_ParentSessionId");

                entity.HasIndex(e => new { e.LeagueId, e.ScheduleId }, "IX_ScheduleId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SessionType)
                    .HasConversion<string>();

                entity.HasOne(d => d.ParentSession)
                    .WithMany(p => p.SubSessions)
                    .HasForeignKey(d => new { d.LeagueId, d.ParentSessionId })
                    .HasConstraintName("FK_dbo.Sessions_dbo.Sessions_ParentSessionId");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => new { d.LeagueId, d.ScheduleId })
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.Sessions_dbo.Schedules_Schedule_ScheduleId");

                entity.HasOne(d => d.Track)
                    .WithMany()
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_dbo.Sessions_dbo.TrackConfigs_TrackId")
                    .IsRequired(false);
            });

            modelBuilder.Entity<StatisticSetEntity>(entity =>
            {
                entity.HasIndex(e => e.CurrentChampId, "IX_CurrentChampId");

                entity.HasIndex(e => new { e.LeagueId, e.StandingId }, "IX_StandingId");

                entity.HasIndex(e => new { e.LeagueId, e.SeasonId }, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FirstDate).HasColumnType("datetime");

                entity.Property(e => e.LastDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.CurrentChamp)
                    .WithMany(p => p.StatisticSets)
                    .HasForeignKey(d => d.CurrentChampId)
                    .HasConstraintName("FK_dbo.StatisticSets_dbo.LeagueMembers_CurrentChampId");

                entity.HasOne(d => d.Standing)
                    .WithMany(p => p.StatisticSets)
                    .HasForeignKey(d => new { d.LeagueId, d.StandingId })
                    .HasConstraintName("FK_dbo.StatisticSets_dbo.Standings_StandingId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.StatisticSets)
                    .HasForeignKey(d => new { d.LeagueId, d.SeasonId })
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.StatisticSets_dbo.Seasons_SeasonId");

                entity.HasMany(d => d.DependendStatisticSets)
                    .WithMany(p => p.LeagueStatisticSets)
                    .UsingEntity(e => e.ToTable("LeagueStatisticSetsStatisticSets"));
            });

            modelBuilder.Entity<TeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .HasName("PK_dbo.Teams");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<VoteCategoryEntity>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK_dbo.VoteCategorys");
            });

            modelBuilder.Entity<TrackGroupEntity>(entity =>
            {
                entity.HasKey(e => e.TrackGroupId)
                    .HasName("PK_dbo.TrackGroups");

                entity.HasMany(d => d.TrackConfigs)
                    .WithOne(p => p.TrackGroup)
                    .HasForeignKey(d => d.TrackGroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.TrackGroups_dbo.TrackConfigs_TrackGroupId");
            });

            modelBuilder.Entity<TrackConfigEntity>(entity =>
            {
                entity.HasKey(e => e.TrackId)
                    .HasName("PK_dbo.TrackConfigs");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
