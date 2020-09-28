using Microsoft.Extensions.Configuration;
using Oqtane.Modules;
using PoisnFang.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo.Repository
{
    public class TodoRepoApi : ITodoRepoApi, IService
    {
        public ITodoRepository<TodoList> TodoLists { get; private set; }
        public ITodoRepository<TodoTask> TodoTasks { get; private set; }
        public ITodoRepository<TodoUser> TodoUsers { get; private set; }
        public ITodoRepository<Step> Steps { get; private set; }

        public TodoRepoApi(TodoContext todoContext, IConfigurationRoot config)
        {
            TodoLists = new TodoRepository<TodoList>(todoContext, config);
            TodoTasks = new TodoRepository<TodoTask>(todoContext, config);
            TodoUsers = new TodoRepository<TodoUser>(todoContext, config);
            Steps = new TodoRepository<Step>(todoContext, config);
        }

        public void Dispose()
        {
        }
    }
}