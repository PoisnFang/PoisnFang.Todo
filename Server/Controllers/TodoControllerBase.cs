using Microsoft.AspNetCore.Mvc;
using Oqtane.Infrastructure;
using PoisnFang.Todo.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisnFang.Todo.Controllers
{
    [ApiController]
    [Route("{site}/api/[controller]")]
    public class TodoControllerBase : Controller
    {
        protected readonly ITodoRepoApi _todoRepo;
        protected readonly ILogManager _logger;

        public TodoControllerBase(ITodoRepoApi todoRepo, ILogManager logger)
        {
            _todoRepo = todoRepo;
            _logger = logger;
        }
    }
}