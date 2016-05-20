using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Infrastructure
{
    public interface IPlugin
    {
        string DriverType { get; }

        void Execute(IRequest request);
    }
}
