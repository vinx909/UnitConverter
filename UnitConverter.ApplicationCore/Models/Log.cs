using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConverter.ApplicationCore.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string StartUnit { get; set; }
        public string EndUnit { get; set; }
    }
}
