using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Drivers
{
    public interface IDriver
    {
        DriverType Type { get; }
        DateTime Test();
        void Start();
        void Stop();
    }
}
