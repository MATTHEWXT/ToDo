using ToDo.API.Domain.Core.Models;

namespace ToDo.API.Domain.Entities
{
    public class MissionCategory : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MissionItem> Items { get; set; } = new List<MissionItem>();

        public MissionCategory(string name) {
            Name = name;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name of TaskCategory is required.");
        }
    }
}
