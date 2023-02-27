using TestProject.Domain.Common;

namespace TestProject.Domain
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
    }
}