using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Common
{
    public interface IAppLogger
    {
        void Error(string message, Exception ex);
    }
}
