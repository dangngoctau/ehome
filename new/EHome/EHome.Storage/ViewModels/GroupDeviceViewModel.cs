using System.Collections.Generic;
using System.Collections.ObjectModel;
using EHome.Storage.Models;

namespace EHome.Storage.ViewModels
{
    public class GroupDeviceViewModel
    {
        public GroupDeviceViewModel()
        {
            Devices = new Collection<DeviceViewModel>();
        }

        public GroupModel Group { get; set; }
        public ICollection<DeviceViewModel> Devices { get; set; }
    }
}
