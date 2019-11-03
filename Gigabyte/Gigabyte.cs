using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Threading;

namespace SDKs
{
    public class Gigabyte
    {
        private RGBFusion fusion_;
        private const int TIMEPERFRAME = 190;
        private int duration_ = 10000;

        private Color RED = Color.FromRgb(255, 0, 0);
        private Color CYAN = Color.FromRgb(0, 255, 255);

        public Gigabyte()
        {
            fusion_ = new RGBFusion();

            fusion_.Init();
        }

        public void SetDuration(int duration)
        {
            duration_ = duration;
        }

        public void ToRed(bool immediate)
        {
            if (immediate)
            {
                fusion_.SetAllAreas(RED);
                return;
            }

            for (double i = 0; i < 1; i += (double)TIMEPERFRAME / duration_)
            {
                int val = (int)((1 - Math.Pow(i - 1, 2)) * 255);

                byte r = (byte)val;
                byte g = (byte)(255 - val);
                byte b = (byte)(255 - val);

                fusion_.SetAllAreas(Color.FromRgb(r, g, b));
            }
        }
        public void ToCyan(bool immediate)
        {
            if (immediate)
            {
                fusion_.SetAllAreas(CYAN);
                return;
            }

            for (double i = 0; i < 1; i += (double)TIMEPERFRAME / duration_)
            {
                int val = (int)((1 - Math.Pow(i - 1, 2)) * 255);

                byte r = (byte)(255 - val);
                byte g = (byte)val;
                byte b = (byte)val;

                fusion_.SetAllAreas(Color.FromRgb(r, g, b));
            }
        }
    }
}
