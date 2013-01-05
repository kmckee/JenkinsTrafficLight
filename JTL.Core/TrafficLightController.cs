using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTL.Core
{
    public class TrafficLightController
    {
        private IUsbPort _port;

        public TrafficLightController(IUsbPort port)
        {
            _port = port;
        }

        public void TurnOn(Light lightToTurnOn)
        {
            switch (lightToTurnOn)
            {
                case Light.Red:
                    _port.Write("1");
                    break;
                case Light.Yellow:
                    _port.Write("2");
                    break;
                case Light.Green:
                    _port.Write("3");
                    break;
                case Light.All:
                    _port.Write("123");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("lightToTurnOn");
            }
            
        }

        public void TurnOff(Light lightToTurnOff)
        {
            switch (lightToTurnOff)
            {
                case Light.Red:
                    _port.Write("5");
                    break;
                case Light.Yellow:
                    _port.Write("6");
                    break;
                case Light.Green:
                    _port.Write("7");
                    break;
                case Light.All:
                    _port.Write("567");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("lightToTurnOff");
            }
        }
    }
}
