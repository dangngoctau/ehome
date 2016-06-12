using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EHome.Storage.ViewModels
{
    public class GroupDeviceViewModel
    {
        public GroupDeviceViewModel()
        {
            Devices = new Collection<DeviceViewModel>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<DeviceViewModel> Devices { get; set; }
    }
}
