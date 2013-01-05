using System;
using System.IO.Ports;

namespace JTL.Core
{
    public class UsbPort : IUsbPort, IDisposable
    {
        private readonly SerialPort _port;

        public UsbPort(string portName)
        {
            _port = new SerialPort(portName, 9600);
            _port.Open();
        }

        public void Write(string textToWrite)
        {
            if (!_port.IsOpen)
                _port.Open();

            _port.Write(textToWrite);
        }

        public void Dispose()
        {
            if (_port.IsOpen)
                _port.Close();
        }
    }
}
