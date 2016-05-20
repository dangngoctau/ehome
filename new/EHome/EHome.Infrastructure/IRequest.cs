using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Infrastructure
{
    public interface IRequest
    {
        byte[] Message { get; set; }
        string Topic { get; set; }
    }
}
