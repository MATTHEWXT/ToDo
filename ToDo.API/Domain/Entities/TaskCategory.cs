using ToDo.API.Domain.Core.Models;

namespace ToDo.API.Domain.Entities
{
    public class TaskCategory : BaseEntity
    {
        public string Name { get; set; }
        public List<TaskItem> Items { get; set; } = new List<TaskItem>();

        public TaskCategory(string name) {
            Name = name;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name of TaskCategory is required.");
        }
    }
}
