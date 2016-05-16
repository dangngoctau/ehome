using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Drivers.Implementations
{
    public class Af24Driver : IDriver
    {
        public DriverType Type
        {
            get
            {
                return DriverType.Af24;
            }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public DateTime Test()
        {
            return DateTime.Now;
        }
    }
}
