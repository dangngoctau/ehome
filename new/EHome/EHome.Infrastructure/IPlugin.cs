using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Infrastructure
{
    public interface IPlugin
    {
        int Id { get; }

        void Execute(IRequest request);
    }
}
