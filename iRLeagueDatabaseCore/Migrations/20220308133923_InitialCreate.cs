using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "Latin1_General_CI_AS");

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameFull = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.LeagueEntities", x => x.LeagueId);
                });

            migrationBuilder.CreateTable(
                name: "TeamEntities",
                columns: table => new
                {
                    TeamId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Profile = table.Column<string>(type: "text", nullable: true),
                    TeamColor = table.Column<string>(type: "text", nullable: true),
                    TeamHomepage = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TeamEntities", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "TrackGroups",
                columns: table => new
                {
                    TrackGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TrackName = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TrackGroupEntities", x => x.TrackGroupId);
                });

            migrationBuilder.CreateTable(
                name: "VoteCategoryEntities",
                columns: table => new
                {
                    CatId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    DefaultPenalty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.VoteCategoryEntities", x => x.CatId);
                });

            migrationBuilder.CreateTable(
                name: "CustomIncidentEntities",
                columns: table => new
                {
                    IncidentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CustomIncidentEntities", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_CustomIncidentEntities_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    IRacingId = table.Column<string>(type: "text", nullable: true),
                    DanLisaId = table.Column<string>(type: "text", nullable: true),
                    DiscordId = table.Column<string>(type: "text", nullable: true),
                    TeamEntityTeamId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.LeagueMemberEntities", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Members_TeamEntities_TeamEntityTeamId",
                        column: x => x.TeamEntityTeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackConfigEntity",
                columns: table => new
                {
                    TrackId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TrackGroupId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigName = table.Column<string>(type: "text", nullable: true),
                    LengthKm = table.Column<double>(type: "double", nullable: false),
                    Turns = table.Column<int>(type: "int", nullable: false),
                    ConfigType = table.Column<int>(type: "int", nullable: false),
                    HasNigtLigthing = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MapImageSrc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TrackConfigEntities", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_dbo.TrackGroups_dbo.TrackConfigs_TrackGroupId",
                        column: x => x.TrackGroupId,
                        principalTable: "TrackGroups",
                        principalColumn: "TrackGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewPenaltyEntities",
                columns: table => new
                {
                    ResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewId = table.Column<long>(type: "bigint", nullable: false),
                    PenaltyPoints = table.Column<int>(type: "int", nullable: false),
                    ReviewVoteId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ReviewPenaltyEntities", x => new { x.ResultRowId, x.ReviewId });
                });

            migrationBuilder.CreateTable(
                name: "CommentReviewVoteEntities",
                columns: table => new
                {
                    ReviewVoteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CommentId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    MemberAtFaultId = table.Column<long>(type: "bigint", nullable: true),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    CustomVoteCatId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CommentReviewVoteEntities", x => x.ReviewVoteId);
                    table.ForeignKey(
                        name: "FK_dbo.CommentReviewVotes_dbo.LeagueMembers_MemberAtFaultId",
                        column: x => x.MemberAtFaultId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.CommentReviewVotes_dbo.VoteCategorys_CustomVoteCatId",
                        column: x => x.CustomVoteCatId,
                        principalTable: "VoteCategoryEntities",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcceptedReviewVoteEntities",
                columns: table => new
                {
                    ReviewVoteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewId = table.Column<long>(type: "bigint", nullable: false),
                    MemberAtFaultId = table.Column<long>(type: "bigint", nullable: true),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    CustomVoteCatId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AcceptedReviewVoteEntities", x => x.ReviewVoteId);
                    table.ForeignKey(
                        name: "FK_dbo.AcceptedReviewVotes_dbo.LeagueMembers_MemberAtFaultId",
                        column: x => x.MemberAtFaultId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.AcceptedReviewVotes_dbo.VoteCategorys_CustomVoteCatId",
                        column: x => x.CustomVoteCatId,
                        principalTable: "VoteCategoryEntities",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentBaseEntities",
                columns: table => new
                {
                    CommentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    AuthorUserId = table.Column<string>(type: "text", nullable: true),
                    AuthorName = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    ReplyToCommentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    ReviewId = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CommentBaseEntities", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_dbo.CommentBases_dbo.CommentBases_ReplyToCommentId",
                        column: x => x.ReplyToCommentId,
                        principalTable: "CommentBaseEntities",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncidentReviewEntityMemberEntity",
                columns: table => new
                {
                    InvolvedMembersMemberId = table.Column<long>(type: "bigint", nullable: false),
                    InvolvedReviewsReviewId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentReviewEntityMemberEntity", x => new { x.InvolvedMembersMemberId, x.InvolvedReviewsReviewId });
                    table.ForeignKey(
                        name: "FK_IncidentReviewEntityMemberEntity_Members_InvolvedMembersMemb~",
                        column: x => x.InvolvedMembersMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEntities",
                columns: table => new
                {
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScheduleEntities", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_dbo.Schedules_dbo.Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionEntities",
                columns: table => new
                {
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SessionTitle = table.Column<string>(type: "text", nullable: true),
                    SessionType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    TrackId = table.Column<long>(type: "bigint", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    RaceId = table.Column<long>(type: "bigint", nullable: true),
                    Laps = table.Column<int>(type: "int", nullable: true),
                    PracticeLength = table.Column<TimeSpan>(type: "time", nullable: true),
                    QualyLength = table.Column<TimeSpan>(type: "time", nullable: true),
                    RaceLength = table.Column<TimeSpan>(type: "time", nullable: true),
                    IrSessionId = table.Column<string>(type: "text", nullable: true),
                    IrResultLink = table.Column<string>(type: "text", nullable: true),
                    QualyAttached = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    PracticeAttached = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ParentSessionId = table.Column<long>(type: "bigint", nullable: true),
                    SubSessionNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SessionEntities", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_dbo.Sessions_dbo.Schedules_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ScheduleEntities",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Sessions_dbo.Sessions_ParentSessionId",
                        column: x => x.ParentSessionId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Sessions_dbo.TrackConfigs_TrackId",
                        column: x => x.TrackId,
                        principalTable: "TrackConfigEntity",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "IncidentReviewEntities",
                columns: table => new
                {
                    ReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    SessionId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorUserId = table.Column<string>(type: "text", nullable: true),
                    AuthorName = table.Column<string>(type: "text", nullable: true),
                    IncidentKind = table.Column<string>(type: "text", nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    OnLap = table.Column<string>(type: "text", nullable: true),
                    Corner = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    ResultLongText = table.Column<string>(type: "text", nullable: true),
                    IncidentNr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.IncidentReviewEntities", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_dbo.IncidentReviews_dbo.Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonEntities",
                columns: table => new
                {
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    SeasonName = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    MainScoring_ScoringId = table.Column<long>(type: "bigint", nullable: true),
                    HideCommentsBeforeVoted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Finished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SeasonStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    SeasonEnd = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SeasonEntities", x => x.SeasonId);
                    table.ForeignKey(
                        name: "FK_SeasonEntities_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultEntities",
                columns: table => new
                {
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: true),
                    RequiresRecalculation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PoleLaptime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ResultEntities", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_dbo.Results_dbo.Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Results_dbo.Sessions_ResultId",
                        column: x => x.ResultId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringEntities",
                columns: table => new
                {
                    ScoringId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringKind = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DropWeeks = table.Column<int>(type: "int", nullable: false),
                    AverageRaceNr = table.Column<int>(type: "int", nullable: false),
                    MaxResultsPerGroup = table.Column<int>(type: "int", nullable: false),
                    TakeGroupAverage = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExtScoringSourceId = table.Column<long>(type: "bigint", nullable: true),
                    TakeResultsFromExtSource = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BasePoints = table.Column<string>(type: "text", nullable: true),
                    BonusPoints = table.Column<string>(type: "text", nullable: true),
                    IncPenaltyPoints = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    ConnectedScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false),
                    UseResultSetTeam = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UpdateTeamOnRecalculation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParentScoringId = table.Column<long>(type: "bigint", nullable: true),
                    ScoringSessionType = table.Column<int>(type: "int", nullable: false),
                    SessionSelectType = table.Column<int>(type: "int", nullable: false),
                    ScoringWeightValues = table.Column<string>(type: "text", nullable: true),
                    AccumulateBy = table.Column<int>(type: "int", nullable: false),
                    AccumulateResultsOption = table.Column<int>(type: "int", nullable: false),
                    PointsSortOptions = table.Column<string>(type: "text", nullable: true),
                    FinalSortOptions = table.Column<string>(type: "text", nullable: true),
                    ShowResults = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "((1))"),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoringEntities", x => x.ScoringId);
                    table.ForeignKey(
                        name: "FK_dbo.Scorings_dbo.Schedules_ConnectedSchedule_ScheduleId",
                        column: x => x.ConnectedScheduleId,
                        principalTable: "ScheduleEntities",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Scorings_dbo.Scorings_ExtScoringSourceId",
                        column: x => x.ExtScoringSourceId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Scorings_dbo.Scorings_ParentScoringId",
                        column: x => x.ParentScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Scorings_dbo.Seasons_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringTableEntities",
                columns: table => new
                {
                    ScoringTableId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScoringKind = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DropWeeks = table.Column<int>(type: "int", nullable: false),
                    AverageRaceNr = table.Column<int>(type: "int", nullable: false),
                    ScoringFactors = table.Column<string>(type: "text", nullable: true),
                    DropRacesOption = table.Column<int>(type: "int", nullable: false),
                    ResultsPerRaceCount = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoringTableEntities", x => x.ScoringTableId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringTables_dbo.Seasons_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IRSimSessionDetailsEntity",
                columns: table => new
                {
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    IRSubsessionId = table.Column<long>(type: "bigint", nullable: false),
                    IRSeasonId = table.Column<long>(type: "bigint", nullable: false),
                    IRSeasonName = table.Column<string>(type: "text", nullable: true),
                    IRSeasonYear = table.Column<int>(type: "int", nullable: false),
                    IRSeasonQuarter = table.Column<int>(type: "int", nullable: false),
                    IRRaceWeek = table.Column<int>(type: "int", nullable: false),
                    IRSessionId = table.Column<long>(type: "bigint", nullable: false),
                    LicenseCategory = table.Column<int>(type: "int", nullable: false),
                    SessionName = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CornersPerLap = table.Column<int>(type: "int", nullable: false),
                    KmDistPerLap = table.Column<double>(type: "double", nullable: false),
                    MaxWeeks = table.Column<int>(type: "int", nullable: false),
                    EventStrengthOfField = table.Column<int>(type: "int", nullable: false),
                    EventAverageLap = table.Column<long>(type: "bigint", nullable: false),
                    EventLapsComplete = table.Column<int>(type: "int", nullable: false),
                    NumCautions = table.Column<int>(type: "int", nullable: false),
                    NumCautionLaps = table.Column<int>(type: "int", nullable: false),
                    NumLeadChanges = table.Column<int>(type: "int", nullable: false),
                    TimeOfDay = table.Column<int>(type: "int", nullable: false),
                    DamageModel = table.Column<int>(type: "int", nullable: false),
                    IRTrackId = table.Column<int>(type: "int", nullable: false),
                    TrackName = table.Column<string>(type: "text", nullable: true),
                    ConfigName = table.Column<string>(type: "text", nullable: true),
                    TrackCategoryId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    WeatherType = table.Column<int>(type: "int", nullable: false),
                    TempUnits = table.Column<int>(type: "int", nullable: false),
                    TempValue = table.Column<int>(type: "int", nullable: false),
                    RelHumidity = table.Column<int>(type: "int", nullable: false),
                    Fog = table.Column<int>(type: "int", nullable: false),
                    WindDir = table.Column<int>(type: "int", nullable: false),
                    WindUnits = table.Column<int>(type: "int", nullable: false),
                    Skies = table.Column<int>(type: "int", nullable: false),
                    WeatherVarInitial = table.Column<int>(type: "int", nullable: false),
                    WeatherVarOngoing = table.Column<int>(type: "int", nullable: false),
                    SimStartUTCTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    SimStartUTCOffset = table.Column<long>(type: "bigint", nullable: false),
                    LeaveMarbles = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PracticeRubber = table.Column<int>(type: "int", nullable: false),
                    QualifyRubber = table.Column<int>(type: "int", nullable: false),
                    WarmupRubber = table.Column<int>(type: "int", nullable: false),
                    RaceRubber = table.Column<int>(type: "int", nullable: false),
                    PracticeGripCompound = table.Column<int>(type: "int", nullable: false),
                    QualifyGripCompund = table.Column<int>(type: "int", nullable: false),
                    WarmupGripCompound = table.Column<int>(type: "int", nullable: false),
                    RaceGripCompound = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IRSimSessionDetailsEntity", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_dbo.IRSimSessionDetailss_dbo.Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "ResultEntities",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultRowEntities",
                columns: table => new
                {
                    ResultRowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    StartPosition = table.Column<double>(type: "double", nullable: false),
                    FinishPosition = table.Column<double>(type: "double", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    CarNumber = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Car = table.Column<string>(type: "text", nullable: true),
                    CarClass = table.Column<string>(type: "text", nullable: true),
                    CompletedLaps = table.Column<double>(type: "double", nullable: false),
                    LeadLaps = table.Column<double>(type: "double", nullable: false),
                    FastLapNr = table.Column<int>(type: "int", nullable: false),
                    Incidents = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    QualifyingTime = table.Column<long>(type: "bigint", nullable: false),
                    Interval = table.Column<long>(type: "bigint", nullable: false),
                    AvgLapTime = table.Column<long>(type: "bigint", nullable: false),
                    FastestLapTime = table.Column<long>(type: "bigint", nullable: false),
                    PositionChange = table.Column<double>(type: "double", nullable: false),
                    IRacingId = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    SimSessionType = table.Column<int>(type: "int", nullable: false),
                    OldIRating = table.Column<int>(type: "int", nullable: false),
                    NewIRating = table.Column<int>(type: "int", nullable: false),
                    SeasonStartIRating = table.Column<int>(type: "int", nullable: false),
                    License = table.Column<string>(type: "text", nullable: true),
                    OldSafetyRating = table.Column<double>(type: "double", nullable: false),
                    NewSafetyRating = table.Column<double>(type: "double", nullable: false),
                    OldCpi = table.Column<int>(type: "int", nullable: false),
                    NewCpi = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "text", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CompletedPct = table.Column<double>(type: "double", nullable: false),
                    QualifyingTimeAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Division = table.Column<int>(type: "int", nullable: false),
                    OldLicenseLevel = table.Column<int>(type: "int", nullable: false),
                    NewLicenseLevel = table.Column<int>(type: "int", nullable: false),
                    NumPitStops = table.Column<int>(type: "int", nullable: false),
                    PittedLaps = table.Column<string>(type: "text", nullable: true),
                    NumOfftrackLaps = table.Column<int>(type: "int", nullable: false),
                    OfftrackLaps = table.Column<string>(type: "text", nullable: true),
                    NumContactLaps = table.Column<int>(type: "int", nullable: false),
                    ContactLaps = table.Column<string>(type: "text", nullable: true),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    PointsEligible = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ResultRowEntities", x => x.ResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.ResultRows_dbo.LeagueMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ResultRows_dbo.Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "ResultEntities",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ResultRows_dbo.Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResultsFilterOptionEntities",
                columns: table => new
                {
                    ResultsFilterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    ResultsFilterType = table.Column<string>(type: "text", nullable: true),
                    ColumnPropertyName = table.Column<string>(type: "text", nullable: true),
                    Comparator = table.Column<int>(type: "int", nullable: false),
                    Exclude = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FilterValues = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    FilterPointsOnly = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ResultsFilterOptionEntities", x => x.ResultsFilterId);
                    table.ForeignKey(
                        name: "FK_dbo.ResultsFilterOptions_dbo.Scorings_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoredResultEntities",
                columns: table => new
                {
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    FastestLap = table.Column<long>(type: "bigint", nullable: false),
                    FastestQualyLap = table.Column<long>(type: "bigint", nullable: false),
                    FastestAvgLap = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    FastestAvgLapDriver_MemberId = table.Column<long>(type: "bigint", nullable: true),
                    FastestLapDriver_MemberId = table.Column<long>(type: "bigint", nullable: true),
                    FastestQualyLapDriver_MemberId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredResultEntities", x => new { x.ResultId, x.ScoringId });
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResults_dbo.LeagueMembers_FAvgLapDriver_MemberId",
                        column: x => x.FastestAvgLapDriver_MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResults_dbo.LeagueMembers_FLapDriver_MemberId",
                        column: x => x.FastestLapDriver_MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResults_dbo.LeagueMembers_QLapDriver_MemberId",
                        column: x => x.FastestQualyLapDriver_MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResults_dbo.Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "ResultEntities",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResults_dbo.Scorings_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoringEntitySessionEntity",
                columns: table => new
                {
                    ScoringsScoringId = table.Column<long>(type: "bigint", nullable: false),
                    SessionsSessionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringEntitySessionEntity", x => new { x.ScoringsScoringId, x.SessionsSessionId });
                    table.ForeignKey(
                        name: "FK_ScoringEntitySessionEntity_ScoringEntities_ScoringsScoringId",
                        column: x => x.ScoringsScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoringEntitySessionEntity_SessionEntities_SessionsSessionId",
                        column: x => x.SessionsSessionId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringTableMap",
                columns: table => new
                {
                    ScoringTableRefId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoringTableMap", x => new { x.ScoringTableRefId, x.ScoringRefId });
                    table.ForeignKey(
                        name: "FK_dbo.ScoringTableMap_dbo.Scorings_ScoringRefId",
                        column: x => x.ScoringRefId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScrTableMap_dbo.ScrTables_ScrTableRefId",
                        column: x => x.ScoringTableRefId,
                        principalTable: "ScoringTableEntities",
                        principalColumn: "ScoringTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatisticSetEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UpdateInterval = table.Column<long>(type: "bigint", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequiresRecalculation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "text", nullable: true),
                    CurrentChampId = table.Column<long>(type: "bigint", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: true),
                    ScoringTableId = table.Column<long>(type: "bigint", nullable: true),
                    FinishedRaces = table.Column<int>(type: "int", nullable: true),
                    IsSeasonFinished = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ImportSource = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Discriminator = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticSetEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.StatisticSets_dbo.LeagueMembers_CurrentChampId",
                        column: x => x.CurrentChampId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.StatisticSets_dbo.ScoringTables_ScoringTableId",
                        column: x => x.ScoringTableId,
                        principalTable: "ScoringTableEntities",
                        principalColumn: "ScoringTableId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.StatisticSets_dbo.Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberEntityScoredResultEntity",
                columns: table => new
                {
                    CleanestDriversMemberId = table.Column<long>(type: "bigint", nullable: false),
                    CleanestDriverResultsResultId = table.Column<long>(type: "bigint", nullable: false),
                    CleanestDriverResultsScoringId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberEntityScoredResultEntity", x => new { x.CleanestDriversMemberId, x.CleanestDriverResultsResultId, x.CleanestDriverResultsScoringId });
                    table.ForeignKey(
                        name: "FK_MemberEntityScoredResultEntity_Members_CleanestDriversMember~",
                        column: x => x.CleanestDriversMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberEntityScoredResultEntity_ScoredResultEntities_Cleanest~",
                        columns: x => new { x.CleanestDriverResultsResultId, x.CleanestDriverResultsScoringId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberEntityScoredResultEntity1",
                columns: table => new
                {
                    HardChargersMemberId = table.Column<long>(type: "bigint", nullable: false),
                    HardChargerResultsResultId = table.Column<long>(type: "bigint", nullable: false),
                    HardChargerResultsScoringId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberEntityScoredResultEntity1", x => new { x.HardChargersMemberId, x.HardChargerResultsResultId, x.HardChargerResultsScoringId });
                    table.ForeignKey(
                        name: "FK_MemberEntityScoredResultEntity1_Members_HardChargersMemberId",
                        column: x => x.HardChargersMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberEntityScoredResultEntity1_ScoredResultEntities_HardCha~",
                        columns: x => new { x.HardChargerResultsResultId, x.HardChargerResultsScoringId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoredResultRowEntities",
                columns: table => new
                {
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScoredResultId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    ResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    RacePoints = table.Column<double>(type: "double", nullable: false),
                    BonusPoints = table.Column<double>(type: "double", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "double", nullable: false),
                    FinalPosition = table.Column<int>(type: "int", nullable: false),
                    FinalPositionChange = table.Column<double>(type: "double", nullable: false),
                    TotalPoints = table.Column<double>(type: "double", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    PointsEligible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredResultRowEntities", x => x.ScoredResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultRows_dbo.ResultRows_ResultRowId",
                        column: x => x.ResultRowId,
                        principalTable: "ResultRowEntities",
                        principalColumn: "ResultRowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultRows_dbo.Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScrResultRows_dbo.ScrResults_ScrResultId_ScoringId",
                        columns: x => new { x.ScoredResultId, x.ScoringId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoredTeamResultRowEntities",
                columns: table => new
                {
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScoredResultId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CarClass = table.Column<string>(type: "text", nullable: true),
                    RacePoints = table.Column<double>(type: "double", nullable: false),
                    BonusPoints = table.Column<double>(type: "double", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "double", nullable: false),
                    FinalPosition = table.Column<int>(type: "int", nullable: false),
                    FinalPositionChange = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<double>(type: "double", nullable: false),
                    AvgLapTime = table.Column<long>(type: "bigint", nullable: false),
                    FastestLapTime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredTeamResultRowEntities", x => x.ScoredResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredTeamResultRows_dbo.Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScrTeamResultRows_dbo.ScrResults_ScrResultId_ScoringId",
                        columns: x => new { x.ScoredResultId, x.ScoringId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatisticSetEntityStatisticSetEntity",
                columns: table => new
                {
                    DependendStatisticSetsId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueStatisticSetsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticSetEntityStatisticSetEntity", x => new { x.DependendStatisticSetsId, x.LeagueStatisticSetsId });
                    table.ForeignKey(
                        name: "FK_StatisticSetEntityStatisticSetEntity_StatisticSetEntities_De~",
                        column: x => x.DependendStatisticSetsId,
                        principalTable: "StatisticSetEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatisticSetEntityStatisticSetEntity_StatisticSetEntities_Le~",
                        column: x => x.LeagueStatisticSetsId,
                        principalTable: "StatisticSetEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddPenaltyEntities",
                columns: table => new
                {
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    PenaltyPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AddPenaltyEntities", x => x.ScoredResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.AddPenaltys_dbo.ScoredResultRows_ScoredResultRowId",
                        column: x => x.ScoredResultRowId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverStatisticRowEntities",
                columns: table => new
                {
                    StatisticSetId = table.Column<long>(type: "bigint", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    FirstResultRowId = table.Column<long>(type: "bigint", nullable: true),
                    LastResultRowId = table.Column<long>(type: "bigint", nullable: true),
                    StartIRating = table.Column<int>(type: "int", nullable: false),
                    EndIRating = table.Column<int>(type: "int", nullable: false),
                    StartSRating = table.Column<double>(type: "double", nullable: false),
                    EndSRating = table.Column<double>(type: "double", nullable: false),
                    FirstSessionId = table.Column<long>(type: "bigint", nullable: true),
                    FirstSessionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FirstRaceId = table.Column<long>(type: "bigint", nullable: true),
                    FirstRaceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastSessionId = table.Column<long>(type: "bigint", nullable: true),
                    LastSessionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastRaceId = table.Column<long>(type: "bigint", nullable: true),
                    LastRaceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RacePoints = table.Column<double>(type: "double", nullable: false),
                    TotalPoints = table.Column<double>(type: "double", nullable: false),
                    BonusPoints = table.Column<double>(type: "double", nullable: false),
                    Races = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Poles = table.Column<int>(type: "int", nullable: false),
                    Top3 = table.Column<int>(type: "int", nullable: false),
                    Top5 = table.Column<int>(type: "int", nullable: false),
                    Top10 = table.Column<int>(type: "int", nullable: false),
                    Top15 = table.Column<int>(type: "int", nullable: false),
                    Top20 = table.Column<int>(type: "int", nullable: false),
                    Top25 = table.Column<int>(type: "int", nullable: false),
                    RacesInPoints = table.Column<int>(type: "int", nullable: false),
                    RacesCompleted = table.Column<int>(type: "int", nullable: false),
                    Incidents = table.Column<double>(type: "double", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "double", nullable: false),
                    FastestLaps = table.Column<int>(type: "int", nullable: false),
                    IncidentsUnderInvestigation = table.Column<int>(type: "int", nullable: false),
                    IncidentsWithPenalty = table.Column<int>(type: "int", nullable: false),
                    LeadingLaps = table.Column<double>(type: "double", nullable: false),
                    CompletedLaps = table.Column<double>(type: "double", nullable: false),
                    CurrentSeasonPosition = table.Column<int>(type: "int", nullable: false),
                    DrivenKm = table.Column<double>(type: "double", nullable: false),
                    LeadingKm = table.Column<double>(type: "double", nullable: false),
                    AvgFinishPosition = table.Column<double>(type: "double", nullable: false),
                    AvgFinalPosition = table.Column<double>(type: "double", nullable: false),
                    AvgStartPosition = table.Column<double>(type: "double", nullable: false),
                    AvgPointsPerRace = table.Column<double>(type: "double", nullable: false),
                    AvgIncidentsPerRace = table.Column<double>(type: "double", nullable: false),
                    AvgIncidentsPerLap = table.Column<double>(type: "double", nullable: false),
                    AvgIncidentsPerKm = table.Column<double>(type: "double", nullable: false),
                    AvgPenaltyPointsPerRace = table.Column<double>(type: "double", nullable: false),
                    AvgPenaltyPointsPerLap = table.Column<double>(type: "double", nullable: false),
                    AvgPenaltyPointsPerKm = table.Column<double>(type: "double", nullable: false),
                    AvgIRating = table.Column<double>(type: "double", nullable: false),
                    AvgSRating = table.Column<double>(type: "double", nullable: false),
                    BestFinishPosition = table.Column<double>(type: "double", nullable: false),
                    WorstFinishPosition = table.Column<double>(type: "double", nullable: false),
                    FirstRaceFinishPosition = table.Column<double>(type: "double", nullable: false),
                    LastRaceFinishPosition = table.Column<double>(type: "double", nullable: false),
                    BestFinalPosition = table.Column<int>(type: "int", nullable: false),
                    WorstFinalPosition = table.Column<int>(type: "int", nullable: false),
                    FirstRaceFinalPosition = table.Column<int>(type: "int", nullable: false),
                    LastRaceFinalPosition = table.Column<int>(type: "int", nullable: false),
                    BestStartPosition = table.Column<double>(type: "double", nullable: false),
                    WorstStartPosition = table.Column<double>(type: "double", nullable: false),
                    FirstRaceStartPosition = table.Column<double>(type: "double", nullable: false),
                    LastRaceStartPosition = table.Column<double>(type: "double", nullable: false),
                    Titles = table.Column<int>(type: "int", nullable: false),
                    HardChargerAwards = table.Column<int>(type: "int", nullable: false),
                    CleanestDriverAwards = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.DriverStatisticRowEntities", x => new { x.StatisticSetId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.LeagueMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.ScoredResultRows_FirstResultRowId",
                        column: x => x.FirstResultRowId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.ScoredResultRows_LastResultRowId",
                        column: x => x.LastResultRowId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.Sessions_FirstRaceId",
                        column: x => x.FirstRaceId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.Sessions_FirstSessionId",
                        column: x => x.FirstSessionId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.Sessions_LastRaceId",
                        column: x => x.LastRaceId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.Sessions_LastSessionId",
                        column: x => x.LastSessionId,
                        principalTable: "SessionEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRows_dbo.StatisticSets_StatisticSetId",
                        column: x => x.StatisticSetId,
                        principalTable: "StatisticSetEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoredResultRowEntityScoredTeamResultRowEntity",
                columns: table => new
                {
                    ScoredResultRowsScoredResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    ScoredTeamResultRowsScoredResultRowId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoredResultRowEntityScoredTeamResultRowEntity", x => new { x.ScoredResultRowsScoredResultRowId, x.ScoredTeamResultRowsScoredResultRowId });
                    table.ForeignKey(
                        name: "FK_ScoredResultRowEntityScoredTeamResultRowEntity_ScoredResultR~",
                        column: x => x.ScoredResultRowsScoredResultRowId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoredResultRowEntityScoredTeamResultRowEntity_ScoredTeamRes~",
                        column: x => x.ScoredTeamResultRowsScoredResultRowId,
                        principalTable: "ScoredTeamResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomVoteCatId",
                table: "AcceptedReviewVoteEntities",
                column: "CustomVoteCatId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAtFaultId",
                table: "AcceptedReviewVoteEntities",
                column: "MemberAtFaultId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewId",
                table: "AcceptedReviewVoteEntities",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoredResultRowId",
                table: "AddPenaltyEntities",
                column: "ScoredResultRowId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyToCommentId",
                table: "CommentBaseEntities",
                column: "ReplyToCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewId1",
                table: "CommentBaseEntities",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentId",
                table: "CommentReviewVoteEntities",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomVoteCatId1",
                table: "CommentReviewVoteEntities",
                column: "CustomVoteCatId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAtFaultId1",
                table: "CommentReviewVoteEntities",
                column: "MemberAtFaultId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomIncidentEntities_LeagueId",
                table: "CustomIncidentEntities",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstRaceId",
                table: "DriverStatisticRowEntities",
                column: "FirstRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstResultRowId",
                table: "DriverStatisticRowEntities",
                column: "FirstResultRowId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstSessionId",
                table: "DriverStatisticRowEntities",
                column: "FirstSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LastRaceId",
                table: "DriverStatisticRowEntities",
                column: "LastRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_LastResultRowId",
                table: "DriverStatisticRowEntities",
                column: "LastResultRowId");

            migrationBuilder.CreateIndex(
                name: "IX_LastSessionId",
                table: "DriverStatisticRowEntities",
                column: "LastSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberId",
                table: "DriverStatisticRowEntities",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSetId",
                table: "DriverStatisticRowEntities",
                column: "StatisticSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionId",
                table: "IncidentReviewEntities",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReviewEntityMemberEntity_InvolvedReviewsReviewId",
                table: "IncidentReviewEntityMemberEntity",
                column: "InvolvedReviewsReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEntityScoredResultEntity_CleanestDriverResultsResultId~",
                table: "MemberEntityScoredResultEntity",
                columns: new[] { "CleanestDriverResultsResultId", "CleanestDriverResultsScoringId" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberEntityScoredResultEntity1_HardChargerResultsResultId_H~",
                table: "MemberEntityScoredResultEntity1",
                columns: new[] { "HardChargerResultsResultId", "HardChargerResultsScoringId" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_TeamEntityTeamId",
                table: "Members",
                column: "TeamEntityTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultId",
                table: "ResultEntities",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId",
                table: "ResultEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberId1",
                table: "ResultRowEntities",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultId1",
                table: "ResultRowEntities",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamId",
                table: "ResultRowEntities",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringId",
                table: "ResultsFilterOptionEntities",
                column: "ScoringId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRowId",
                table: "ReviewPenaltyEntities",
                column: "ResultRowId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewId2",
                table: "ReviewPenaltyEntities",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewVoteId",
                table: "ReviewPenaltyEntities",
                column: "ReviewVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntities_LeagueId",
                table: "ScheduleEntities",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId1",
                table: "ScheduleEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_FastestAvgLapDriver_MemberId",
                table: "ScoredResultEntities",
                column: "FastestAvgLapDriver_MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FastestLapDriver_MemberId",
                table: "ScoredResultEntities",
                column: "FastestLapDriver_MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FastestQualyLapDriver_MemberId",
                table: "ScoredResultEntities",
                column: "FastestQualyLapDriver_MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultId2",
                table: "ScoredResultEntities",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringId1",
                table: "ScoredResultEntities",
                column: "ScoringId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRowId1",
                table: "ScoredResultRowEntities",
                column: "ResultRowId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoredResultId_ScoringId",
                table: "ScoredResultRowEntities",
                columns: new[] { "ScoredResultId", "ScoringId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamId1",
                table: "ScoredResultRowEntities",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoredResultRowEntityScoredTeamResultRowEntity_ScoredTeamRes~",
                table: "ScoredResultRowEntityScoredTeamResultRowEntity",
                column: "ScoredTeamResultRowsScoredResultRowId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoredResultId_ScoringId1",
                table: "ScoredTeamResultRowEntities",
                columns: new[] { "ScoredResultId", "ScoringId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamId2",
                table: "ScoredTeamResultRowEntities",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedScheduleId",
                table: "ScoringEntities",
                column: "ConnectedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtScoringSourceId",
                table: "ScoringEntities",
                column: "ExtScoringSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentScoringId",
                table: "ScoringEntities",
                column: "ParentScoringId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId2",
                table: "ScoringEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringEntitySessionEntity_SessionsSessionId",
                table: "ScoringEntitySessionEntity",
                column: "SessionsSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId3",
                table: "ScoringTableEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringRefId",
                table: "ScoringTableMap",
                column: "ScoringRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringTableRefId",
                table: "ScoringTableMap",
                column: "ScoringTableRefId");

            migrationBuilder.CreateIndex(
                name: "IX_MainScoring_ScoringId",
                table: "SeasonEntities",
                column: "MainScoring_ScoringId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonEntities_LeagueId",
                table: "SeasonEntities",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentSessionId",
                table: "SessionEntities",
                column: "ParentSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleId",
                table: "SessionEntities",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEntities_TrackId",
                table: "SessionEntities",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentChampId",
                table: "StatisticSetEntities",
                column: "CurrentChampId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringTableId",
                table: "StatisticSetEntities",
                column: "ScoringTableId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId4",
                table: "StatisticSetEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSetEntityStatisticSetEntity_LeagueStatisticSetsId",
                table: "StatisticSetEntityStatisticSetEntity",
                column: "LeagueStatisticSetsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackConfigEntity_TrackGroupId",
                table: "TrackConfigEntity",
                column: "TrackGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ReviewPenaltys_dbo.AcceptedReviewVotes_ReviewVoteId",
                table: "ReviewPenaltyEntities",
                column: "ReviewVoteId",
                principalTable: "AcceptedReviewVoteEntities",
                principalColumn: "ReviewVoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ReviewPenaltys_dbo.IncidentReviews_ReviewId",
                table: "ReviewPenaltyEntities",
                column: "ReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ReviewPenaltys_dbo.ScoredResultRows_ResultRowId",
                table: "ReviewPenaltyEntities",
                column: "ResultRowId",
                principalTable: "ScoredResultRowEntities",
                principalColumn: "ScoredResultRowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.CommentReviewVotes_dbo.CommentBases_CommentId",
                table: "CommentReviewVoteEntities",
                column: "CommentId",
                principalTable: "CommentBaseEntities",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.AcceptedReviewVotes_dbo.IncidentReviews_ReviewId",
                table: "AcceptedReviewVoteEntities",
                column: "ReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.CommentBases_dbo.IncidentReviews_ReviewId",
                table: "CommentBaseEntities",
                column: "ReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentReviewEntityMemberEntity_IncidentReviewEntities_Invo~",
                table: "IncidentReviewEntityMemberEntity",
                column: "InvolvedReviewsReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Schedules_dbo.Seasons_SeasonId",
                table: "ScheduleEntities",
                column: "SeasonId",
                principalTable: "SeasonEntities",
                principalColumn: "SeasonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Seasons_dbo.Scorings_MainScoring_ScoringId",
                table: "SeasonEntities",
                column: "MainScoring_ScoringId",
                principalTable: "ScoringEntities",
                principalColumn: "ScoringId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Schedules_dbo.Leagues_LeagueId",
                table: "ScheduleEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonEntities_Leagues_LeagueId",
                table: "SeasonEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Schedules_dbo.Seasons_SeasonId",
                table: "ScheduleEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Scorings_dbo.Seasons_Season_SeasonId",
                table: "ScoringEntities");

            migrationBuilder.DropTable(
                name: "AddPenaltyEntities");

            migrationBuilder.DropTable(
                name: "CommentReviewVoteEntities");

            migrationBuilder.DropTable(
                name: "CustomIncidentEntities");

            migrationBuilder.DropTable(
                name: "DriverStatisticRowEntities");

            migrationBuilder.DropTable(
                name: "IncidentReviewEntityMemberEntity");

            migrationBuilder.DropTable(
                name: "IRSimSessionDetailsEntity");

            migrationBuilder.DropTable(
                name: "MemberEntityScoredResultEntity");

            migrationBuilder.DropTable(
                name: "MemberEntityScoredResultEntity1");

            migrationBuilder.DropTable(
                name: "ResultsFilterOptionEntities");

            migrationBuilder.DropTable(
                name: "ReviewPenaltyEntities");

            migrationBuilder.DropTable(
                name: "ScoredResultRowEntityScoredTeamResultRowEntity");

            migrationBuilder.DropTable(
                name: "ScoringEntitySessionEntity");

            migrationBuilder.DropTable(
                name: "ScoringTableMap");

            migrationBuilder.DropTable(
                name: "StatisticSetEntityStatisticSetEntity");

            migrationBuilder.DropTable(
                name: "CommentBaseEntities");

            migrationBuilder.DropTable(
                name: "AcceptedReviewVoteEntities");

            migrationBuilder.DropTable(
                name: "ScoredResultRowEntities");

            migrationBuilder.DropTable(
                name: "ScoredTeamResultRowEntities");

            migrationBuilder.DropTable(
                name: "StatisticSetEntities");

            migrationBuilder.DropTable(
                name: "IncidentReviewEntities");

            migrationBuilder.DropTable(
                name: "VoteCategoryEntities");

            migrationBuilder.DropTable(
                name: "ResultRowEntities");

            migrationBuilder.DropTable(
                name: "ScoredResultEntities");

            migrationBuilder.DropTable(
                name: "ScoringTableEntities");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "ResultEntities");

            migrationBuilder.DropTable(
                name: "TeamEntities");

            migrationBuilder.DropTable(
                name: "SessionEntities");

            migrationBuilder.DropTable(
                name: "TrackConfigEntity");

            migrationBuilder.DropTable(
                name: "TrackGroups");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "SeasonEntities");

            migrationBuilder.DropTable(
                name: "ScoringEntities");

            migrationBuilder.DropTable(
                name: "ScheduleEntities");

            migrationBuilder.AlterDatabase(
                oldCollation: "Latin1_General_CI_AS");
        }
    }
}
