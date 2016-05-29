using System.Collections.Generic;
using EHome.Storage.Models;

namespace EHome.Storage.ViewModels
{
    public class DeviceViewModel
    {
        public DeviceModel Device { get; set; }
        public IEnumerable<PropertyModel> Properties { get; set; }
        public ModuleModel Module { get; set; }
    }
}
