using System;

namespace EHome.Storage.Models
{
    public class DeviceStateModel
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
