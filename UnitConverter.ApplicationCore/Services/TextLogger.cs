using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.ApplicationCore.Interfaces;

namespace UnitConverter.ApplicationCore.Services
{
    public class TextLogger : ILogger
    {
        private const string convertLog = "converted: {0}: {1}-{2}";
        private const string exceptionLog = "exception: {0}: {1}";

        public void LogConvert(Unit startUnit, Unit endUnit)
        {
            using (StreamWriter file = new("Log.txt", append: true))
            {
                file.WriteLine(string.Format(convertLog, DateTime.Now, startUnit, endUnit));
            }
        }

        public void LogException(Exception exception)
        {
            using (StreamWriter file = new("Log.txt", append: true))
            {
                file.WriteLine(string.Format(exceptionLog, DateTime.Now, exception));
            }
        }
    }
}
