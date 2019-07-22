using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBSync
{
    class GPU
    {
        Computer computer = new Computer() { GPUEnabled = true };
        public GPU()
        {
            computer.Open();
        }

        public float Temperature
        {
            get
            {
                foreach(IHardware hardware in computer.Hardware)
                {
                    hardware.Update();
                    foreach (ISensor sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            return (float) sensor.Value;
                        }

                    }
                }
                return 0;
            }
        }
    }
}
