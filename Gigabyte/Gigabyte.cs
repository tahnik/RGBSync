using System;
using System.Diagnostics;
using System.Windows.Media;

namespace SDKs
{
    public class Gigabyte
    {
        enum Mode
        {
            INITIAL,
            GAME,
            WORK
        }

        private RGBFusion fusion_;
        private const int TIMEPERFRAME = 500;
        private int duration_ = 30000;
        private Mode currentMode_ = Mode.INITIAL;

        public Gigabyte()
        {
            fusion_ = new RGBFusion();
        }

        public void Init()
        {
            fusion_.Init();
        }

        public void SetDuration(int duration)
        {
            duration_ = duration;
        }

        public void ToGameMode()
        {
            if (!fusion_.IsInitialized() || currentMode_ == Mode.GAME)
            {
                return;
            }

            if (currentMode_ == Mode.INITIAL)
            {
                fusion_.SetAllAreas(Color.FromRgb(255, 0, 0));
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                for (double i = 0; i <= 255; i += 1)
                {
                    // int val = (int)((1 - Math.Pow(i - 1, 2)) * 255);

                    byte r = (byte)i;
                    byte g = (byte)(255 - i);
                    byte b = (byte)(255 - i);


                    fusion_.SetAllAreas(Color.FromRgb(r, g, b));
                }

                sw.Stop();
                Console.WriteLine("Elapsed={0}", sw.Elapsed);

            }
            currentMode_ = Mode.GAME;
        }
        public void ToWorkMode()
        {
            if (!fusion_.IsInitialized() || currentMode_ == Mode.WORK)
            {
                return;
            }

            if (currentMode_ == Mode.INITIAL)
            {
                fusion_.SetAllAreas(Color.FromRgb(0, 255, 255));
            }
            else
            {
                // for (double i = 0; i < 1; i += (double)TIMEPERFRAME / duration_)
                for (double i = 0; i <= 255; i += 1)
                {
                    // int val = (int)((1 - Math.Pow(i - 1, 2)) * 255);

                    byte r = (byte)(255 - i);
                    byte g = (byte)i;
                    byte b = (byte)i;

                    fusion_.SetAllAreas(Color.FromRgb(r, g, b));
                }
            }
            currentMode_ = Mode.WORK;
        }
    }
}
