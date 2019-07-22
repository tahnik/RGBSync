
using NZXTSharp;
using NZXTSharp.KrakenX;

namespace RGBSync
{
    class NzxtRGBDevices : IRGBDevices
    {
        private KrakenX kraken = new KrakenX();

        public NzxtRGBDevices() { }

        void IRGBDevices.ActivateGameMode()
        {
            Fixed effect = new Fixed(new Color(255, 0, 0));
            kraken.ApplyEffect(effect);
        }

        void IRGBDevices.ActivateWorkMode()
        {
            Fixed effect = new Fixed(new Color(0, 255, 255));
            kraken.ApplyEffect(effect);
        }
    }
}
