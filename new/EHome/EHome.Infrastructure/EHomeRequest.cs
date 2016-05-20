namespace EHome.Infrastructure
{
    public class EHomeRequest : IRequest
    {
        public byte[] Message { get; set; }

        public string Topic { get; set; }
    }
}
