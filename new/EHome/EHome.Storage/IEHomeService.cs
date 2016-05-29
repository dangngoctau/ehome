using System.Collections.Generic;
using System.Threading.Tasks;
using EHome.Storage.Models;
using EHome.Storage.ViewModels;

namespace EHome.Storage
{
    public interface IEHomeService
    {
        Task<IEnumerable<DeviceModel>> GetDevicesAsync();
        Task<IEnumerable<ModuleModel>> GetModulesAsync();
        Task<IEnumerable<GroupDeviceViewModel>> GetGroupDevicesAsync();
        Task UpdateDeviceStateAsync(int deviceId, string value);
    }
}