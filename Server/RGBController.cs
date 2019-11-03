using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SDKs;

namespace Server
{
    enum MODE
    {
        INITIAL,
        WORK,
        GAME
    }

    class RGBController
    {

        int threshold_ = 60;
        int interval_ = 1000;
        MODE currentMode_ = MODE.INITIAL;

        Gigabyte gigabyte_;
        Nvidia nvidia_;
        Corsair corsair_;

        public RGBController()
        {
            nvidia_ = new Nvidia();
            gigabyte_ = new Gigabyte();
            corsair_ = new Corsair();

            corsair_.Init();
        }

        void ActivateGameMode()
        {
            if (currentMode_ == MODE.GAME)
            {
                return;
            }

            bool immediate = false;

            if (currentMode_ == MODE.INITIAL)
            {
                immediate = true;
            }

            Thread g = new Thread(() => gigabyte_.ToRed(immediate));
            g.SetApartmentState(ApartmentState.STA);
            g.Start();

            Thread c = new Thread(() => corsair_.ToRed(immediate));
            c.Start();

            g.Join();
            c.Join();
            currentMode_ = MODE.GAME;
        }

        void ActivateWorkMode()
        {
            if (currentMode_ == MODE.WORK)
            {
                return;
            }

            bool immediate = false;
            if (currentMode_ == MODE.INITIAL)
            {
                immediate = true;
            }

            Thread g = new Thread(() => gigabyte_.ToCyan(immediate));
            g.SetApartmentState(ApartmentState.STA);
            g.Start();

            Thread c = new Thread(() => corsair_.ToCyan(immediate));
            c.Start();

            g.Join();
            c.Join();
            currentMode_ = MODE.WORK;
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
