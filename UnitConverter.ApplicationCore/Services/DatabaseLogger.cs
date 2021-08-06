using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.ApplicationCore.Interfaces;
using UnitConverter.ApplicationCore.Models;

namespace UnitConverter.ApplicationCore.Services
{
    public class DatabaseLogger : ILogger
    {
        private readonly IMeterConvertorRepository repository;

        public DatabaseLogger(IMeterConvertorRepository repository)
        {
            this.repository = repository;
        }

        public void LogConvert(Unit startUnit, Unit endUnit)
        {
            repository.Create(CreateLog(startUnit, endUnit));
        }

        public void LogException(Exception exception)
        {
            throw new NotImplementedException();
        }

        private Log CreateLog(Unit startUnit, Unit endUnit)
        {
            return new Log() { Time = DateTime.Now, StartUnit = startUnit.ToString(), EndUnit = endUnit.ToString() };
        }
    }
}
