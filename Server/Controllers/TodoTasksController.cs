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
    public class TodoTasksController : TodoControllerBase
    {
        public TodoTasksController(ITodoRepoApi todoRepo, ILogManager logger) : base(todoRepo, logger)
        {
        }

        // GET: api/<controller>/sites/1
        [HttpGet("todolists/{todolistid}")]
        [Authorize(Policy = "ViewModule")]
        public IEnumerable<TodoTask> GetAllByTodoList(int todolistid)
        {
            return _todoRepo.TodoTasks.GetAllByExpression(i => i.TodoListId == todolistid);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ViewModule")]
        public TodoTask GetById(int id)
        {
            TodoTask Todo = _todoRepo.TodoTasks.GetById(id);
            return Todo;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = "EditModule")]
        public TodoTask Post([FromBody] TodoTask Todo)
        {
            if (ModelState.IsValid)
            {
                Todo = _todoRepo.TodoTasks.AddNew(Todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Todo Added {Todo}", Todo);
            }
            return Todo;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "EditModule")]
        public TodoTask Put(int id, [FromBody] TodoTask Todo)
        {
            if (ModelState.IsValid)
            {
                Todo = _todoRepo.TodoTasks.Update(Todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Todo Updated {Todo}", Todo);
            }
            return Todo;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EditModule")]
        public bool Delete(int id)
        {
            TodoTask Todo = _todoRepo.TodoTasks.GetById(id);
            if (Todo != null)
            {
                _todoRepo.TodoTasks.Delete(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Todo Deleted {TodoId}", id);
                return true;
            }
            return false;
        }
    }
}