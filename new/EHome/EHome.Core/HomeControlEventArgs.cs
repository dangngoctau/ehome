namespace EHome.Core
{
    public class HomeControlEventArgs
    {
        public byte ModuleId { get; set; }
        public byte DeviceId { get; set; }
        public byte PropertyType { get; set; }
        public byte[] Data { get; set; }
    }
}
