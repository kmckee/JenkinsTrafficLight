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
        [TestCase(Light.Red, "1", TestName="Turning on the red light should cause a '1' to be written to the usb port.")]
        [TestCase(Light.Yellow, "2", TestName = "Turning on the yellow light should cause a '2' to be written to the usb port.")]
        [TestCase(Light.Green, "3", TestName = "Turning on the green light should cause a '2' to be written to the usb port.")]
        [TestCase(Light.All, "123", TestName = "Turning on all the lights should cause '123' to be written to the usb port.")]
        public void Turning_on_lights(Light lightColor, string expectedOutputToUsbPort)
        {
            var portMock = Substitute.For<IUsbPort>();
            var controller = new TrafficLightController(portMock);
            controller.TurnOn(lightColor);

            portMock.Received(1).Write(expectedOutputToUsbPort);
        }

        [Test]
        [TestCase(Light.Red, "5", TestName = "Turning off the red light should cause a '5' to be written to the usb port.")]
        [TestCase(Light.Yellow, "6", TestName = "Turning off the yellow light should cause a '6' to be written to the usb port.")]
        [TestCase(Light.Green, "7", TestName = "Turning off the green light should cause a '7' to be written to the usb port.")]
        [TestCase(Light.All, "567", TestName = "Turning off all the lights should cause '567' to be written to the usb port.")]
        public void Turning_off_lights(Light lightColor, string expectedOutputToUsbPort)
        {
            var portMock = Substitute.For<IUsbPort>();
            var controller = new TrafficLightController(portMock);
            controller.TurnOff(lightColor);

            portMock.Received(1).Write(expectedOutputToUsbPort);
        }

    }
}
