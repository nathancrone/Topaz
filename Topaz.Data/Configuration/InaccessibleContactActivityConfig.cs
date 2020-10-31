using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleContactActivityConfig : IEntityTypeConfiguration<InaccessibleContactActivity>
    {
        public void Configure(EntityTypeBuilder<InaccessibleContactActivity> builder)
        {
            builder.HasKey(x => x.InaccessibleContactActivityId);
            builder.Property(x => x.InaccessibleContactActivityId).ValueGeneratedOnAdd();
        }
    }
}