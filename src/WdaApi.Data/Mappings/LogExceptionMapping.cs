using SaturnApi.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaturnApi.Data.Mappings
{
    public class LogExceptionMapping : IEntityTypeConfiguration<LogException>
    {
        public void Configure(EntityTypeBuilder<LogException> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.TimeStamp)
                .HasColumnType("datetime");

            builder.Property(c => c.RequestId)
                .HasColumnType("varchar(256)");

            builder.Property(c => c.Message)
                .HasColumnType("LONGTEXT");

            builder.Property(c => c.Type)
                .HasColumnType("LONGTEXT");

            builder.Property(c => c.Source)
                .HasColumnType("LONGTEXT");

            builder.Property(c => c.StackTrace)
                .HasColumnType("LONGTEXT");

            builder.Property(c => c.RequestPath)
                .HasColumnType("LONGTEXT");

            builder.Property(c => c.User)
                .HasColumnType("varchar(256)");

            builder.Property(c => c.IpAddress)
                .HasColumnType("LONGTEXT");
        }
    }
}
