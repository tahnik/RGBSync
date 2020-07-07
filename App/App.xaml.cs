using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RGBSync
{
    public partial class App : Application
    {

        private void onStartUp(object sender, StartupEventArgs e)
        {
            Tray tray = new Tray();
            tray.addExitHandler(new EventHandler(onExit));

            RGBController controller = new RGBController();

            controller.WatchTemperature();
            controller.Init();
        }

        private void onExit(object Sender, EventArgs e)
        {
            Current.Shutdown();
        }

    }
}
