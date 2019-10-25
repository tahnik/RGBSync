using AuraServiceLib;
using System;
using System.Timers;

namespace RGBSync
{
    class AsusRGBDevices : IRGBDevices
    {
        private State state = State.Initial;

        bool pending = false;

        /**
         * For some reason, once you enumarate a type of device
         * you cannot enumarate other device types anymore.
         * Which means that if you want to change color of two different types
         * of devices differently, you just need to load sdk twice.
         */
        IAuraSdk sdkDram = new AuraSdk();
        IAuraSdk sdkMotherboard = new AuraSdk();
        public AsusRGBDevices() { }

        void IRGBDevices.ActivateGameMode()
        {
            if (pending) { return; }
            pending = true;

            sdkDram.SwitchMode();
            IAuraSyncDeviceCollection dram = sdkDram.Enumerate(0x00070000);
            ToRedInstant(dram);

            sdkMotherboard.SwitchMode();
            IAuraSyncDeviceCollection motherboard = sdkMotherboard.Enumerate(0x00010000);
            ToRed(motherboard);
        }

        void IRGBDevices.ActivateWorkMode()
        {
            if (pending) { return; }
            pending = true;

            sdkDram.SwitchMode();
            IAuraSyncDeviceCollection dram = sdkDram.Enumerate(0x00070000);
            ToCyanInstant(dram);

            sdkMotherboard.SwitchMode();
            IAuraSyncDeviceCollection motherboard = sdkMotherboard.Enumerate(0x00010000);
            ToCyan(motherboard);
        }


        void ToCyan(IAuraSyncDeviceCollection devices)
        {
            int i = 255;

            Timer timer = new Timer() { Enabled = true, Interval = 25 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
            //while(true)
            // {
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
                    timer.Stop();
                    state = State.Cyan;
                    pending = false;
                    // break;
                }

                i--;
            //}
            };
        }
        void ToRedInstant(IAuraSyncDeviceCollection devices)
        {
            foreach (IAuraSyncDevice dev in devices)
            {
                foreach (IAuraRgbLight light in dev.Lights)
                {
                    Console.WriteLine(light.Name);
                    light.Red = 255;
                    light.Green = 0;
                    light.Blue = 0;
                }
                dev.Apply();
            }

            state = State.Red;
            pending = false;
        }
        void ToCyanInstant(IAuraSyncDeviceCollection devices)
        {
            foreach (IAuraSyncDevice dev in devices)
            {
                foreach (IAuraRgbLight light in dev.Lights)
                {
                    Console.WriteLine(light.Name);
                    light.Red = 0;
                    light.Green = 255;
                    light.Blue = 255;
                }
                dev.Apply();
            }

            state = State.Cyan;
            pending = false;
        }
        void ToRed(IAuraSyncDeviceCollection devices)
        {
            int i = 255;

            Timer timer = new Timer() { Enabled = true, Interval = 25 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
            // while(true)
            // {
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
                    timer.Stop();
                    state = State.Red;
                    pending = false;
                    // break;
                }

                i--;
            // }
            };
        }
    }
}
