
using NZXTSharp;
using NZXTSharp.KrakenX;
using System.Timers;

namespace RGBSync
{
    class NzxtRGBDevices : IRGBDevices
    {
        private KrakenX kraken = new KrakenX();

        bool pending = false;
        private State state = State.Initial;

        public NzxtRGBDevices() { }

        void IRGBDevices.ActivateGameMode()
        {
            if (pending) { return;  }

            pending = true;
            int i = 255;

            Timer timer = new Timer() { Enabled = true, Interval = 25 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                int toSub = state == State.Initial ? 255 : 255 - i;
                int r = toSub;

                int g = i;
                int b = i;

                Fixed effect = new Fixed(new Color(r, g ,b));
                kraken.ApplyEffect(effect);

                if (i == 0)
                {
                    timer.Stop();
                    state = State.Red;
                    pending = false;
                }

                i--;
            };
        }

        void IRGBDevices.ActivateWorkMode()
        {
            if (pending) { return;  }

            pending = true;
            int i = 255;

            Timer timer = new Timer() { Enabled = true, Interval = 25 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                int r = state == State.Initial ? 0 : i;

                int toSub = state == State.Initial ? 255 : 255 - i;
                int g = toSub;
                int b = toSub;

                Fixed effect = new Fixed(new Color(r, g, b));
                kraken.ApplyEffect(effect);

                if (i == 0)
                {
                    timer.Stop();
                    state = State.Cyan;
                    pending = false;
                }

                i--;
            };
        }
    }
}
