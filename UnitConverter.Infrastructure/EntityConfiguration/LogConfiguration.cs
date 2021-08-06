using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitConverter.ApplicationCore.Models;

namespace UnitConverter.Infrastructure.EntityConfiguration
{
    class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        private const string tableName = "Logs";

        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(l => l.Id);
            builder.Property(l => l.StartUnit).IsRequired().HasMaxLength(20);
            builder.Property(l => l.EndUnit).IsRequired().HasMaxLength(20);
        }
    }
}
