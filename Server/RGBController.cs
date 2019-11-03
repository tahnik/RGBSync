using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SDKs;

namespace Server
{
    class RGBController
    {

        int threshold_ = 60;
        int interval_ = 1000;

        Gigabyte gigabyte_;
        Nvidia nvidia_;
        Corsair corsair_;

        public RGBController()
        {
            nvidia_ = new Nvidia();
            gigabyte_ = new Gigabyte();
            corsair_ = new Corsair();
        }

        public void Init()
        {
            Thread g = new Thread(gigabyte_.Init);
            g.SetApartmentState(ApartmentState.STA);
            g.Start();

            Thread c = new Thread(corsair_.Init);
            c.Start();

            g.Join();
            c.Join();
        }

        void ActivateGameMode()
        {
            Thread g = new Thread(gigabyte_.ToGameMode);
            g.SetApartmentState(ApartmentState.STA);
            g.Start();

            Thread c = new Thread(corsair_.ToGameMode);
            c.Start();

            g.Join();
            c.Join();
        }

        void ActivateWorkMode()
        {

            Thread g = new Thread(gigabyte_.ToWorkMode);
            g.SetApartmentState(ApartmentState.STA);
            g.Start();

            Thread c = new Thread(corsair_.ToWorkMode);
            c.Start();

            g.Join();
            c.Join();
        }

        public void WatchTemperature()
        {
            while(true)
            {
                int currentTemp = nvidia_.GetTemperature();

                if (currentTemp > threshold_)
                {
                    ActivateGameMode();
                }
                else
                {
                    ActivateWorkMode();
                }

                Thread.Sleep(interval_);
            }
        }
    }
}
