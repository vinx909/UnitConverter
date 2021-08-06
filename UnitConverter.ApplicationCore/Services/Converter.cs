using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.ApplicationCore.Interfaces;

namespace UnitConverter.ApplicationCore.Services
{
    public class Converter:IConverter
    {
        private readonly ILogger logger;

        private Dictionary<Unit, decimal> distanceInMeter;

        public Converter(ILogger logger)
        {
            this.logger = logger;

            distanceInMeter = new();
            distanceInMeter.Add(Unit.centimeter, 0.01M);
            distanceInMeter.Add(Unit.inch, 0.0254M);
            distanceInMeter.Add(Unit.meter, 1M);
            distanceInMeter.Add(Unit.millimeter, 0.001M);
        }

        public decimal Convert(decimal value, Unit startUnit, Unit endUnit)
        {
            if (distanceInMeter.ContainsKey(startUnit) && distanceInMeter.ContainsKey(endUnit))
            {
                logger?.LogConvert(startUnit, endUnit);
                decimal valueInMeters = value * distanceInMeter[startUnit];
                return valueInMeters / distanceInMeter[endUnit];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
