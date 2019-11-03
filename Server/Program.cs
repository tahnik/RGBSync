using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKs;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Gigabyte g = new Gigabyte();

            Thread gThread = new Thread(() => g.ToRed(false));
            gThread.SetApartmentState(ApartmentState.STA);
            gThread.Start();
            gThread.Join();
            */

            RGBController controller = new RGBController();

            Thread init = new Thread(controller.Init);
            init.Start();

            Thread watcher = new Thread(controller.WatchTemperature);
            watcher.Start();

            init.Join();
            watcher.Join();
        }
    }
}
