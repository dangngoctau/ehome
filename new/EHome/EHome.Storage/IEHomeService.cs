using System.Collections.Generic;
using EHome.Storage.Models;

namespace EHome.Storage
{
    public interface IEHomeService
    {
        IEnumerable<Device> GetDevices();
    }
}