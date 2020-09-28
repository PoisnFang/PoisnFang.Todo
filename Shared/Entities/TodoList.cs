using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo.Entities
{
    public class TodoList : Entity
    {
        public int SiteId { get; set; }
        public int TodoUserId { get; set; }
        public TodoUser TodoUser { get; set; }
        public string Name { get; set; }

        public ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    }
}