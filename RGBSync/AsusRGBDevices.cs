using AuraServiceLib;
using System;
using System.Timers;

namespace RGBSync
{
    class AsusRGBDevices : IRGBDevices
    {
        private State state = State.Initial;

        bool pending = false;

        IAuraSdk sdk = new AuraSdk();
        public AsusRGBDevices() { }

        void IRGBDevices.ActivateGameMode()
        {
            if (pending) { return; }
            pending = true;

            sdk.SwitchMode();

            IAuraSyncDeviceCollection motherboard = sdk.Enumerate(0x00000000);

            ToRed(motherboard);
        }

        void IRGBDevices.ActivateWorkMode()
        {
            if (pending) { return; }
            pending = true;

            sdk.SwitchMode();

            IAuraSyncDeviceCollection motherboard = sdk.Enumerate(0x00000000);

            ToCyan(motherboard);
        }


        void ToCyan(IAuraSyncDeviceCollection devices)
        {
            int i = 255;

            /*
            Timer timer = new Timer() { Enabled = true, Interval = 25 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
            */
            while(true)
            {
                foreach (IAuraSyncDevice dev in devices)
                {
                    foreach (IAuraRgbLight light in dev.Lights)
                    {
                        light.Red = state == State.Initial ? (byte)0 : (byte)i;

                        byte toSub = state == State.Initial ? (byte)255 : (byte)(255 - i);
                        light.Green = toSub;
                        light.Blue = toSub;
                    }
                    dev.Apply();
                }

                if (i == 0)
                {
                    // timer.Stop();
                    state = State.Cyan;
                    pending = false;
                    break;
                }

                i--;
            }
            // };
        }
        void ToRed(IAuraSyncDeviceCollection devices)
        {
            int i = 255;

            /*
            Timer timer = new Timer() { Enabled = true, Interval = 25 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
            */
            while(true)
            {
                foreach (IAuraSyncDevice dev in devices)
                {
                    foreach (IAuraRgbLight light in dev.Lights)
                    {
                        byte toSub = state == State.Initial ? (byte) 255 : (byte)(255 - i);
                        light.Red = toSub;

                        light.Green = (byte) i;
                        light.Blue = (byte) i;
                    }
                    dev.Apply();
                }

                if (i == 0)
                {
                    // timer.Stop();
                    state = State.Red;
                    pending = false;
                    break;
                }

                i--;
            }
            // };
        }
    }
}
