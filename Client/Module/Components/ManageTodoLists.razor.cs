using Oqtane.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoisnFang.Todo.Components
{
    partial class ManageTodoLists : TodoBaseIgnore
    {
        private async Task Delete(int formId)
        {
            try
            {
                await TodoApi.TodoLists.DeleteAsync(formId);
                await logger.LogInformation("Form Deleted {Id}", formId);
                NavigationManager.NavigateTo(EditUrl(TodoListsPage));
            }
            catch (Exception ex)
            {
                await logger.LogError(ex, "Error Deleting Form {Id} {Error}", formId, ex.Message);
                ModuleInstance.AddModuleMessage("Error Deleting Form", MessageType.Error);
            }
        }
    }
}