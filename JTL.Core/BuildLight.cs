using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTL.Core
{
    public class BuildLight
    {
        private readonly ITrafficLightController _lightController;

        public BuildLight(ITrafficLightController lightController)
        {
            _lightController = lightController;
        }

        public void Off()
        {
            _lightController.TurnOff(Light.All);
        }

        public void Update(BuildStatus status)
        {
            switch (status)
            {
                case BuildStatus.Unknown:
                    _lightController.TurnOff(Light.Red);
                    _lightController.TurnOff(Light.Yellow);
                    _lightController.TurnOff(Light.Green);
                    break;
                case BuildStatus.Successful:
                    _lightController.TurnOff(Light.Red);
                    _lightController.TurnOff(Light.Yellow);
                    _lightController.TurnOn(Light.Green);
                    break;
                case BuildStatus.Broken:
                    _lightController.TurnOn(Light.Red);
                    _lightController.TurnOff(Light.Yellow);
                    _lightController.TurnOff(Light.Green);
                    break;
                case BuildStatus.TestFailures:
                    _lightController.TurnOff(Light.Red);
                    _lightController.TurnOn(Light.Yellow);
                    _lightController.TurnOff(Light.Green);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }
    }
}
