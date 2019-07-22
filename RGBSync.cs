using System;
using System.Diagnostics;

using OpenHardwareMonitor.Hardware;
using System.Timers;

namespace RGBSync
{
    enum Mode { GAME, WORK };
    class RGBSync
    {
        static bool IsOverwatchRunning()
        {
            return Process.GetProcessesByName("Overwatch").Length > 0;
        }

        static void Main(string[] args)
        {
            Vendor[] vendors = { Vendor.CORSAIR, Vendor.ASUS, Vendor.NZXT };
            IRGBDevices d = new RGBDevices(vendors);

            GPU gpu = new GPU();


            bool gameMode = false;
            bool firstTime = true;

            Timer timer = new Timer() { Enabled = true, Interval = 5000 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                // Console.WriteLine(gpu.Temperature);
                if (gpu.Temperature > 60 && !gameMode)
                {
                    d.ActivateGameMode();

                    Console.WriteLine("Activated game mode");
                    gameMode = true;
                }
                else if (gpu.Temperature <= 60 && gameMode || firstTime)
                {
                    d.ActivateWorkMode();

                    Console.WriteLine("Deactivated game mode");
                    gameMode = false;
                    firstTime = false;
                }
            };


            Console.ReadLine();
        }
    }
}
