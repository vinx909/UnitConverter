using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UnitConverter.Infrastructure.EntityConfiguration;

namespace UnitConverter.Infrastructure.Data
{
    public class MeterConvertorDBContext : DbContext
    {
        private readonly string connectionString;

        public MeterConvertorDBContext() : base()
        {
            this.connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MeterConvertor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public MeterConvertorDBContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
    }
}
