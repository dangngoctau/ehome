using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EHome.Storage.ViewModels
{
    public class DeviceViewModel
    {
        public DeviceViewModel()
        {
            States = new Collection<DeviceStateViewModel>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<DeviceStateViewModel> States { get; set; }
    }
}
