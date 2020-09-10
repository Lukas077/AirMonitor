using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    class Measurement
    {
        public int CurrentDisplayValue { get; set; }
        public MeasurementItem Current { get; set; }
        public MeasurementItem[] History { get; set; }
        public MeasurementItem[] Forecast { get; set; }
        public Nearest Installation { get; set; }
    }
}
