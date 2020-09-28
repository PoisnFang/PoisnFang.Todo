using PoisnFang.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo.Services
{
    public interface ITodoRepoServiceApi : IDisposable
    {
        ITodoRepoService<TodoList> TodoLists { get; }
        ITodoRepoService<TodoTask> TodoTasks { get; }
        ITodoRepoService<TodoUser> TodoUsers { get; }
        ITodoRepoService<Step> Steps { get; }
    }
}