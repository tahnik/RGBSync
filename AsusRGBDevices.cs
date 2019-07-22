using AuraServiceLib;
using OpenHardwareMonitor.Hardware;

namespace RGBSync
{
    class AsusRGBDevices : IRGBDevices
    {
        private static uint RED = 0x000000FF;
        private static uint CYAN = 0x00FFFF00;

        IAuraSdk sdk = new AuraSdk();
        public AsusRGBDevices() { }

        void IRGBDevices.ActivateGameMode()
        {
            ChangeColour(RED);
        }

        void IRGBDevices.ActivateWorkMode()
        {
            ChangeColour(CYAN);
        }

        void ChangeColour(uint color)
        {
            sdk.SwitchMode();
            IAuraSyncDeviceCollection devices = sdk.Enumerate(0);

            foreach (IAuraSyncDevice dev in devices)
            {
                foreach (IAuraRgbLight light in dev.Lights)
                {
                    light.Color = color;
                }
                dev.Apply();
            }
        }
    }
}
