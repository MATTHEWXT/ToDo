using Microsoft.AspNetCore.Components.Web;
using ToDo.API.Domain.Core.Models;

namespace ToDo.API.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid CategoryId { get; set; }
        public TaskCategory Category { get; set; }

        public TaskItem(string name, string description) {
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
