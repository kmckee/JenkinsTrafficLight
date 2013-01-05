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
    public class TrafficLightControllerTests
    {
        [Test]
        public void Turning_on_red_light_should_transmit_a_1_to_teensy()
        {
            var portMock = Substitute.For<IUsbPort>();
            var controller = new TrafficLightController(portMock);
            controller.TurnOn(Light.Red);

            portMock.Received(1).Write("1");
        }

        [Test]
        public void Turning_on_yellow_light_should_transmit_a_2_to_teensy()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Turning_on_green_light_should_transmit_a_3_to_teensy()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Turning_on_all_lights_should_transmit_a_123_to_teensy()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Turning_off_red_light_should_transmit_a_5_to_teensy()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Turning_off_yellow_light_should_transmit_a_6_to_teensy()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Turning_off_green_light_should_transmit_a_7_to_teensy()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Turning_off_all_lights_should_transmit_a_567_to_teensy()
        {
            Assert.Inconclusive();
        }
    }
}
