using Oqtane.Modules;
using Oqtane.Shared;
using PoisnFang.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoisnFang.Todo
{
    partial class TodoLists : TodoBase
    {
        public override string Actions { get { return $"{TodoListsPage}"; } }

        public override string UrlParametersTemplate
        {
            get
            {
                return $"{AddTodoList}," + //Add Site Todo List
                    $"{{{todoListId}}}," + //Edit Todo List
                    $"{{{todoListId}}}/{AddTodoTask}," + //Add Todo Task
                    $"{{{todoListId}}}/{{{todoTaskId}}}"; //Edit Todo Task
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (IsCurrentPageAction && IsNotCurrentUrlTemplate)
            {
                _moduleData.UrlParameterTemplate = UrlParameterState.CurrentTemplate;
                await SetDataAsync().ConfigureAwait(false);
            }
        }

        private async Task SetDataAsync()
        {
            try
            {
                _moduleData.TodoLists = await TodoApi.TodoLists.GetAllByRouteAsync($"{sites}/{PageState.Site.SiteId}");
                foreach (var list in _moduleData.TodoLists)
                {
                    list.TodoTasks = await TodoApi.TodoTasks.GetAllByRouteAsync($"{todolists}/{list.Id}");
                }
                _moduleData.TodoList = new TodoList();
                var foundTodoList = await CheckSetTodoList();

                if (UrlParameterState.Paramerters.ContainsKey(parameter0))
                {
                    switch (UrlParameterState.Paramerters[parameter0])
                    {
                        case AddTodoList:
                            _moduleData.TodoList.Id = -1;
                            _moduleData.ModuleAction = AddTodoList;
                            break;

                        case AddTodoTask:
                            _moduleData.TodoTask = new TodoTask { Id = -1 };
                            _moduleData.ModuleAction = AddTodoTask;
                            break;

                        default:
                            break;
                    }
                }
                else if (await CheckSetTodoTask())
                {
                    _moduleData.ModuleAction = AddTodoTask;
                }
                else if (foundTodoList)   //Edit Todo List
                {
                    _moduleData.ModuleAction = EditTodoList;
                }
                else
                {
                    if (await IsNotNullError(todolists)) _moduleData.ModuleAction = ManageTodoLists;
                }
                OnCustomModuleStateChange?.Invoke(_moduleData);
            }
            catch (Exception ex)
            {
                await logger.LogError(ex, "Error Loading Todo Lists {Error}", ex.Message);
                ModuleInstance.AddModuleMessage($"Error Loading Todo Lists {ex}", MessageType.Error);
            }
        }

        protected async Task<bool> CheckSetTodoList()
        {
            var notNull = false;
            if (UrlParameterState.Paramerters.ContainsKey($"{todoListId}"))
            {
                TodoListID = int.Parse(UrlParameterState.Paramerters[$"{todoListId}"]);
                _moduleData.TodoList = await TodoApi.TodoLists.GetByIdAsync(TodoListID);
                if (await IsNotNullError(_moduleData.TodoList))
                {
                    _moduleData.TodoList.TodoTasks = await TodoApi.TodoTasks.GetAllByRouteAsync($"{todolists}/{_moduleData.TodoList.Id}");

                    if (await IsNotNullError(_moduleData.TodoList.TodoTasks))
                    {
                    }
                    notNull = true;
                }
            }
            return notNull;
        }

        protected async Task<bool> CheckSetTodoTask()
        {
            var notNull = false;
            if (UrlParameterState.Paramerters.ContainsKey($"{todoTaskId}") && await CheckSetTodoList())
            {
                TodoTaskID = int.Parse(UrlParameterState.Paramerters[$"{todoTaskId}"]);
                _moduleData.TodoTask = await TodoApi.TodoTasks.GetByIdAsync(TodoTaskID);

                if (await IsNotNullError(_moduleData.TodoTask))
                {
                    notNull = true;
                }
            }
            return notNull;
        }
    }
}