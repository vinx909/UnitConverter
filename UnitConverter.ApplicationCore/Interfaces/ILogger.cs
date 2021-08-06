using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConverter.ApplicationCore.Interfaces
{
    public interface ILogger
    {
        public void LogConvert(Unit startUnit, Unit endUnit);
        public void LogException(Exception exception);
    }
}
