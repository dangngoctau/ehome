namespace EHome.Core
{
    public class HomeControlEventArgs
    {
        public short ModuleId { get; set; }
        public short DeviceId { get; set; }
        public short State { get; set; }
    }
}
