using PoisnFang.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo
{
    public class ModuleData
    {
        public string ModuleAction { get; set; } = string.Empty;
        public string UrlParameterTemplate { get; set; } = string.Empty;

        public TodoUser CurrentTodoUser { get; set; }

        public virtual TodoList TodoList { get; set; }
        public virtual TodoTask TodoTask { get; set; }
        public virtual Step Step { get; set; }
        public virtual List<TodoList> TodoLists { get; set; } = new List<TodoList>();
        public virtual List<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    }
}