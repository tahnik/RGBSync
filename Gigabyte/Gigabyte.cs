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
        private Mode currentMode_ = Mode.INITIAL;
        private const int TIMEPERFRAME = 152;
        private int duration_ = 30000;

        public Gigabyte()
        {
            fusion_ = new RGBFusion();
        }

        public void Init()
        {
            fusion_.Init();
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
                for (double i = 0; i < 1; i += (double)TIMEPERFRAME / duration_)
                {
                    int val = (int)((1 - Math.Pow(i - 1, 2)) * 255);

                    byte r = (byte)val;
                    byte g = (byte)(255 - val);
                    byte b = (byte)(255 - val);

                    fusion_.SetAllAreas(Color.FromRgb(r, g, b));
                }
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
                for (double i = 0; i < 1; i += (double)TIMEPERFRAME / duration_)
                {
                    int val = (int)((1 - Math.Pow(i - 1, 2)) * 255);

                    byte r = (byte)(255 - val);
                    byte g = (byte)val;
                    byte b = (byte)val;

                    fusion_.SetAllAreas(Color.FromRgb(r, g, b));
                }
            }
            currentMode_ = Mode.WORK;
        }
    }
}
