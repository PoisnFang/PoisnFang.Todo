using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using PoisnFang.Todo.Entities;
using PoisnFang.Todo.Repository;

namespace PoisnFang.Todo.Controllers
{
    public class TodoListsController : TodoControllerBase
    {
        public TodoListsController(ITodoRepoApi todoRepo, ILogManager logger) : base(todoRepo, logger)
        {
        }

        // GET: api/<controller>/sites/1
        [HttpGet("sites/{siteid}")]
        [Authorize(Policy = "ViewModule")]
        public IEnumerable<TodoList> GetAllBySite(int siteid)
        {
            return _todoRepo.TodoLists.GetAllByExpression(i => i.SiteId == siteid);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ViewModule")]
        public TodoList GetById(int id)
        {
            TodoList Todo = _todoRepo.TodoLists.GetById(id);
            return Todo;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = "EditModule")]
        public TodoList Post([FromBody] TodoList Todo)
        {
            if (ModelState.IsValid)
            {
                Todo = _todoRepo.TodoLists.AddNew(Todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Todo Added {Todo}", Todo);
            }
            return Todo;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "EditModule")]
        public TodoList Put(int id, [FromBody] TodoList Todo)
        {
            if (ModelState.IsValid)
            {
                Todo = _todoRepo.TodoLists.Update(Todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Todo Updated {Todo}", Todo);
            }
            return Todo;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EditModule")]
        public bool Delete(int id)
        {
            TodoList Todo = _todoRepo.TodoLists.GetById(id);
            if (Todo != null)
            {
                _todoRepo.TodoLists.Delete(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Todo Deleted {TodoId}", id);
                return true;
            }
            return false;
        }
    }
}