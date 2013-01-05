using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTL.Core
{
    public interface IUsbPort
    {
        void Write(string textToWrite);
    }
}
