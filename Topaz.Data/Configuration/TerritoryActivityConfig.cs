using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class TerritoryActivityConfig : IEntityTypeConfiguration<TerritoryActivity>
    {
        public void Configure(EntityTypeBuilder<TerritoryActivity> builder)
        {
            builder.HasKey(x => x.TerritoryActivityId);
            builder.Property(x => x.TerritoryActivityId).ValueGeneratedOnAdd();
        }
    }
}