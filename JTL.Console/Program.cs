using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using JTL.Core;

namespace JTL.Console
{
    class Program
    {
        // This class really needs some love.  There's a lot of behavior in here that should be pushed out into other classes that can be unit tests.

        static void Main(string[] args)
        {
            string jenkinsUrl;

            if (args.Length == 1)
                jenkinsUrl = args[0];
            else
            {
                System.Console.Write("Enter the Jenkins RSS feed url: ");
                jenkinsUrl = System.Console.ReadLine();
            }


            var feed = new JenkinsRssFeed(jenkinsUrl);
            var monitor = new BuildMonitor(feed);
            var port = GetTrafficLightPort();
            if (port == null)
                return;

            var light = new BuildLight(new TrafficLightController(port));
            
            light.TurnOffAllLights();
            
            while (true)
            {
                var currentStatus = GetCurrentStatus(monitor, feed);

                light.Update(currentStatus);

                System.Console.WriteLine("Current status: {0} {1}", currentStatus, DateTime.Now.ToShortTimeString());

                System.Threading.Thread.Sleep(10000);
            }
        }

        private static UsbPort GetTrafficLightPort()
        {
            var possiblePorts = SerialPort.GetPortNames();

            if (possiblePorts.Count() == 0)
            {
                System.Console.WriteLine("Traffic light not detected.  Check the connection and restart.");
                return null;
            }
            else if (possiblePorts.Count() == 1)
            {
                return new UsbPort(possiblePorts.First());
            }
            else
            {
                System.Console.WriteLine("Multiple ports detected - " + string.Join(", ", possiblePorts));
                System.Console.Write("Please enter the port name of the traffic light: ");
                return new UsbPort(System.Console.ReadLine());
            }
        }

        private static BuildStatus GetCurrentStatus(BuildMonitor monitor, JenkinsRssFeed feed)
        {
            try
            {
                return monitor.GetCurrentBuildStatus();
            }
            catch (AuthorizationFailedException)
            {
                System.Console.Write("Please enter Jenkins Username: ");
                feed.UserName = System.Console.ReadLine();

                System.Console.Write("Please enter Jenkins Password: ");
                feed.Password = System.Console.ReadLine();

                System.Console.Clear();

                try
                {
                    return monitor.GetCurrentBuildStatus();
                }
                catch (AuthorizationFailedException)
                {
                    System.Console.WriteLine("Invalid Jenkins username / password combination.  Please try again.");
                    return GetCurrentStatus(monitor, feed);
                }
                
            }
        }
    }
}
