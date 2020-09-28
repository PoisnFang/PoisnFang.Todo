using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo.Entities
{
    public class Step : Entity
    {
        public int TodoTaskId { get; set; }
        public TodoTask TodoTask { get; set; }
        public string Name { get; set; }
        public bool IsCompeleted { get; set; }
    }
}