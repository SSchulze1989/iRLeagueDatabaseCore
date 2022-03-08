using System;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual DbSet<AcceptedReviewVoteEntity> AcceptedReviewVoteEntities { get; set; }
        public virtual DbSet<AddPenaltyEntity> AddPenaltyEntities { get; set; }
        public virtual DbSet<CommentBaseEntity> CommentBaseEntities { get; set; }
        public virtual DbSet<CommentReviewVoteEntity> CommentReviewVoteEntities { get; set; }
        public virtual DbSet<CustomIncidentEntity> CustomIncidentEntities { get; set; }
        public virtual DbSet<DriverStatisticRowEntity> DriverStatisticRowEntities { get; set; }
        public virtual DbSet<IncidentReviewEntity> IncidentReviewEntities { get; set; }
        public virtual DbSet<LeagueEntity> Leagues { get; set; }
        public virtual DbSet<MemberEntity> Members { get; set; }
        public virtual DbSet<ResultEntity> ResultEntities { get; set; }
        public virtual DbSet<ResultRowEntity> ResultRowEntities { get; set; }
        public virtual DbSet<ResultsFilterOptionEntity> ResultsFilterOptionEntities { get; set; }
        public virtual DbSet<ReviewPenaltyEntity> ReviewPenaltyEntities { get; set; }
        public virtual DbSet<ScheduleEntity> ScheduleEntities { get; set; }
        public virtual DbSet<ScoredResultEntity> ScoredResultEntities { get; set; }
        public virtual DbSet<ScoredResultRowEntity> ScoredResultRowEntities { get; set; }
        public virtual DbSet<ScoredTeamResultRowEntity> ScoredTeamResultRowEntities { get; set; }
        public virtual DbSet<ScoringEntity> ScoringEntities { get; set; }
        public virtual DbSet<ScoringTableEntity> ScoringTableEntities { get; set; }
        public virtual DbSet<ScoringTableMap> ScoringTableMaps { get; set; }
        public virtual DbSet<SeasonEntity> SeasonEntities { get; set; }
        public virtual DbSet<SessionEntity> SessionEntities { get; set; }
        public virtual DbSet<StatisticSetEntity> StatisticSetEntities { get; set; }
        public virtual DbSet<TeamEntity> TeamEntities { get; set; }
        public virtual DbSet<VoteCategoryEntity> VoteCategoryEntities { get; set; }
        public virtual DbSet<TrackGroupEntity> TrackGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\IRLEAGUEDB;Database=LeagueDbCore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AcceptedReviewVoteEntity>(entity =>
            {
                entity.HasKey(e => e.ReviewVoteId)
                    .HasName("PK_dbo.AcceptedReviewVoteEntities");

                entity.HasIndex(e => e.CustomVoteCatId, "IX_CustomVoteCatId");

                entity.HasIndex(e => e.MemberAtFaultId, "IX_MemberAtFaultId");

                entity.HasIndex(e => e.ReviewId, "IX_ReviewId");

                entity.HasOne(d => d.CustomVoteCat)
                    .WithMany(p => p.AcceptedReviewVoteEntities)
                    .HasForeignKey(d => d.CustomVoteCatId)
                    .HasConstraintName("FK_dbo.AcceptedReviewVoteEntities_dbo.VoteCategoryEntities_CustomVoteCatId");

                entity.HasOne(d => d.MemberAtFault)
                    .WithMany(p => p.AcceptedReviewVoteEntities)
                    .HasForeignKey(d => d.MemberAtFaultId)
                    .HasConstraintName("FK_dbo.AcceptedReviewVoteEntities_dbo.LeagueMemberEntities_MemberAtFaultId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.AcceptedReviewVoteEntities)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_dbo.AcceptedReviewVoteEntities_dbo.IncidentReviewEntities_ReviewId");
            });

            modelBuilder.Entity<AddPenaltyEntity>(entity =>
            {
                entity.HasKey(e => e.ScoredResultRowId)
                    .HasName("PK_dbo.AddPenaltyEntities");

                entity.HasIndex(e => e.ScoredResultRowId, "IX_ScoredResultRowId");

                entity.Property(e => e.ScoredResultRowId).ValueGeneratedNever();

                entity.HasOne(d => d.ScoredResultRow)
                    .WithOne(p => p.AddPenaltyEntity)
                    .HasForeignKey<AddPenaltyEntity>(d => d.ScoredResultRowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.AddPenaltyEntities_dbo.ScoredResultRowEntities_ScoredResultRowId");
            });

            modelBuilder.Entity<CommentBaseEntity>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_dbo.CommentBaseEntities");

                entity.HasIndex(e => e.ReplyToCommentId, "IX_ReplyToCommentId");

                entity.HasIndex(e => e.ReviewId, "IX_ReviewId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ReplyToComment)
                    .WithMany(p => p.InverseReplyToComment)
                    .HasForeignKey(d => d.ReplyToCommentId)
                    .HasConstraintName("FK_dbo.CommentBaseEntities_dbo.CommentBaseEntities_ReplyToCommentId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.CommentBaseEntities)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.CommentBaseEntities_dbo.IncidentReviewEntities_ReviewId");
            });

            modelBuilder.Entity<CommentReviewVoteEntity>(entity =>
            {
                entity.HasKey(e => e.ReviewVoteId)
                    .HasName("PK_dbo.CommentReviewVoteEntities");

                entity.HasIndex(e => e.CommentId, "IX_CommentId");

                entity.HasIndex(e => e.CustomVoteCatId, "IX_CustomVoteCatId");

                entity.HasIndex(e => e.MemberAtFaultId, "IX_MemberAtFaultId");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReviewVoteEntities)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_dbo.CommentReviewVoteEntities_dbo.CommentBaseEntities_CommentId");

                entity.HasOne(d => d.CustomVoteCat)
                    .WithMany(p => p.CommentReviewVoteEntities)
                    .HasForeignKey(d => d.CustomVoteCatId)
                    .HasConstraintName("FK_dbo.CommentReviewVoteEntities_dbo.VoteCategoryEntities_CustomVoteCatId");

                entity.HasOne(d => d.MemberAtFault)
                    .WithMany(p => p.CommentReviewVoteEntities)
                    .HasForeignKey(d => d.MemberAtFaultId)
                    .HasConstraintName("FK_dbo.CommentReviewVoteEntities_dbo.LeagueMemberEntities_MemberAtFaultId");
            });

            modelBuilder.Entity<CustomIncidentEntity>(entity =>
            {
                entity.HasKey(e => e.IncidentId)
                    .HasName("PK_dbo.CustomIncidentEntities");
            });

            modelBuilder.Entity<DriverStatisticRowEntity>(entity =>
            {
                entity.HasKey(e => new { e.StatisticSetId, e.MemberId })
                    .HasName("PK_dbo.DriverStatisticRowEntities");

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
                    .WithMany(p => p.DriverStatisticRowEntityFirstRaces)
                    .HasForeignKey(d => d.FirstRaceId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionEntities_FirstRaceId");

                entity.HasOne(d => d.FirstResultRow)
                    .WithMany(p => p.DriverStatisticRowEntityFirstResultRows)
                    .HasForeignKey(d => d.FirstResultRowId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.ScoredResultRowEntities_FirstResultRowId");

                entity.HasOne(d => d.FirstSession)
                    .WithMany(p => p.DriverStatisticRowEntityFirstSessions)
                    .HasForeignKey(d => d.FirstSessionId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionEntities_FirstSessionId");

                entity.HasOne(d => d.LastRace)
                    .WithMany(p => p.DriverStatisticRowEntityLastRaces)
                    .HasForeignKey(d => d.LastRaceId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionEntities_LastRaceId");

                entity.HasOne(d => d.LastResultRow)
                    .WithMany(p => p.DriverStatisticRowEntityLastResultRows)
                    .HasForeignKey(d => d.LastResultRowId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.ScoredResultRowEntities_LastResultRowId");

                entity.HasOne(d => d.LastSession)
                    .WithMany(p => p.DriverStatisticRowEntityLastSessions)
                    .HasForeignKey(d => d.LastSessionId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionEntities_LastSessionId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.DriverStatisticRowEntities)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.LeagueMemberEntities_MemberId");

                entity.HasOne(d => d.StatisticSet)
                    .WithMany(p => p.DriverStatisticRowEntities)
                    .HasForeignKey(d => d.StatisticSetId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.StatisticSetEntities_StatisticSetId");
            });

            modelBuilder.Entity<IncidentReviewEntity>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK_dbo.IncidentReviewEntities");

                entity.HasIndex(e => e.SessionId, "IX_SessionId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.IncidentReviewEntities)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_dbo.IncidentReviewEntities_dbo.SessionEntities_SessionId");

                entity.HasMany(d => d.InvolvedMembers)
                    .WithMany(p => p.InvolvedReviews);
            });

            modelBuilder.Entity<MemberEntity>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_dbo.LeagueMemberEntities");

            });

            modelBuilder.Entity<ResultEntity>(entity =>
            {
                entity.HasKey(e => e.ResultId)
                    .HasName("PK_dbo.ResultEntities");

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.HasIndex(e => e.SeasonId, "IX_SeasonId");

                entity.Property(e => e.ResultId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithOne(p => p.ResultEntity)
                    .HasForeignKey<ResultEntity>(d => d.ResultId)
                    .HasConstraintName("FK_dbo.ResultEntities_dbo.SessionEntities_ResultId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_dbo.ResultEntities_dbo.SeasonEntities_SeasonId");

                entity.HasOne(d => d.IRSimSessionDetails)
                    .WithOne(p => p.Result);
            });

            modelBuilder.Entity<IRSimSessionDetailsEntity>(entity =>
            {
                entity.HasKey(e => e.ResultId);

                entity.Navigation(d => d.Result).UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SimStartUtcOffset).HasColumnName("SimStartUTCOffset");

                entity.Property(e => e.SimStartUtcTime)
                    .HasColumnType("datetime")
                    .HasColumnName("SimStartUTCTime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(e => e.Result)
                    .WithOne(p => p.IRSimSessionDetails)
                    .HasForeignKey<IRSimSessionDetailsEntity>(d => d.ResultId)
                    .IsRequired()
                    .HasConstraintName("FK_dbo.IRSimSessionDetailsEntities_dbo.ResultEntities_ResultId");
            });

            modelBuilder.Entity<ResultRowEntity>(entity =>
            {
                entity.HasKey(e => e.ResultRowId)
                    .HasName("PK_dbo.ResultRowEntities");

                entity.HasIndex(e => e.MemberId, "IX_MemberId");

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.HasIndex(e => e.TeamId, "IX_TeamId");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.IracingId).HasColumnName("IRacingId");

                entity.Property(e => e.NewIrating).HasColumnName("NewIRating");

                entity.Property(e => e.OldIrating).HasColumnName("OldIRating");

                entity.Property(e => e.PointsEligible)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QualifyingTimeAt).HasColumnType("datetime");

                entity.Property(e => e.SeasonStartIrating).HasColumnName("SeasonStartIRating");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ResultRowEntities)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ResultRowEntities_dbo.LeagueMemberEntities_MemberId");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ResultRows)
                    .HasForeignKey(d => d.ResultId)
                    .HasConstraintName("FK_dbo.ResultRowEntities_dbo.ResultEntities_ResultId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ResultRowEntities)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_dbo.ResultRowEntities_dbo.TeamEntities_TeamId");
            });

            modelBuilder.Entity<ResultsFilterOptionEntity>(entity =>
            {
                entity.HasKey(e => e.ResultsFilterId)
                    .HasName("PK_dbo.ResultsFilterOptionEntities");

                entity.HasIndex(e => e.ScoringId, "IX_ScoringId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Scoring)
                    .WithMany(p => p.ResultsFilterOptionEntities)
                    .HasForeignKey(d => d.ScoringId)
                    .HasConstraintName("FK_dbo.ResultsFilterOptionEntities_dbo.ScoringEntities_ScoringId");
            });

            modelBuilder.Entity<ReviewPenaltyEntity>(entity =>
            {
                entity.HasKey(e => new { e.ResultRowId, e.ReviewId })
                    .HasName("PK_dbo.ReviewPenaltyEntities");

                entity.HasIndex(e => e.ResultRowId, "IX_ResultRowId");

                entity.HasIndex(e => e.ReviewId, "IX_ReviewId");

                entity.HasIndex(e => e.ReviewVoteId, "IX_ReviewVoteId");

                entity.HasOne(d => d.ResultRow)
                    .WithMany(p => p.ReviewPenaltyEntities)
                    .HasForeignKey(d => d.ResultRowId)
                    .HasConstraintName("FK_dbo.ReviewPenaltyEntities_dbo.ScoredResultRowEntities_ResultRowId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewPenaltyEntities)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ReviewPenaltyEntities_dbo.IncidentReviewEntities_ReviewId");

                entity.HasOne(d => d.ReviewVote)
                    .WithMany(p => p.ReviewPenaltyEntities)
                    .HasForeignKey(d => d.ReviewVoteId)
                    .HasConstraintName("FK_dbo.ReviewPenaltyEntities_dbo.AcceptedReviewVoteEntities_ReviewVoteId");
            });

            modelBuilder.Entity<ScheduleEntity>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK_dbo.ScheduleEntities");

                entity.HasIndex(e => e.SeasonId, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SeasonId).HasColumnName("SeasonId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_dbo.ScheduleEntities_dbo.SeasonEntities_SeasonId")
                    .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Property);

                entity.HasOne(d => d.League)
                    .WithMany()
                    .HasForeignKey(d => d.LeagueId)
                    .HasConstraintName("FK_dbo.ScheduleEntities_dbo.LeagueEntities_LeagueId");
            });

            modelBuilder.Entity<ScoredResultEntity>(entity =>
            {
                entity.HasKey(e => new { e.ResultId, e.ScoringId })
                    .HasName("PK_dbo.ScoredResultEntities");

                entity.HasIndex(e => e.FastestAvgLapDriverMemberId, "IX_FastestAvgLapDriver_MemberId");

                entity.HasIndex(e => e.FastestLapDriverMemberId, "IX_FastestLapDriver_MemberId");

                entity.HasIndex(e => e.FastestQualyLapDriverMemberId, "IX_FastestQualyLapDriver_MemberId");

                entity.HasIndex(e => e.ResultId, "IX_ResultId");

                entity.HasIndex(e => e.ScoringId, "IX_ScoringId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FastestAvgLapDriverMemberId).HasColumnName("FastestAvgLapDriver_MemberId");

                entity.Property(e => e.FastestLapDriverMemberId).HasColumnName("FastestLapDriver_MemberId");

                entity.Property(e => e.FastestQualyLapDriverMemberId).HasColumnName("FastestQualyLapDriver_MemberId");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.FastestAvgLapDriver)
                    .WithMany(p => p.FastestAvgLapResults)
                    .HasForeignKey(d => d.FastestAvgLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestAvgLapDriver_MemberId");

                entity.HasOne(d => d.FastestLapDriver)
                    .WithMany(p => p.FastestLapResults)
                    .HasForeignKey(d => d.FastestLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestLapDriver_MemberId");

                entity.HasOne(d => d.FastestQualyLapDriver)
                    .WithMany(p => p.FastestQualyLapResults)
                    .HasForeignKey(d => d.FastestQualyLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestQualyLapDriver_MemberId");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ScoredResults)
                    .HasForeignKey(d => d.ResultId)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.ResultEntities_ResultId");

                entity.HasOne(d => d.Scoring)
                    .WithMany(p => p.ScoredResultEntities)
                    .HasForeignKey(d => d.ScoringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.ScoringEntities_ScoringId");

                entity.HasMany(d => d.CleanestDrivers)
                    .WithMany(p => p.CleanestDriverResults);

                entity.HasMany(d => d.HardChargers)
                    .WithMany(p => p.HardChargerResults);
            });

            modelBuilder.Entity<ScoredResultRowEntity>(entity =>
            {
                entity.HasKey(e => e.ScoredResultRowId)
                    .HasName("PK_dbo.ScoredResultRowEntities");

                entity.HasIndex(e => e.ResultRowId, "IX_ResultRowId");

                entity.HasIndex(e => new { e.ScoredResultId, e.ScoringId }, "IX_ScoredResultId_ScoringId");

                entity.HasIndex(e => e.TeamId, "IX_TeamId");

                entity.HasOne(d => d.ResultRow)
                    .WithMany(p => p.ScoredResultRowEntities)
                    .HasForeignKey(d => d.ResultRowId)
                    .HasConstraintName("FK_dbo.ScoredResultRowEntities_dbo.ResultRowEntities_ResultRowId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ScoredResultRowEntities)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_dbo.ScoredResultRowEntities_dbo.TeamEntities_TeamId");

                entity.HasOne(d => d.Scor)
                    .WithMany(p => p.ScoredResultRows)
                    .HasForeignKey(d => new { d.ScoredResultId, d.ScoringId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredResultRowEntities_dbo.ScoredResultEntities_ScoredResultId_ScoringId");
            });

            modelBuilder.Entity<ScoredTeamResultRowEntity>(entity =>
            {
                entity.HasKey(e => e.ScoredResultRowId)
                    .HasName("PK_dbo.ScoredTeamResultRowEntities");

                entity.HasIndex(e => new { e.ScoredResultId, e.ScoringId }, "IX_ScoredResultId_ScoringId");

                entity.HasIndex(e => e.TeamId, "IX_TeamId");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ScoredTeamResultRowEntities)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredTeamResultRowEntities_dbo.TeamEntities_TeamId");

                entity.HasOne(d => d.ScoredResult)
                    .WithMany(p => p.ScoredTeamResultRows)
                    .HasForeignKey(d => new { d.ScoredResultId, d.ScoringId })
                    .HasConstraintName("FK_dbo.ScoredTeamResultRowEntities_dbo.ScoredResultEntities_ScoredResultId_ScoringId");

                entity.HasMany(d => d.ScoredResultRows)
                    .WithMany(p => p.ScoredTeamResultRows);
            });

            modelBuilder.Entity<ScoringEntity>(entity =>
            {
                entity.HasKey(e => e.ScoringId)
                    .HasName("PK_dbo.ScoringEntities");

                entity.HasIndex(e => e.ConnectedScheduleId, "IX_ConnectedScheduleId");

                entity.HasIndex(e => e.ExtScoringSourceId, "IX_ExtScoringSourceId");

                entity.HasIndex(e => e.ParentScoringId, "IX_ParentScoringId");

                entity.HasIndex(e => e.SeasonId, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ShowResults)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ConnectedSchedule)
                    .WithMany(p => p.Scorings)
                    .HasForeignKey(d => d.ConnectedScheduleId)
                    .HasConstraintName("FK_dbo.ScoringEntities_dbo.ScheduleEntities_ConnectedSchedule_ScheduleId");

                entity.HasOne(d => d.ExtScoringSource)
                    .WithMany(p => p.InverseExtScoringSource)
                    .HasForeignKey(d => d.ExtScoringSourceId)
                    .HasConstraintName("FK_dbo.ScoringEntities_dbo.ScoringEntities_ExtScoringSourceId");

                entity.HasOne(d => d.ParentScoring)
                    .WithMany(p => p.InverseParentScoring)
                    .HasForeignKey(d => d.ParentScoringId)
                    .HasConstraintName("FK_dbo.ScoringEntities_dbo.ScoringEntities_ParentScoringId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Scorings)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_dbo.ScoringEntities_dbo.SeasonEntities_Season_SeasonId");
            });

            modelBuilder.Entity<ScoringTableEntity>(entity =>
            {
                entity.HasKey(e => e.ScoringTableId)
                    .HasName("PK_dbo.ScoringTableEntities");

                entity.HasIndex(e => e.SeasonId, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.ScoringTables)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_dbo.ScoringTableEntities_dbo.SeasonEntities_Season_SeasonId");
            });

            modelBuilder.Entity<ScoringTableMap>(entity =>
            {
                entity.HasKey(e => new { e.ScoringTableRefId, e.ScoringRefId })
                    .HasName("PK_dbo.ScoringTableMap");

                entity.ToTable("ScoringTableMap");

                entity.HasIndex(e => e.ScoringRefId, "IX_ScoringRefId");

                entity.HasIndex(e => e.ScoringTableRefId, "IX_ScoringTableRefId");

                entity.HasOne(d => d.ScoringRef)
                    .WithMany(p => p.ScoringTableMaps)
                    .HasForeignKey(d => d.ScoringRefId)
                    .HasConstraintName("FK_dbo.ScoringTableMap_dbo.ScoringEntities_ScoringRefId");

                entity.HasOne(d => d.ScoringTableRef)
                    .WithMany(p => p.ScoringTableMaps)
                    .HasForeignKey(d => d.ScoringTableRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoringTableMap_dbo.ScoringTableEntities_ScoringTableRefId");
            });

            modelBuilder.Entity<LeagueEntity>(entity =>
            {
                entity.HasKey(e => e.LeagueId)
                    .HasName("PK_dbo.LeagueEntities");
            });

            modelBuilder.Entity<SeasonEntity>(entity =>
            {
                entity.HasKey(e => e.SeasonId )
                    .HasName("PK_dbo.SeasonEntities");

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
                    .WithMany(p => p.SeasonEntities)
                    .HasForeignKey(d => d.MainScoringScoringId)
                    .HasConstraintName("FK_dbo.SeasonEntities_dbo.ScoringEntities_MainScoring_ScoringId");
            });

            modelBuilder.Entity<SessionEntity>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK_dbo.SessionEntities");

                entity.HasIndex(e => e.ParentSessionId, "IX_ParentSessionId");

                entity.HasIndex(e => e.ScheduleId, "IX_ScheduleId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ParentSession)
                    .WithMany(p => p.InverseParentSession)
                    .HasForeignKey(d => d.ParentSessionId)
                    .HasConstraintName("FK_dbo.SessionEntities_dbo.SessionEntities_ParentSessionId");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.SessionEntities_dbo.ScheduleEntities_Schedule_ScheduleId");

                entity.HasOne(d => d.Track)
                    .WithMany()
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_dbo.SessionEntities_dbo.TrackConfigEntities_TrackId");

                entity.HasMany(d => d.Scorings)
                    .WithMany(p => p.Sessions);
            });

            modelBuilder.Entity<StatisticSetEntity>(entity =>
            {
                entity.HasIndex(e => e.CurrentChampId, "IX_CurrentChampId");

                entity.HasIndex(e => e.ScoringTableId, "IX_ScoringTableId");

                entity.HasIndex(e => e.SeasonId, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FirstDate).HasColumnType("datetime");

                entity.Property(e => e.LastDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.CurrentChamp)
                    .WithMany(p => p.StatisticSetEntities)
                    .HasForeignKey(d => d.CurrentChampId)
                    .HasConstraintName("FK_dbo.StatisticSetEntities_dbo.LeagueMemberEntities_CurrentChampId");

                entity.HasOne(d => d.ScoringTable)
                    .WithMany(p => p.StatisticSetEntities)
                    .HasForeignKey(d => d.ScoringTableId)
                    .HasConstraintName("FK_dbo.StatisticSetEntities_dbo.ScoringTableEntities_ScoringTableId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.StatisticSets)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.StatisticSetEntities_dbo.SeasonEntities_SeasonId");

                entity.HasMany(d => d.LeagueStatisticSets)
                    .WithMany(p => p.DependendStatisticSets);
            });

            modelBuilder.Entity<TeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .HasName("PK_dbo.TeamEntities");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<VoteCategoryEntity>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK_dbo.VoteCategoryEntities");
            });

            modelBuilder.Entity<TrackGroupEntity>(entity =>
            {
                entity.HasKey(e => e.TrackGroupId)
                    .HasName("PK_dbo.TrackGroupEntities");

                entity.HasMany(d => d.TrackConfigs)
                    .WithOne(p => p.TrackGroup)
                    .HasForeignKey(d => d.TrackGroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.TrackGroupEntities_dbo.TrackConfigEntities_TrackGroupId");
            });

            modelBuilder.Entity<TrackConfigEntity>(entity =>
            {
                entity.HasKey(e => e.TrackId)
                    .HasName("PK_dbo.TrackConfigEntities");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
