using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SDKs;

namespace RGBSync
{
    class RGBController
    {

        int threshold_ = 60;
        int interval_ = 1000;

        Gigabyte gigabyte_;
        Nvidia nvidia_;
        Corsair corsair_;

        public RGBController()
        {
            nvidia_ = new Nvidia();
            gigabyte_ = new Gigabyte();
            corsair_ = new Corsair();

        }

        public async void Init()
        {
            ConcurrentBag<Task> tasks = new ConcurrentBag<Task>
            {
                Task.Run(() => gigabyte_.Init()),
                Task.Run(() => corsair_.Init())
            };

            await Task.WhenAll(tasks.ToArray());

            Console.WriteLine("Successfully initialised RGBSync");
        }

        private async Task ActivateGameMode()
        {
            ConcurrentBag<Task> tasks = new ConcurrentBag<Task>
            {
                Task.Run(() => gigabyte_.ToGameMode()),
                Task.Run(() => corsair_.ToGameMode())
            };

            await Task.WhenAll(tasks.ToArray());
        }

        private async Task ActivateWorkMode()
        {
            ConcurrentBag<Task> tasks = new ConcurrentBag<Task>
            {
                Task.Run(() => gigabyte_.ToWorkMode()),
                Task.Run(() => corsair_.ToWorkMode())
            };

            await Task.WhenAll(tasks.ToArray());
        }

        public async void WatchTemperature()
        {
            while (true)
            {
                int currentTemp = nvidia_.GetTemperature();

                if (currentTemp > threshold_)
                {
                    await ActivateGameMode();
                }
                else
                {
                    await ActivateWorkMode();
                }

                await Task.Delay(interval_);            }
        }
    }
}
