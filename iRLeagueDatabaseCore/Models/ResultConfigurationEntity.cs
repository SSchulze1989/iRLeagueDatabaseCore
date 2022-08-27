﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class ResultConfigurationEntity : IVersionEntity
    {
        public ResultConfigurationEntity()
        {
            Scorings = new HashSet<ScoringEntity>();
            ResultTabs = new HashSet<ResultTabEntity>();
        }

        public long LeagueId { get; set; }
        public long ResultConfigId { get; set; }

        public virtual ICollection<ScoringEntity> Scorings { get; set; }
        public virtual ICollection<ResultTabEntity> ResultTabs { get; set; }

        #region version
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int Version { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        #endregion

    }

    public class ResultConfigurationEntityConfiguration : IEntityTypeConfiguration<ResultConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<ResultConfigurationEntity> entity)
        {
            entity.HasKey(e => new {e.LeagueId, e.ResultConfigId});

            entity.HasAlternateKey(e => e.ResultConfigId);

            entity.Property(e => e.ResultConfigId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

            entity.HasMany(d => d.ResultTabs)
                .WithMany(p => p.ResultConfigurations)
                .UsingEntity<ResultConfigurationsResultTabs>(
                    left => left.HasOne(e => e.ResultTabRef)
                        .WithMany().HasForeignKey(e => new { e.LeagueId, e.ResultTabRefId }),
                    right => right.HasOne(e => e.ResultConfigurationRef)
                        .WithMany().HasForeignKey(e => new { e.LeagueId, e.ResultConfigurationRefId }));
        }
    }
}
