using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using PoisnFang.Todo.Entities;
using PoisnFang.Todo.Repository;
using Oqtane.Shared;
using Microsoft.EntityFrameworkCore;

namespace PoisnFang.Todo.Manager
{
    public class TodoManager : IInstallable, IPortable
    {
        private readonly ITodoRepoApi _todoRepo;
        private readonly ISqlRepository _sql;
        private readonly TodoContext _appDb;

        public TodoManager(ITodoRepoApi todoRepo, ISqlRepository sql, TodoContext appDb)
        {
            _todoRepo = todoRepo;
            _sql = sql;
            _appDb = appDb;
        }

        public bool Install(Tenant tenant, string version)
        {
            _appDb.InitializeTenant = tenant;
            _appDb.Database.Migrate();

            return true;

            //return _sql.ExecuteScript(tenant, GetType().Assembly, "PoisnFang.Todo." + version + ".sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "PoisnFang.Todo.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<TodoList> Todos = _todoRepo.TodoLists.GetAll().ToList();
            if (Todos != null)
            {
                content = JsonSerializer.Serialize(Todos);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<TodoList> Todos = null;
            if (!string.IsNullOrEmpty(content))
            {
                Todos = JsonSerializer.Deserialize<List<TodoList>>(content);
            }
            if (Todos != null)
            {
                foreach (TodoList Todo in Todos)
                {
                    TodoList todolist = new TodoList
                    {
                        Name = Todo.Name
                    };
                    _todoRepo.TodoLists.AddNew(todolist);
                }
            }
        }
    }
}