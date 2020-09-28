using Oqtane.Modules;
using Oqtane.Shared;
using PoisnFang.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PoisnFang.Todo.Services
{
    public class TodoRepoServiceApi : ITodoRepoServiceApi, IService
    {
        public ITodoRepoService<TodoList> TodoLists { get; private set; }
        public ITodoRepoService<TodoTask> TodoTasks { get; private set; }
        public ITodoRepoService<TodoUser> TodoUsers { get; private set; }
        public ITodoRepoService<Step> Steps { get; private set; }

        public TodoRepoServiceApi(HttpClient http, SiteState siteState)
        {
            TodoLists = new TodoRepoService<TodoList>(http, siteState, nameof(TodoLists));
            TodoTasks = new TodoRepoService<TodoTask>(http, siteState, nameof(TodoTasks));
            TodoUsers = new TodoRepoService<TodoUser>(http, siteState, nameof(TodoUsers));
            Steps = new TodoRepoService<Step>(http, siteState, nameof(Steps));
        }

        public void Dispose()
        {
        }
    }
}