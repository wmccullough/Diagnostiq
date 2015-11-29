using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Diagnostiq.Worker.Controllers
{
    public class CounterWorkerController : IWorkerController
    {
        public CounterWorkerController(TimeSpan startDelayTimeSpan, TimeSpan pollingIntervalTimeSpan)
        {
            StartDelayTimeSpan = startDelayTimeSpan;
            PollingIntervalTimeSpan = pollingIntervalTimeSpan;
        }

        public Timer HarvestTimer { get; set; }
        protected TimeSpan StartDelayTimeSpan { get; set; }
        protected TimeSpan PollingIntervalTimeSpan { get; set; }

        private PerformanceCounter theCPUCounter =
            new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

        private PerformanceCounter theMemCounter =
            new PerformanceCounter("Memory", "Available MBytes");

        public void Start()
        {
            HarvestTimer =
                new Timer(OnHarvest, this, StartDelayTimeSpan, PollingIntervalTimeSpan);
        }

        public void Stop()
        {
            HarvestTimer.Change(Timeout.Infinite, Timeout.Infinite);
            HarvestTimer.Dispose();
        }



        private void OnHarvest(object state)
        {
            float usage = theCPUCounter.NextValue();
            Thread.Sleep(1000);
            float usage2 = theCPUCounter.NextValue();

            float memory = theMemCounter.NextValue();

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            IDatabase database = redis.GetDatabase();

            Moment<double> cpuMoment = new Moment<double>("CPU", Math.Round((usage + usage2), 2), DateTime.UtcNow);
            Moment<float> memoryFreeMoment = new Moment<float>("Free Memory", memory, DateTime.UtcNow);

            string cpuMomentJson = JsonConvert.SerializeObject(cpuMoment);
            string memoryMomentJson = JsonConvert.SerializeObject(memoryFreeMoment);

            database.ListLeftPush("counters:cpu", cpuMomentJson, When.Always, CommandFlags.HighPriority);
            database.ListLeftPush("counters:freememory", memoryMomentJson, When.Always, CommandFlags.HighPriority);

            Console.WriteLine($"CPU: {Math.Round(usage + usage2, 2)}%");
            Console.WriteLine($"Free Memory: {memory}Mb");
        }

    }
}
