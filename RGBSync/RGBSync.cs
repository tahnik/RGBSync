using System;
using System.Diagnostics;
using System.Timers;

namespace RGBSync
{
    enum Mode { GAME, WORK };
    enum State { Initial, Red, Cyan }
    public class RGBSync
    {

        /*
        public RGBSync()
        {
            Vendor[] vendors = { Vendor.CORSAIR, Vendor.ASUS, Vendor.NZXT };
            IRGBDevices d = new RGBDevices(vendors);

            bool gameMode = false;
            bool firstTime = true;

            Timer timer = new Timer() { Enabled = true, Interval = 5000 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
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
        }

        public float Temperature
        {
            get
            {
                return gpu.Temperature;
            }
        }
        */

        static void Main(string[] args)
        {
            Vendor[] vendors = { Vendor.CORSAIR, Vendor.NZXT, Vendor.ASUS };
            IRGBDevices d = new RGBDevices(vendors);

            GPU gpu = new GPU();

            bool gameMode = false;
            bool firstTime = true;

            Timer timer = new Timer() { Enabled = true, Interval = 5000 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                // Console.WriteLine(gpu.Temperature);
                if (gpu.Temperature > 70 && !gameMode)
                {
                    d.ActivateGameMode();

                    Console.WriteLine("Activated game mode");
                    gameMode = true;
                }
                else if (gpu.Temperature <= 70 && gameMode || firstTime)
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
