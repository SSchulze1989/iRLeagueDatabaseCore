using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace iRLeagueDatabaseCore.Models
{
    public partial class ScoredResultRowEntity : ResultRowBase
    {
        public ScoredResultRowEntity()
        {
            ReviewPenalties = new HashSet<ReviewPenaltyEntity>();
            ScoredTeamResultRows = new HashSet<ScoredTeamResultRowEntity>();
        }

        public ScoredResultRowEntity(ResultRowEntity resultRow)
            : this()
        {
            Member = resultRow.Member;
            Team = resultRow.Team;
            StartPosition = resultRow.StartPosition;
            FinishPosition = resultRow.FinishPosition;
            CarNumber = resultRow.CarNumber;
            ClassId = resultRow.ClassId;
            Car = resultRow.Car;
            CarClass = resultRow.CarClass;
            CompletedLaps = resultRow.CompletedLaps;
            LeadLaps = resultRow.LeadLaps;
            FastLapNr = resultRow.FastLapNr;
            Incidents = resultRow.Incidents;
            Status = resultRow.Status;
            QualifyingTime = resultRow.QualifyingTime;
            Interval = resultRow.Interval;
            AvgLapTime = resultRow.AvgLapTime;
            FastestLapTime = resultRow.FastestLapTime;
            PositionChange = resultRow.PositionChange;
            IRacingId = resultRow.IRacingId;
            SimSessionType = resultRow.SimSessionType;
            OldIRating = resultRow.OldIRating;
            NewIRating = resultRow.NewIRating;
            SeasonStartIRating = resultRow.SeasonStartIRating;
            License = resultRow.License;
            OldSafetyRating = resultRow.OldSafetyRating;
            NewSafetyRating = resultRow.NewSafetyRating;
            OldCpi = resultRow.OldCpi;
            NewCpi = resultRow.NewCpi;
            ClubId = resultRow.ClubId;
            ClubName = resultRow.ClubName;
            CarId = resultRow.CarId;
            CompletedPct = resultRow.CompletedPct;
            QualifyingTimeAt = resultRow.QualifyingTimeAt;
            Division = resultRow.Division;
            OldLicenseLevel = resultRow.OldLicenseLevel;
            NewLicenseLevel = resultRow.NewLicenseLevel;
            NumPitStops = resultRow.NumPitStops;
            PittedLaps = resultRow.PittedLaps;
            NumOfftrackLaps = resultRow.NumOfftrackLaps;
            OfftrackLaps = resultRow.OfftrackLaps;
            NumContactLaps = resultRow.NumContactLaps;
            ContactLaps = resultRow.ContactLaps;
    }

        public long LeagueId { get; set; }
        public long ResultId { get; set; }
        public long ScoredResultRowId { get; set; }
        public long ScoringId { get; set; }
        public long MemberId { get; set; }
        public long? TeamId { get; set; }
        public double RacePoints { get; set; }
        public double BonusPoints { get; set; }
        public double PenaltyPoints { get; set; }
        public int FinalPosition { get; set; }
        public double FinalPositionChange { get; set; }
        public double TotalPoints { get; set; }
        public bool PointsEligible { get; set; }

        public virtual MemberEntity Member { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual ScoredResultEntity ScoredResult { get; set; }
        public virtual AddPenaltyEntity AddPenalty { get; set; }
        public virtual ICollection<ReviewPenaltyEntity> ReviewPenalties { get; set; }
        public virtual ICollection<ScoredTeamResultRowEntity> ScoredTeamResultRows { get; set; }
    }

    public class ScoredResultRowEntityConfiguration : IEntityTypeConfiguration<ScoredResultRowEntity>
    {
        public void Configure(EntityTypeBuilder<ScoredResultRowEntity> entity)
        {
            entity.HasKey(e => new { e.LeagueId, e.ScoredResultRowId });

            entity.HasAlternateKey(e => e.ScoredResultRowId);

            entity.Property(e => e.ScoredResultRowId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => new { e.LeagueId, e.ResultId, e.ScoringId });

            entity.HasIndex(e => e.MemberId);

            entity.HasIndex(e => e.TeamId);

            entity.Property(e => e.QualifyingTimeAt).HasColumnType("datetime");

            entity.HasOne(d => d.Team)
                .WithMany(p => p.ScoredResultRows)
                .HasForeignKey(d => d.TeamId);

            entity.HasOne(d => d.ScoredResult)
                .WithMany(p => p.ScoredResultRows)
                .HasForeignKey(d => new { d.LeagueId, d.ResultId, d.ScoringId });

            entity.HasOne(d => d.Member)
                .WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
