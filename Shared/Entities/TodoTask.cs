using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace PoisnFang.Todo.Entities
{
    public class TodoTask : Entity
    {
        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }

        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public string Note { get; set; }

        public bool IsImportant { get; set; }
        public bool IsCompeleted { get; set; }

        public ICollection<Step> Steps { get; set; } = new List<Step>();
    }
}