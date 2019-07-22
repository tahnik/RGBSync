using RGB.NET.Core;
using RGB.NET.Devices.Corsair;
using System;
using System.Timers;

namespace RGBSync
{
    class CorsairRGBDevices : IRGBDevices
    {
        const int COLOR_RANGE_MAX = 255;

        Mode currentMode = Mode.WORK;
        bool pending = false;

        RGBSurface surface = RGBSurface.Instance;
        CorsairDeviceProvider corsair = CorsairDeviceProvider.Instance;

        public CorsairRGBDevices()
        {
            Timer timer = new Timer() { Enabled = true, Interval = 5000 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                try
                {
                    corsair.Initialize(exclusiveAccessIfPossible: true, throwExceptions: true);
                    surface.LoadDevices(corsair);
                    ReactivateCurrentMode();
                    // Task.Delay(5000).ContinueWith(t => ReactivateCurrentMode());
                    timer.Stop();
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load Corsair SDK");
                }
            };
        }

        void ReactivateCurrentMode()
        {
            Console.WriteLine("Reactivating the current mode");
            if (currentMode == Mode.GAME)
            {
                ActivateGameMode();
            }
            else
            {
                ActivateWorkMode();
            }
        }

        public void ActivateGameMode()
        {
            ActivateMode(Mode.GAME);
            currentMode = Mode.GAME;
        }

        public void ActivateWorkMode()
        {
            ActivateMode(Mode.WORK);
            currentMode = Mode.WORK;
        }
        void ActivateMode(Mode mode)
        {
            // If there is transition pending already, skip it
            if (pending) { return; }
            pending = true;

            int i = 0;

            // Slowly change the color
            Timer timer = new Timer() { Enabled = true, Interval = 10 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                if (i != COLOR_RANGE_MAX) { i += 1; };

                ToMode(mode);

                if (i == COLOR_RANGE_MAX)
                {
                    pending = false;
                    timer.Stop();
                }
            };
        }

        void ToMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.WORK:
                    ToCyan();
                    break;
                case Mode.GAME:
                    ToRed();
                    break;
                default:
                    ToCyan();
                    break;
            }
        }

        void ToCyan()
        {
            foreach (Led LED in surface.Leds)
            {
                int a = LED.Color.GetA() + 1;
                int r = LED.Color.GetR() - 1;
                int g = LED.Color.GetG() + 1;
                int b = LED.Color.GetB() + 1;

                LED.Color = new Color(a, r, g, b);
            }
            surface.Update(true);
        }
        void ToRed()
        {
            foreach (Led LED in surface.Leds)
            {
                int a = LED.Color.GetA() + 1;
                int r = LED.Color.GetR() + 1;
                int g = LED.Color.GetG() - 1;
                int b = LED.Color.GetB() - 1;

                LED.Color = new Color(a, r, g, b);
            }
            surface.Update(true);
        }
    }
}
