using Oqtane.Models;
using Oqtane.Modules;

namespace PoisnFang.Todo
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Todo",
            Description = "Todo",
            Version = "1.0.0",
            ServerManagerType = "PoisnFang.Todo.Manager.TodoManager, PoisnFang.Todo.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "PoisnFang.Todo.Shared.Oqtane"
        };
    }
}