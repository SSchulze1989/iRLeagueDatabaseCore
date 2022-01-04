using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class CreateLeagueDbCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueEntity",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameFull = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamHomepage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TeamEntities", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "VoteCategoryEntities",
                columns: table => new
                {
                    CatId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CustomIncidentEntities", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_CustomIncidentEntities_LeagueEntity_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "LeagueEntity",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueMemberEntities",
                columns: table => new
                {
                    MemberId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IRacingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanLisaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscordId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamEntityTeamId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.LeagueMemberEntities", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_LeagueMemberEntities_TeamEntities_TeamEntityTeamId",
                        column: x => x.TeamEntityTeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    MemberAtFaultId = table.Column<long>(type: "bigint", nullable: true),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    CustomVoteCatId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CommentReviewVoteEntities", x => x.ReviewVoteId);
                    table.ForeignKey(
                        name: "FK_dbo.CommentReviewVoteEntities_dbo.LeagueMemberEntities_MemberAtFaultId",
                        column: x => x.MemberAtFaultId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.CommentReviewVoteEntities_dbo.VoteCategoryEntities_CustomVoteCatId",
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewId = table.Column<long>(type: "bigint", nullable: false),
                    MemberAtFaultId = table.Column<long>(type: "bigint", nullable: true),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    CustomVoteCatId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AcceptedReviewVoteEntities", x => x.ReviewVoteId);
                    table.ForeignKey(
                        name: "FK_dbo.AcceptedReviewVoteEntities_dbo.LeagueMemberEntities_MemberAtFaultId",
                        column: x => x.MemberAtFaultId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.AcceptedReviewVoteEntities_dbo.VoteCategoryEntities_CustomVoteCatId",
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    AuthorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplyToCommentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewId = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CommentBaseEntities", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_dbo.CommentBaseEntities_dbo.CommentBaseEntities_ReplyToCommentId",
                        column: x => x.ReplyToCommentId,
                        principalTable: "CommentBaseEntities",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncidentReview_InvolvedLeagueMember",
                columns: table => new
                {
                    ReviewRefId = table.Column<long>(type: "bigint", nullable: false),
                    MemberRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.IncidentReview_InvolvedLeagueMember", x => new { x.ReviewRefId, x.MemberRefId });
                    table.ForeignKey(
                        name: "FK_dbo.IncidentReview_InvolvedLeagueMember_dbo.LeagueMemberEntities_MemberRefId",
                        column: x => x.MemberRefId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonEntities",
                columns: table => new
                {
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    SeasonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainScoring_ScoringId = table.Column<long>(type: "bigint", nullable: true),
                    HideCommentsBeforeVoted = table.Column<bool>(type: "bit", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    SeasonStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    SeasonEnd = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SeasonEntities", x => x.SeasonId);
                    table.ForeignKey(
                        name: "FK_SeasonEntities_LeagueEntity_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "LeagueEntity",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEntities",
                columns: table => new
                {
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Season_SeasonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScheduleEntities", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_dbo.ScheduleEntities_dbo.SeasonEntities_Season_SeasonId",
                        column: x => x.Season_SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringTableEntities",
                columns: table => new
                {
                    ScoringTableId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoringKind = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropWeeks = table.Column<int>(type: "int", nullable: false),
                    AverageRaceNr = table.Column<int>(type: "int", nullable: false),
                    ScoringFactors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropRacesOption = table.Column<int>(type: "int", nullable: false),
                    ResultsPerRaceCount = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoringTableEntities", x => x.ScoringTableId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringTableEntities_dbo.SeasonEntities_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringEntities",
                columns: table => new
                {
                    ScoringId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringKind = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropWeeks = table.Column<int>(type: "int", nullable: false),
                    AverageRaceNr = table.Column<int>(type: "int", nullable: false),
                    MaxResultsPerGroup = table.Column<int>(type: "int", nullable: false),
                    TakeGroupAverage = table.Column<bool>(type: "bit", nullable: false),
                    ExtScoringSourceId = table.Column<long>(type: "bigint", nullable: true),
                    TakeResultsFromExtSource = table.Column<bool>(type: "bit", nullable: false),
                    BasePoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonusPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncPenaltyPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectedScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false),
                    UseResultSetTeam = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTeamOnRecalculation = table.Column<bool>(type: "bit", nullable: false),
                    ParentScoringId = table.Column<long>(type: "bigint", nullable: true),
                    ScoringSessionType = table.Column<int>(type: "int", nullable: false),
                    SessionSelectType = table.Column<int>(type: "int", nullable: false),
                    ScoringWeightValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccumulateBy = table.Column<int>(type: "int", nullable: false),
                    AccumulateResultsOption = table.Column<int>(type: "int", nullable: false),
                    PointsSortOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalSortOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowResults = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoringEntities", x => x.ScoringId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringEntities_dbo.ScheduleEntities_ConnectedSchedule_ScheduleId",
                        column: x => x.ConnectedScheduleId,
                        principalTable: "ScheduleEntities",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringEntities_dbo.ScoringEntities_ExtScoringSourceId",
                        column: x => x.ExtScoringSourceId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringEntities_dbo.ScoringEntities_ParentScoringId",
                        column: x => x.ParentScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringEntities_dbo.SeasonEntities_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionBaseEntities",
                columns: table => new
                {
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaceId = table.Column<long>(type: "bigint", nullable: true),
                    Laps = table.Column<int>(type: "int", nullable: true),
                    PracticeLength = table.Column<TimeSpan>(type: "time", nullable: true),
                    QualyLength = table.Column<TimeSpan>(type: "time", nullable: true),
                    RaceLength = table.Column<TimeSpan>(type: "time", nullable: true),
                    IrSessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IrResultLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualyAttached = table.Column<bool>(type: "bit", nullable: true),
                    PracticeAttached = table.Column<bool>(type: "bit", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentSessionId = table.Column<long>(type: "bigint", nullable: true),
                    SubSessionNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SessionBaseEntities", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_dbo.SessionBaseEntities_dbo.ScheduleEntities_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ScheduleEntities",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.SessionBaseEntities_dbo.SessionBaseEntities_ParentSessionId",
                        column: x => x.ParentSessionId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatisticSetEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateInterval = table.Column<long>(type: "bigint", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequiresRecalculation = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentChampId = table.Column<long>(type: "bigint", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: true),
                    ScoringTableId = table.Column<long>(type: "bigint", nullable: true),
                    FinishedRaces = table.Column<int>(type: "int", nullable: true),
                    IsSeasonFinished = table.Column<bool>(type: "bit", nullable: true),
                    ImportSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticSetEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.StatisticSetEntities_dbo.LeagueMemberEntities_CurrentChampId",
                        column: x => x.CurrentChampId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.StatisticSetEntities_dbo.ScoringTableEntities_ScoringTableId",
                        column: x => x.ScoringTableId,
                        principalTable: "ScoringTableEntities",
                        principalColumn: "ScoringTableId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.StatisticSetEntities_dbo.SeasonEntities_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultsFilterOptionEntities",
                columns: table => new
                {
                    ResultsFilterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    ResultsFilterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnPropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comparator = table.Column<int>(type: "int", nullable: false),
                    Exclude = table.Column<bool>(type: "bit", nullable: false),
                    FilterValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterPointsOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ResultsFilterOptionEntities", x => x.ResultsFilterId);
                    table.ForeignKey(
                        name: "FK_dbo.ResultsFilterOptionEntities_dbo.ScoringEntities_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
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
                        name: "FK_dbo.ScoringTableMap_dbo.ScoringEntities_ScoringRefId",
                        column: x => x.ScoringRefId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoringTableMap_dbo.ScoringTableEntities_ScoringTableRefId",
                        column: x => x.ScoringTableRefId,
                        principalTable: "ScoringTableEntities",
                        principalColumn: "ScoringTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncidentReviewEntities",
                columns: table => new
                {
                    ReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    SessionId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncidentKind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnLap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Corner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultLongText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncidentNr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.IncidentReviewEntities", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_dbo.IncidentReviewEntities_dbo.SessionBaseEntities_SessionId",
                        column: x => x.SessionId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
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
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: true),
                    RequiresRecalculation = table.Column<bool>(type: "bit", nullable: false),
                    PoleLaptime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ResultEntities", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_dbo.ResultEntities_dbo.SeasonEntities_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonEntities",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ResultEntities_dbo.SessionBaseEntities_ResultId",
                        column: x => x.ResultId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scoring_Session",
                columns: table => new
                {
                    ScoringRefId = table.Column<long>(type: "bigint", nullable: false),
                    SessionRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Scoring_Session", x => new { x.ScoringRefId, x.SessionRefId });
                    table.ForeignKey(
                        name: "FK_dbo.Scoring_Session_dbo.ScoringEntities_ScoringRefId",
                        column: x => x.ScoringRefId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Scoring_Session_dbo.SessionBaseEntities_SessionRefId",
                        column: x => x.SessionRefId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueStatisticSet_SeasonStatisticSet",
                columns: table => new
                {
                    LeagueStatisticSetRefId = table.Column<long>(type: "bigint", nullable: false),
                    SeasonStatisticSetRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.LeagueStatisticSet_SeasonStatisticSet", x => new { x.LeagueStatisticSetRefId, x.SeasonStatisticSetRefId });
                    table.ForeignKey(
                        name: "FK_dbo.LeagueStatisticSet_SeasonStatisticSet_dbo.StatisticSetEntities_LeagueStatisticSetRefId",
                        column: x => x.LeagueStatisticSetRefId,
                        principalTable: "StatisticSetEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.LeagueStatisticSet_SeasonStatisticSet_dbo.StatisticSetEntities_SeasonStatisticSetRefId",
                        column: x => x.SeasonStatisticSetRefId,
                        principalTable: "StatisticSetEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IRSimSessionDetailsEntity",
                columns: table => new
                {
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    IRSubsessionId = table.Column<long>(type: "bigint", nullable: false),
                    IRSeasonId = table.Column<long>(type: "bigint", nullable: false),
                    IRSeasonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IRSeasonYear = table.Column<int>(type: "int", nullable: false),
                    IRSeasonQuarter = table.Column<int>(type: "int", nullable: false),
                    IRRaceWeek = table.Column<int>(type: "int", nullable: false),
                    IRSessionId = table.Column<long>(type: "bigint", nullable: false),
                    LicenseCategory = table.Column<int>(type: "int", nullable: false),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CornersPerLap = table.Column<int>(type: "int", nullable: false),
                    KmDistPerLap = table.Column<double>(type: "float", nullable: false),
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
                    TrackName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfigName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackCategoryId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    LeaveMarbles = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_dbo.IRSimSessionDetailsEntities_dbo.ResultEntities_ResultId",
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    StartPosition = table.Column<double>(type: "float", nullable: false),
                    FinishPosition = table.Column<double>(type: "float", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    CarNumber = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Car = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedLaps = table.Column<double>(type: "float", nullable: false),
                    LeadLaps = table.Column<double>(type: "float", nullable: false),
                    FastLapNr = table.Column<int>(type: "int", nullable: false),
                    Incidents = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    QualifyingTime = table.Column<long>(type: "bigint", nullable: false),
                    Interval = table.Column<long>(type: "bigint", nullable: false),
                    AvgLapTime = table.Column<long>(type: "bigint", nullable: false),
                    FastestLapTime = table.Column<long>(type: "bigint", nullable: false),
                    PositionChange = table.Column<double>(type: "float", nullable: false),
                    IRacingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SimSessionType = table.Column<int>(type: "int", nullable: false),
                    OldIRating = table.Column<int>(type: "int", nullable: false),
                    NewIRating = table.Column<int>(type: "int", nullable: false),
                    SeasonStartIRating = table.Column<int>(type: "int", nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldSafetyRating = table.Column<double>(type: "float", nullable: false),
                    NewSafetyRating = table.Column<double>(type: "float", nullable: false),
                    OldCpi = table.Column<int>(type: "int", nullable: false),
                    NewCpi = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CompletedPct = table.Column<double>(type: "float", nullable: false),
                    QualifyingTimeAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Division = table.Column<int>(type: "int", nullable: false),
                    OldLicenseLevel = table.Column<int>(type: "int", nullable: false),
                    NewLicenseLevel = table.Column<int>(type: "int", nullable: false),
                    NumPitStops = table.Column<int>(type: "int", nullable: false),
                    PittedLaps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfftrackLaps = table.Column<int>(type: "int", nullable: false),
                    OfftrackLaps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumContactLaps = table.Column<int>(type: "int", nullable: false),
                    ContactLaps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    PointsEligible = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ResultRowEntities", x => x.ResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.ResultRowEntities_dbo.LeagueMemberEntities_MemberId",
                        column: x => x.MemberId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ResultRowEntities_dbo.ResultEntities_ResultId",
                        column: x => x.ResultId,
                        principalTable: "ResultEntities",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ResultRowEntities_dbo.TeamEntities_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
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
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FastestAvgLapDriver_MemberId = table.Column<long>(type: "bigint", nullable: true),
                    FastestLapDriver_MemberId = table.Column<long>(type: "bigint", nullable: true),
                    FastestQualyLapDriver_MemberId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredResultEntities", x => new { x.ResultId, x.ScoringId });
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestAvgLapDriver_MemberId",
                        column: x => x.FastestAvgLapDriver_MemberId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestLapDriver_MemberId",
                        column: x => x.FastestLapDriver_MemberId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultEntities_dbo.LeagueMemberEntities_FastestQualyLapDriver_MemberId",
                        column: x => x.FastestQualyLapDriver_MemberId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultEntities_dbo.ResultEntities_ResultId",
                        column: x => x.ResultId,
                        principalTable: "ResultEntities",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultEntities_dbo.ScoringEntities_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "ScoringEntities",
                        principalColumn: "ScoringId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoredResult_CleanestDrivers",
                columns: table => new
                {
                    ResultRefId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringRefId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueMemberRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredResult_CleanestDrivers", x => new { x.ResultRefId, x.ScoringRefId, x.LeagueMemberRefId });
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResult_CleanestDrivers_dbo.LeagueMemberEntities_LeagueMemberRefId",
                        column: x => x.LeagueMemberRefId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResult_CleanestDrivers_dbo.ScoredResultEntities_ResultRefId_ScoringRefId",
                        columns: x => new { x.ResultRefId, x.ScoringRefId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoredResult_HardChargers",
                columns: table => new
                {
                    ResultRefId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringRefId = table.Column<long>(type: "bigint", nullable: false),
                    MemberRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredResult_HardChargers", x => new { x.ResultRefId, x.ScoringRefId, x.MemberRefId });
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResult_HardChargers_dbo.LeagueMemberEntities_LeagueMemberRefId",
                        column: x => x.MemberRefId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResult_HardChargers_dbo.ScoredResultEntities_ResultRefId_ScoringRefId",
                        columns: x => new { x.ResultRefId, x.ScoringRefId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoredResultRowEntities",
                columns: table => new
                {
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoredResultId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    ResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    RacePoints = table.Column<double>(type: "float", nullable: false),
                    BonusPoints = table.Column<double>(type: "float", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "float", nullable: false),
                    FinalPosition = table.Column<int>(type: "int", nullable: false),
                    FinalPositionChange = table.Column<double>(type: "float", nullable: false),
                    TotalPoints = table.Column<double>(type: "float", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    PointsEligible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredResultRowEntities", x => x.ScoredResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultRowEntities_dbo.ResultRowEntities_ResultRowId",
                        column: x => x.ResultRowId,
                        principalTable: "ResultRowEntities",
                        principalColumn: "ResultRowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultRowEntities_dbo.ScoredResultEntities_ScoredResultId_ScoringId",
                        columns: x => new { x.ScoredResultId, x.ScoringId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredResultRowEntities_dbo.TeamEntities_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoredTeamResultRowEntities",
                columns: table => new
                {
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoredResultId = table.Column<long>(type: "bigint", nullable: false),
                    ScoringId = table.Column<long>(type: "bigint", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CarClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RacePoints = table.Column<double>(type: "float", nullable: false),
                    BonusPoints = table.Column<double>(type: "float", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "float", nullable: false),
                    FinalPosition = table.Column<int>(type: "int", nullable: false),
                    FinalPositionChange = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<double>(type: "float", nullable: false),
                    AvgLapTime = table.Column<long>(type: "bigint", nullable: false),
                    FastestLapTime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredTeamResultRowEntities", x => x.ScoredResultRowId);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredTeamResultRowEntities_dbo.ScoredResultEntities_ScoredResultId_ScoringId",
                        columns: x => new { x.ScoredResultId, x.ScoringId },
                        principalTable: "ScoredResultEntities",
                        principalColumns: new[] { "ResultId", "ScoringId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredTeamResultRowEntities_dbo.TeamEntities_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_dbo.AddPenaltyEntities_dbo.ScoredResultRowEntities_ScoredResultRowId",
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
                    StartSRating = table.Column<double>(type: "float", nullable: false),
                    EndSRating = table.Column<double>(type: "float", nullable: false),
                    FirstSessionId = table.Column<long>(type: "bigint", nullable: true),
                    FirstSessionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FirstRaceId = table.Column<long>(type: "bigint", nullable: true),
                    FirstRaceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastSessionId = table.Column<long>(type: "bigint", nullable: true),
                    LastSessionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastRaceId = table.Column<long>(type: "bigint", nullable: true),
                    LastRaceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RacePoints = table.Column<double>(type: "float", nullable: false),
                    TotalPoints = table.Column<double>(type: "float", nullable: false),
                    BonusPoints = table.Column<double>(type: "float", nullable: false),
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
                    Incidents = table.Column<double>(type: "float", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "float", nullable: false),
                    FastestLaps = table.Column<int>(type: "int", nullable: false),
                    IncidentsUnderInvestigation = table.Column<int>(type: "int", nullable: false),
                    IncidentsWithPenalty = table.Column<int>(type: "int", nullable: false),
                    LeadingLaps = table.Column<double>(type: "float", nullable: false),
                    CompletedLaps = table.Column<double>(type: "float", nullable: false),
                    CurrentSeasonPosition = table.Column<int>(type: "int", nullable: false),
                    DrivenKm = table.Column<double>(type: "float", nullable: false),
                    LeadingKm = table.Column<double>(type: "float", nullable: false),
                    AvgFinishPosition = table.Column<double>(type: "float", nullable: false),
                    AvgFinalPosition = table.Column<double>(type: "float", nullable: false),
                    AvgStartPosition = table.Column<double>(type: "float", nullable: false),
                    AvgPointsPerRace = table.Column<double>(type: "float", nullable: false),
                    AvgIncidentsPerRace = table.Column<double>(type: "float", nullable: false),
                    AvgIncidentsPerLap = table.Column<double>(type: "float", nullable: false),
                    AvgIncidentsPerKm = table.Column<double>(type: "float", nullable: false),
                    AvgPenaltyPointsPerRace = table.Column<double>(type: "float", nullable: false),
                    AvgPenaltyPointsPerLap = table.Column<double>(type: "float", nullable: false),
                    AvgPenaltyPointsPerKm = table.Column<double>(type: "float", nullable: false),
                    AvgIRating = table.Column<double>(type: "float", nullable: false),
                    AvgSRating = table.Column<double>(type: "float", nullable: false),
                    BestFinishPosition = table.Column<double>(type: "float", nullable: false),
                    WorstFinishPosition = table.Column<double>(type: "float", nullable: false),
                    FirstRaceFinishPosition = table.Column<double>(type: "float", nullable: false),
                    LastRaceFinishPosition = table.Column<double>(type: "float", nullable: false),
                    BestFinalPosition = table.Column<int>(type: "int", nullable: false),
                    WorstFinalPosition = table.Column<int>(type: "int", nullable: false),
                    FirstRaceFinalPosition = table.Column<int>(type: "int", nullable: false),
                    LastRaceFinalPosition = table.Column<int>(type: "int", nullable: false),
                    BestStartPosition = table.Column<double>(type: "float", nullable: false),
                    WorstStartPosition = table.Column<double>(type: "float", nullable: false),
                    FirstRaceStartPosition = table.Column<double>(type: "float", nullable: false),
                    LastRaceStartPosition = table.Column<double>(type: "float", nullable: false),
                    Titles = table.Column<int>(type: "int", nullable: false),
                    HardChargerAwards = table.Column<int>(type: "int", nullable: false),
                    CleanestDriverAwards = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.DriverStatisticRowEntities", x => new { x.StatisticSetId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.LeagueMemberEntities_MemberId",
                        column: x => x.MemberId,
                        principalTable: "LeagueMemberEntities",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.ScoredResultRowEntities_FirstResultRowId",
                        column: x => x.FirstResultRowId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.ScoredResultRowEntities_LastResultRowId",
                        column: x => x.LastResultRowId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_FirstRaceId",
                        column: x => x.FirstRaceId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_FirstSessionId",
                        column: x => x.FirstSessionId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_LastRaceId",
                        column: x => x.LastRaceId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.SessionBaseEntities_LastSessionId",
                        column: x => x.LastSessionId,
                        principalTable: "SessionBaseEntities",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DriverStatisticRowEntities_dbo.StatisticSetEntities_StatisticSetId",
                        column: x => x.StatisticSetId,
                        principalTable: "StatisticSetEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoredTeamResultRowsGroup",
                columns: table => new
                {
                    ScoredTeamResultRowRefId = table.Column<long>(type: "bigint", nullable: false),
                    ScoredResultRowRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ScoredTeamResultRowsGroup", x => new { x.ScoredTeamResultRowRefId, x.ScoredResultRowRefId });
                    table.ForeignKey(
                        name: "FK_dbo.ScoredTeamResultRowsGroup_dbo.ScoredResultRowEntities_ScoredResultRowRefId",
                        column: x => x.ScoredResultRowRefId,
                        principalTable: "ScoredResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ScoredTeamResultRowsGroup_dbo.ScoredTeamResultRowEntities_ScoredTeamResultRowRefId",
                        column: x => x.ScoredTeamResultRowRefId,
                        principalTable: "ScoredTeamResultRowEntities",
                        principalColumn: "ScoredResultRowId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_MemberRefId",
                table: "IncidentReview_InvolvedLeagueMember",
                column: "MemberRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRefId",
                table: "IncidentReview_InvolvedLeagueMember",
                column: "ReviewRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionId",
                table: "IncidentReviewEntities",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMemberEntities_TeamEntityTeamId",
                table: "LeagueMemberEntities",
                column: "TeamEntityTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueStatisticSetRefId",
                table: "LeagueStatisticSet_SeasonStatisticSet",
                column: "LeagueStatisticSetRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonStatisticSetRefId",
                table: "LeagueStatisticSet_SeasonStatisticSet",
                column: "SeasonStatisticSetRefId");

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
                name: "IX_Season_SeasonId",
                table: "ScheduleEntities",
                column: "Season_SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMemberRefId",
                table: "ScoredResult_CleanestDrivers",
                column: "LeagueMemberRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRefId_ScoringRefId",
                table: "ScoredResult_CleanestDrivers",
                columns: new[] { "ResultRefId", "ScoringRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMemberRefId1",
                table: "ScoredResult_HardChargers",
                column: "MemberRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRefId_ScoringRefId1",
                table: "ScoredResult_HardChargers",
                columns: new[] { "ResultRefId", "ScoringRefId" });

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
                name: "IX_ScoredResultId_ScoringId1",
                table: "ScoredTeamResultRowEntities",
                columns: new[] { "ScoredResultId", "ScoringId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamId2",
                table: "ScoredTeamResultRowEntities",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoredResultRowRefId",
                table: "ScoredTeamResultRowsGroup",
                column: "ScoredResultRowRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoredTeamResultRowRefId",
                table: "ScoredTeamResultRowsGroup",
                column: "ScoredTeamResultRowRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringRefId",
                table: "Scoring_Session",
                column: "ScoringRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionRefId",
                table: "Scoring_Session",
                column: "SessionRefId");

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
                name: "IX_SeasonId1",
                table: "ScoringEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId2",
                table: "ScoringTableEntities",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringRefId1",
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
                table: "SessionBaseEntities",
                column: "ParentSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleId",
                table: "SessionBaseEntities",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentChampId",
                table: "StatisticSetEntities",
                column: "CurrentChampId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringTableId",
                table: "StatisticSetEntities",
                column: "ScoringTableId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonId3",
                table: "StatisticSetEntities",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ReviewPenaltyEntities_dbo.AcceptedReviewVoteEntities_ReviewVoteId",
                table: "ReviewPenaltyEntities",
                column: "ReviewVoteId",
                principalTable: "AcceptedReviewVoteEntities",
                principalColumn: "ReviewVoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ReviewPenaltyEntities_dbo.IncidentReviewEntities_ReviewId",
                table: "ReviewPenaltyEntities",
                column: "ReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.ReviewPenaltyEntities_dbo.ScoredResultRowEntities_ResultRowId",
                table: "ReviewPenaltyEntities",
                column: "ResultRowId",
                principalTable: "ScoredResultRowEntities",
                principalColumn: "ScoredResultRowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.CommentReviewVoteEntities_dbo.CommentBaseEntities_CommentId",
                table: "CommentReviewVoteEntities",
                column: "CommentId",
                principalTable: "CommentBaseEntities",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.AcceptedReviewVoteEntities_dbo.IncidentReviewEntities_ReviewId",
                table: "AcceptedReviewVoteEntities",
                column: "ReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.CommentBaseEntities_dbo.IncidentReviewEntities_ReviewId",
                table: "CommentBaseEntities",
                column: "ReviewId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.IncidentReview_InvolvedLeagueMember_dbo.IncidentReviewEntities_ReviewRefId",
                table: "IncidentReview_InvolvedLeagueMember",
                column: "ReviewRefId",
                principalTable: "IncidentReviewEntities",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.SeasonEntities_dbo.ScoringEntities_MainScoring_ScoringId",
                table: "SeasonEntities",
                column: "MainScoring_ScoringId",
                principalTable: "ScoringEntities",
                principalColumn: "ScoringId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeasonEntities_LeagueEntity_LeagueId",
                table: "SeasonEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ScheduleEntities_dbo.SeasonEntities_Season_SeasonId",
                table: "ScheduleEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.ScoringEntities_dbo.SeasonEntities_Season_SeasonId",
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
                name: "IncidentReview_InvolvedLeagueMember");

            migrationBuilder.DropTable(
                name: "IRSimSessionDetailsEntity");

            migrationBuilder.DropTable(
                name: "LeagueStatisticSet_SeasonStatisticSet");

            migrationBuilder.DropTable(
                name: "ResultsFilterOptionEntities");

            migrationBuilder.DropTable(
                name: "ReviewPenaltyEntities");

            migrationBuilder.DropTable(
                name: "ScoredResult_CleanestDrivers");

            migrationBuilder.DropTable(
                name: "ScoredResult_HardChargers");

            migrationBuilder.DropTable(
                name: "ScoredTeamResultRowsGroup");

            migrationBuilder.DropTable(
                name: "Scoring_Session");

            migrationBuilder.DropTable(
                name: "ScoringTableMap");

            migrationBuilder.DropTable(
                name: "CommentBaseEntities");

            migrationBuilder.DropTable(
                name: "StatisticSetEntities");

            migrationBuilder.DropTable(
                name: "AcceptedReviewVoteEntities");

            migrationBuilder.DropTable(
                name: "ScoredResultRowEntities");

            migrationBuilder.DropTable(
                name: "ScoredTeamResultRowEntities");

            migrationBuilder.DropTable(
                name: "ScoringTableEntities");

            migrationBuilder.DropTable(
                name: "IncidentReviewEntities");

            migrationBuilder.DropTable(
                name: "VoteCategoryEntities");

            migrationBuilder.DropTable(
                name: "ResultRowEntities");

            migrationBuilder.DropTable(
                name: "ScoredResultEntities");

            migrationBuilder.DropTable(
                name: "LeagueMemberEntities");

            migrationBuilder.DropTable(
                name: "ResultEntities");

            migrationBuilder.DropTable(
                name: "TeamEntities");

            migrationBuilder.DropTable(
                name: "SessionBaseEntities");

            migrationBuilder.DropTable(
                name: "LeagueEntity");

            migrationBuilder.DropTable(
                name: "SeasonEntities");

            migrationBuilder.DropTable(
                name: "ScoringEntities");

            migrationBuilder.DropTable(
                name: "ScheduleEntities");
        }
    }
}
