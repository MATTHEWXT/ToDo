using Microsoft.AspNetCore.Components.Web;
using ToDo.API.Domain.Core.Models;

namespace ToDo.API.Domain.Entities
{
    public class MissionItem : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public MissionCategory Category { get; set; } = null!;

        public MissionItem(string name, string description) {
            Name = name;
            Description = description;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                Name = "My task";
            if(string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description is required.");
        }
    }
}
