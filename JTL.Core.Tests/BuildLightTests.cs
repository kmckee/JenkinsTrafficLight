using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace JTL.Core.Tests
{
    [TestFixture]
    public class BuildLightTests
    {
        [Test]
        public void Turning_off_the_build_light_should_turn_off_all_individual_lights()
        {
            var lightController = Substitute.For<ITrafficLightController>();
            var light = new BuildLight(lightController);

            light.TurnOffAllLights();

            lightController.Received(1).TurnOff(Light.All);
        }

        [Test]
        public void The_green_light_should_be_the_only_light_on_with_a_successful_build()
        {
            var lightController = Substitute.For<ITrafficLightController>();
            var light = new BuildLight(lightController);

            light.Update(BuildStatus.Successful);

            lightController.Received(1).TurnOff(Light.Red);
            lightController.Received(1).TurnOff(Light.Yellow);
            lightController.Received(1).TurnOn(Light.Green);
        }

        [Test]
        public void The_yellow_light_should_be_the_only_light_on_with_failed_unit_tests()
        {
            var lightController = Substitute.For<ITrafficLightController>();
            var light = new BuildLight(lightController);

            light.Update(BuildStatus.TestFailures);

            lightController.Received(1).TurnOff(Light.Red);
            lightController.Received(1).TurnOn(Light.Yellow);
            lightController.Received(1).TurnOff(Light.Green);
        }

        [Test]
        public void The_red_light_should_be_the_only_light_on_with_a_failed_build()
        {
            var lightController = Substitute.For<ITrafficLightController>();
            var light = new BuildLight(lightController);

            light.Update(BuildStatus.Broken);

            lightController.Received(1).TurnOn(Light.Red);
            lightController.Received(1).TurnOff(Light.Yellow);
            lightController.Received(1).TurnOff(Light.Green);
        }

        [Test]
        public void All_lights_should_be_off_with_an_unknown_build_status()
        {
            var lightController = Substitute.For<ITrafficLightController>();
            var light = new BuildLight(lightController);

            light.Update(BuildStatus.Unknown);

            lightController.Received(1).TurnOff(Light.Red);
            lightController.Received(1).TurnOff(Light.Yellow);
            lightController.Received(1).TurnOff(Light.Green);
        }
    }
}
