namespace EHome.Core
{
    public class HomeControlEventArgs
    {
        public byte ModuleId { get; set; }
        public byte DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
        public byte[] Data { get; set; }
    }
}
