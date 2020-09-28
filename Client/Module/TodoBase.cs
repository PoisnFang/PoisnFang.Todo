using Microsoft.AspNetCore.Components;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Oqtane.UI;
using PoisnFang.Todo.Entities;
using PoisnFang.Todo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisnFang.Todo
{
    [OqtaneIgnore]
    public abstract class TodoBaseIgnore : TodoBase { }

    public abstract class TodoBase : ModuleBase
    {
        public const string parameter0 = nameof(parameter0);
        public const string parameter1 = nameof(parameter1);

        public const string sites = nameof(sites);
        public const string modules = nameof(modules);
        public const string current = nameof(current);

        public const string todolists = nameof(todolists);
        public const string todotasks = nameof(todotasks);
        public const string steps = nameof(steps);

        public const string todoListId = nameof(todoListId);
        public const string todoTaskId = nameof(todoTaskId);

        public const string TodoListsPage = "TodoLists";
        public const string ManageTodoLists = nameof(ManageTodoLists);
        public const string AddTodoList = nameof(AddTodoList);
        public const string EditTodoList = nameof(EditTodoList);
        public const string AddTodoTask = nameof(AddTodoTask);
        public const string EditTodoTask = nameof(EditTodoTask);
        public const string AddStep = nameof(AddStep);

        public virtual int TodoListID { get; set; }
        public virtual int TodoTaskID { get; set; }
        public virtual int StepID { get; set; }

        public override List<Resource> Resources => new List<Resource>()
        {
            new Resource { ResourceType = ResourceType.Stylesheet, Url = "Modules/PoisnFang.Todo/Module.css" }
        };

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected ISettingService SettingService { get; set; }

        [Inject]
        protected ITodoRepoServiceApi TodoApi { get; set; }

        public override SecurityAccessLevel SecurityAccessLevel { get { return SecurityAccessLevel.View; } }
        public override bool UseAdminContainer => false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Resources != null && Resources.Exists(item => item.ResourceType == ResourceType.Stylesheet))
                {
                    var links = new List<object>();
                    foreach (Resource resource in Resources.Where(item => item.ResourceType == ResourceType.Stylesheet))
                    {
                        links.Add(new { rel = "stylesheet", href = resource.Url, type = "text/css", integrity = resource.Integrity ?? "", crossorigin = resource.CrossOrigin ?? "", key = "" });
                    }
                    var interop = new Interop(JSRuntime);
                    await interop.IncludeLinks(links.ToArray());
                }
            }
        }

        protected ModuleData _moduleData = new ModuleData();

        [CascadingParameter]
        protected ModuleData ModuleData { get; set; }

        public Action<ModuleData> OnCustomModuleStateChange { get { return ChangeModuleState; } set { } }

        private void ChangeModuleState(ModuleData moduleData)
        {
            _moduleData = moduleData;
            StateHasChanged();
        }

        public virtual string UrlParametersTemplate => string.Empty;

        public UrlParameters UrlParameterState
        {
            get
            {
                var urlParameterState = new UrlParameters
                {
                    Templates = UrlParametersTemplate.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    Paramerters = new Dictionary<string, string>()
                };

                foreach (var template in urlParameterState.Templates)
                {
                    urlParameterState.Paramerters = GetUrlParameters(template);

                    if (urlParameterState.Paramerters.Count > 0)
                    {
                        urlParameterState.CurrentTemplate = template;
                        break;
                    }
                }

                return urlParameterState;
            }
        }

        protected virtual async Task<bool> IsNotNullError(object obj)
        {
            if (obj == null)
            {
                await logger.LogError($"Error Loading Object {obj}");
                ModuleInstance.AddModuleMessage($"Error Loading Object {obj}", MessageType.Error);
                return false;
            }
            return true;
        }

        public bool IsCurrentPageAction
        {
            get
            {
                return AskedForThisPageAction();
            }
        }

        public bool IsCurrentUrlTemplate
        {
            get
            {
                return _moduleData?.UrlParameterTemplate == UrlParameterState.CurrentTemplate;
            }
        }

        public bool IsNotCurrentUrlTemplate
        {
            get
            {
                return _moduleData?.UrlParameterTemplate != UrlParameterState.CurrentTemplate;
            }
        }

        protected virtual bool AskedForThisPageAction()
        {
            if (Actions.Split(',').ToList().Contains(PageState.Action)) return true;
            else if (Actions == PageState.Action) return true;
            return false;
        }

        protected async Task RefreshDMSUserAsync()
        {
            if (_moduleData.CurrentTodoUser != null)
            {
                return;
            }
            TodoUser dmsUser = null;

            if (PageState.User != null)
            {
                dmsUser = await TodoApi.TodoUsers.GetByRouteAsync(current);
            }

            _moduleData.CurrentTodoUser = dmsUser;
        }
    }
}