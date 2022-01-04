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
        public virtual DbSet<IncidentReviewInvolvedLeagueMember> IncidentReviewInvolvedLeagueMembers { get; set; }
        public virtual DbSet<LeagueEntity> Leagues { get; set; }
        public virtual DbSet<MemberEntity> LeagueMemberEntities { get; set; }
        public virtual DbSet<LeagueStatisticSetSeasonStatisticSet> LeagueStatisticSetSeasonStatisticSets { get; set; }
        public virtual DbSet<ResultEntity> ResultEntities { get; set; }
        public virtual DbSet<ResultRowEntity> ResultRowEntities { get; set; }
        public virtual DbSet<ResultsFilterOptionEntity> ResultsFilterOptionEntities { get; set; }
        public virtual DbSet<ReviewPenaltyEntity> ReviewPenaltyEntities { get; set; }
        public virtual DbSet<ScheduleEntity> ScheduleEntities { get; set; }
        public virtual DbSet<ScoredResultCleanestDriver> ScoredResultCleanestDrivers { get; set; }
        public virtual DbSet<ScoredResultEntity> ScoredResultEntities { get; set; }
        public virtual DbSet<ScoredResultHardCharger> ScoredResultHardChargers { get; set; }
        public virtual DbSet<ScoredResultRowEntity> ScoredResultRowEntities { get; set; }
        public virtual DbSet<ScoredTeamResultRowEntity> ScoredTeamResultRowEntities { get; set; }
        public virtual DbSet<ScoredTeamResultRowsGroup> ScoredTeamResultRowsGroups { get; set; }
        public virtual DbSet<ScoringEntity> ScoringEntities { get; set; }
        public virtual DbSet<ScoringSession> ScoringSessions { get; set; }
        public virtual DbSet<ScoringTableEntity> ScoringTableEntities { get; set; }
        public virtual DbSet<ScoringTableMap> ScoringTableMaps { get; set; }
        public virtual DbSet<SeasonEntity> SeasonEntities { get; set; }
        public virtual DbSet<SessionBaseEntity> SessionBaseEntities { get; set; }
        public virtual DbSet<StatisticSetEntity> StatisticSetEntities { get; set; }
        public virtual DbSet<TeamEntity> TeamEntities { get; set; }
        public virtual DbSet<VoteCategoryEntity> VoteCategoryEntities { get; set; }

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
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_FirstRaceId");

                entity.HasOne(d => d.FirstResultRow)
                    .WithMany(p => p.DriverStatisticRowEntityFirstResultRows)
                    .HasForeignKey(d => d.FirstResultRowId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.ScoredResultRowEntities_FirstResultRowId");

                entity.HasOne(d => d.FirstSession)
                    .WithMany(p => p.DriverStatisticRowEntityFirstSessions)
                    .HasForeignKey(d => d.FirstSessionId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_FirstSessionId");

                entity.HasOne(d => d.LastRace)
                    .WithMany(p => p.DriverStatisticRowEntityLastRaces)
                    .HasForeignKey(d => d.LastRaceId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_LastRaceId");

                entity.HasOne(d => d.LastResultRow)
                    .WithMany(p => p.DriverStatisticRowEntityLastResultRows)
                    .HasForeignKey(d => d.LastResultRowId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.ScoredResultRowEntities_LastResultRowId");

                entity.HasOne(d => d.LastSession)
                    .WithMany(p => p.DriverStatisticRowEntityLastSessions)
                    .HasForeignKey(d => d.LastSessionId)
                    .HasConstraintName("FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_LastSessionId");

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
                    .HasConstraintName("FK_dbo.IncidentReviewEntities_dbo.SessionBaseEntities_SessionId");
            });

            modelBuilder.Entity<IncidentReviewInvolvedLeagueMember>(entity =>
            {
                entity.HasKey(e => new { e.ReviewRefId, e.MemberRefId })
                    .HasName("PK_dbo.IncidentReview_InvolvedLeagueMember");

                entity.ToTable("IncidentReview_InvolvedLeagueMember");

                entity.HasIndex(e => e.MemberRefId, "IX_MemberRefId");

                entity.HasIndex(e => e.ReviewRefId, "IX_ReviewRefId");

                entity.HasOne(d => d.MemberRef)
                    .WithMany(p => p.IncidentReviewsInvolved)
                    .HasForeignKey(d => d.MemberRefId)
                    .HasConstraintName("FK_dbo.IncidentReview_InvolvedLeagueMember_dbo.LeagueMemberEntities_MemberRefId");

                entity.HasOne(d => d.ReviewRef)
                    .WithMany(p => p.IncidentReviewInvolvedLeagueMembers)
                    .HasForeignKey(d => d.ReviewRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IncidentReview_InvolvedLeagueMember_dbo.IncidentReviewEntities_ReviewRefId");
            });

            modelBuilder.Entity<MemberEntity>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_dbo.LeagueMemberEntities");

            });

            modelBuilder.Entity<LeagueStatisticSetSeasonStatisticSet>(entity =>
            {
                entity.HasKey(e => new { e.LeagueStatisticSetRefId, e.SeasonStatisticSetRefId })
                    .HasName("PK_dbo.LeagueStatisticSet_SeasonStatisticSet");

                entity.ToTable("LeagueStatisticSet_SeasonStatisticSet");

                entity.HasIndex(e => e.LeagueStatisticSetRefId, "IX_LeagueStatisticSetRefId");

                entity.HasIndex(e => e.SeasonStatisticSetRefId, "IX_SeasonStatisticSetRefId");

                entity.HasOne(d => d.LeagueStatisticSetRef)
                    .WithMany(p => p.LeagueStatisticSetSeasonStatisticSetLeagueStatisticSetRefs)
                    .HasForeignKey(d => d.LeagueStatisticSetRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.LeagueStatisticSet_SeasonStatisticSet_dbo.StatisticSetEntities_LeagueStatisticSetRefId");

                entity.HasOne(d => d.SeasonStatisticSetRef)
                    .WithMany(p => p.LeagueStatisticSetSeasonStatisticSetSeasonStatisticSetRefs)
                    .HasForeignKey(d => d.SeasonStatisticSetRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.LeagueStatisticSet_SeasonStatisticSet_dbo.StatisticSetEntities_SeasonStatisticSetRefId");
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
                    .HasConstraintName("FK_dbo.ResultEntities_dbo.SessionBaseEntities_ResultId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.ResultEntities)
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

                entity.HasIndex(e => e.SeasonSeasonId, "IX_Season_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SeasonSeasonId).HasColumnName("Season_SeasonId");

                entity.HasOne(d => d.SeasonSeason)
                    .WithMany(p => p.ScheduleEntities)
                    .HasForeignKey(d => d.SeasonSeasonId)
                    .HasConstraintName("FK_dbo.ScheduleEntities_dbo.SeasonEntities_Season_SeasonId");
            });

            modelBuilder.Entity<ScoredResultCleanestDriver>(entity =>
            {
                entity.HasKey(e => new { e.ResultRefId, e.ScoringRefId, e.LeagueMemberRefId })
                    .HasName("PK_dbo.ScoredResult_CleanestDrivers");

                entity.ToTable("ScoredResult_CleanestDrivers");

                entity.HasIndex(e => e.LeagueMemberRefId, "IX_LeagueMemberRefId");

                entity.HasIndex(e => new { e.ResultRefId, e.ScoringRefId }, "IX_ResultRefId_ScoringRefId");

                entity.HasOne(d => d.LeagueMemberRef)
                    .WithMany(p => p.CleanestDriverResults)
                    .HasForeignKey(d => d.LeagueMemberRefId)
                    .HasConstraintName("FK_dbo.ScoredResult_CleanestDrivers_dbo.LeagueMemberEntities_LeagueMemberRefId");

                entity.HasOne(d => d.ScoredResultEntity)
                    .WithMany(p => p.ScoredResultCleanestDrivers)
                    .HasForeignKey(d => new { d.ResultRefId, d.ScoringRefId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredResult_CleanestDrivers_dbo.ScoredResultEntities_ResultRefId_ScoringRefId");
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

                entity.HasOne(d => d.FastestAvgLapDriverMember)
                    .WithMany(p => p.FastestAvgLapResults)
                    .HasForeignKey(d => d.FastestAvgLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestAvgLapDriver_MemberId");

                entity.HasOne(d => d.FastestLapDriverMember)
                    .WithMany(p => p.FastestLapResults)
                    .HasForeignKey(d => d.FastestLapDriverMemberId)
                    .HasConstraintName("FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestLapDriver_MemberId");

                entity.HasOne(d => d.FastestQualyLapDriverMember)
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
            });

            modelBuilder.Entity<ScoredResultHardCharger>(entity =>
            {
                entity.HasKey(e => new { e.ResultRefId, e.ScoringRefId, e.MemberRefId })
                    .HasName("PK_dbo.ScoredResult_HardChargers");

                entity.ToTable("ScoredResult_HardChargers");

                entity.HasIndex(e => e.MemberRefId, "IX_LeagueMemberRefId");

                entity.HasIndex(e => new { e.ResultRefId, e.ScoringRefId }, "IX_ResultRefId_ScoringRefId");

                entity.HasOne(d => d.MemberRef)
                    .WithMany(p => p.HardChargerResults)
                    .HasForeignKey(d => d.MemberRefId)
                    .HasConstraintName("FK_dbo.ScoredResult_HardChargers_dbo.LeagueMemberEntities_LeagueMemberRefId");

                entity.HasOne(d => d.ScoredResultEntity)
                    .WithMany(p => p.ScoredResultHardChargers)
                    .HasForeignKey(d => new { d.ResultRefId, d.ScoringRefId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredResult_HardChargers_dbo.ScoredResultEntities_ResultRefId_ScoringRefId");
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
                    .WithMany(p => p.ScoredResultRowEntities)
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

                entity.HasOne(d => d.Scor)
                    .WithMany(p => p.ScoredTeamResultRowEntities)
                    .HasForeignKey(d => new { d.ScoredResultId, d.ScoringId })
                    .HasConstraintName("FK_dbo.ScoredTeamResultRowEntities_dbo.ScoredResultEntities_ScoredResultId_ScoringId");
            });

            modelBuilder.Entity<ScoredTeamResultRowsGroup>(entity =>
            {
                entity.HasKey(e => new { e.ScoredTeamResultRowRefId, e.ScoredResultRowRefId })
                    .HasName("PK_dbo.ScoredTeamResultRowsGroup");

                entity.ToTable("ScoredTeamResultRowsGroup");

                entity.HasIndex(e => e.ScoredResultRowRefId, "IX_ScoredResultRowRefId");

                entity.HasIndex(e => e.ScoredTeamResultRowRefId, "IX_ScoredTeamResultRowRefId");

                entity.HasOne(d => d.ScoredResultRowRef)
                    .WithMany(p => p.ScoredTeamResultRowsGroups)
                    .HasForeignKey(d => d.ScoredResultRowRefId)
                    .HasConstraintName("FK_dbo.ScoredTeamResultRowsGroup_dbo.ScoredResultRowEntities_ScoredResultRowRefId");

                entity.HasOne(d => d.ScoredTeamResultRowRef)
                    .WithMany(p => p.ScoredTeamResultRowsGroups)
                    .HasForeignKey(d => d.ScoredTeamResultRowRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ScoredTeamResultRowsGroup_dbo.ScoredTeamResultRowEntities_ScoredTeamResultRowRefId");
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
                    .WithMany(p => p.ScoringEntities)
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
                    .WithMany(p => p.ScoringEntities)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_dbo.ScoringEntities_dbo.SeasonEntities_Season_SeasonId");
            });

            modelBuilder.Entity<ScoringSession>(entity =>
            {
                entity.HasKey(e => new { e.ScoringRefId, e.SessionRefId })
                    .HasName("PK_dbo.Scoring_Session");

                entity.ToTable("Scoring_Session");

                entity.HasIndex(e => e.ScoringRefId, "IX_ScoringRefId");

                entity.HasIndex(e => e.SessionRefId, "IX_SessionRefId");

                entity.HasOne(d => d.ScoringRef)
                    .WithMany(p => p.ScoringSessions)
                    .HasForeignKey(d => d.ScoringRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Scoring_Session_dbo.ScoringEntities_ScoringRefId");

                entity.HasOne(d => d.SessionRef)
                    .WithMany(p => p.ScoringSessions)
                    .HasForeignKey(d => d.SessionRefId)
                    .HasConstraintName("FK_dbo.Scoring_Session_dbo.SessionBaseEntities_SessionRefId");
            });

            modelBuilder.Entity<ScoringTableEntity>(entity =>
            {
                entity.HasKey(e => e.ScoringTableId)
                    .HasName("PK_dbo.ScoringTableEntities");

                entity.HasIndex(e => e.SeasonId, "IX_SeasonId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.ScoringTableEntities)
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
                    .WithMany()
                    .HasForeignKey(e => e.LeagueId);

                entity.HasOne(d => d.MainScoringScoring)
                    .WithMany(p => p.SeasonEntities)
                    .HasForeignKey(d => d.MainScoringScoringId)
                    .HasConstraintName("FK_dbo.SeasonEntities_dbo.ScoringEntities_MainScoring_ScoringId");
            });

            modelBuilder.Entity<SessionBaseEntity>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK_dbo.SessionBaseEntities");

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
                    .HasConstraintName("FK_dbo.SessionBaseEntities_dbo.SessionBaseEntities_ParentSessionId");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.SessionBaseEntities)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.SessionBaseEntities_dbo.ScheduleEntities_Schedule_ScheduleId");
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
                    .WithMany(p => p.StatisticSetEntities)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.StatisticSetEntities_dbo.SeasonEntities_SeasonId");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
