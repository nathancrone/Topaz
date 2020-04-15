using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;
using Topaz.Common.Enums;

namespace Topaz.Data.Configuration
{
    internal class PhoneTypeConfig : IEntityTypeConfiguration<PhoneType>
    {
        public void Configure(EntityTypeBuilder<PhoneType> builder)
        {
            builder.HasKey(x => x.PhoneTypeId);
            builder.Property(x => x.PhoneTypeId).ValueGeneratedNever();
            builder.HasData(
                new PhoneType
                {
                    PhoneTypeId = (int)PhoneTypeEnum.Mobile,
                    Name = "Mobile"
                },
                new PhoneType
                {
                    PhoneTypeId = (int)PhoneTypeEnum.Landline,
                    Name = "Landline"
                }
            );
        }
    }
}