using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTL.Core;

namespace JTL.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var jenkinsUrl = "https://builds.apache.org/job/Accumulo-1.3.x/rssAll";
            var usbPortName = "COM3";

            var monitor = new BuildMonitor(new JenkinsRssFeed(jenkinsUrl));
            var light = new BuildLight(new TrafficLightController(new UsbPort(usbPortName)));
            
            light.TurnOffAllLights();
            
            while (true)
            {
                var currentStatus = monitor.GetCurrentBuildStatus();

                light.Update(currentStatus);

                System.Console.WriteLine("Current status: {0} {1}", currentStatus, DateTime.Now.ToShortTimeString());

                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}
