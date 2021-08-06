using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConverter.ApplicationCore.Interfaces
{
    public interface IConverter
    {
        public decimal Convert(decimal value, Unit startUnit, Unit endUnit);
    }
}
