namespace EHome.Infrastructure
{
    public class EHomeRequest : IRequest
    {
        public int PluginId { get; set; }

        public byte[] Message { get; set; }

        public string Topic { get; set; }
    }
}
