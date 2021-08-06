using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.ApplicationCore.Interfaces;
using UnitConverter.ApplicationCore.Models;

namespace UnitConverter.Infrastructure.Data
{
    public class MeterConvertorRepository : IMeterConvertorRepository
    {
        private readonly MeterConvertorDBContext dBContext;

        public MeterConvertorRepository(MeterConvertorDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Create(Log log)
        {
            dBContext.Add(log);
            dBContext.SaveChanges();
        }
    }
}
