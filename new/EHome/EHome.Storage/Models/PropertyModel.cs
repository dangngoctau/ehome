namespace EHome.Storage.Models
{
    public class PropertyModel : EntityBaseModel
    {
        public int Type { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
    }
}
