using System.Collections.Generic;

namespace RGBSync
{
    public enum Vendor { CORSAIR, ASUS, NZXT };

    class RGBDevices : IRGBDevices
    {
        List<IRGBDevices> activeVendors = new List<IRGBDevices>();
        public RGBDevices(Vendor[] vs)
        {
            foreach(Vendor v in vs)
            {
                AddVendor(v);
            }
        }
        
        void AddVendor(Vendor v)
        {
            switch(v)
            {
                case Vendor.CORSAIR:
                    activeVendors.Add(new CorsairRGBDevices());
                    break;
                case Vendor.ASUS:
                    activeVendors.Add(new AsusRGBDevices());
                    break;
                case Vendor.NZXT:
                    activeVendors.Add(new NzxtRGBDevices());
                    break;
                default:
                    break;
            }
        }

        void IRGBDevices.ActivateGameMode()
        {
            foreach(IRGBDevices vendor in activeVendors)
            {
                vendor.ActivateGameMode();
            }
        }

        void IRGBDevices.ActivateWorkMode()
        {
            foreach(IRGBDevices vendor in activeVendors)
            {
                vendor.ActivateWorkMode();
            }
        }
    }
}
