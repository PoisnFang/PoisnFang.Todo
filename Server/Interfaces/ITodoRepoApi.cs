using PoisnFang.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo.Repository
{
    public interface ITodoRepoApi : IDisposable
    {
        ITodoRepository<TodoList> TodoLists { get; }
        ITodoRepository<TodoTask> TodoTasks { get; }
        ITodoRepository<TodoUser> TodoUsers { get; }
        ITodoRepository<Step> Steps { get; }
    }
}