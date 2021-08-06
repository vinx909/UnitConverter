using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.ApplicationCore.Models;

namespace UnitConverter.ApplicationCore.Interfaces
{
    public interface IMeterConvertorRepository
    {
        public void Create(Log log);
    }
}
