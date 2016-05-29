namespace EHome.Storage.Models
{
    public class DeviceModel: EntityBaseModel
    {
        public string Code { get; set; }
        public int ModuleId { get; set; }
        public string Properties { get; set; }
    }
}
