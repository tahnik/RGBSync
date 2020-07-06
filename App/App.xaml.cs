using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RGBSync
{
    public partial class App : Application
    {
        Thread init;
        Thread watcher;
        private void onStartUp(object sender, StartupEventArgs e)
        {
            Tray tray = new Tray();
            tray.addExitHandler(new EventHandler(onExit));

            startRGBController();
        }
        private async void startRGBController()
        {
            RGBController controller = new RGBController();

            init = new Thread(controller.Init);
            watcher = new Thread(controller.WatchTemperature);

            await Task.Run(() =>
            {
                init.Start();

                watcher = new Thread(controller.WatchTemperature);
                watcher.Start();

                init.Join();
                watcher.Join();
            });

        }

        private void onExit(object Sender, EventArgs e)
        {
            init.Abort();
            watcher.Abort();

            Current.Shutdown();
        }

    }
}
