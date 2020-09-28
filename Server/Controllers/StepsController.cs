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
    public class StepsController : TodoControllerBase
    {
        public StepsController(ITodoRepoApi todoRepo, ILogManager logger) : base(todoRepo, logger)
        {
        }

        // GET: api/<controller>/sites/1
        [HttpGet("todotasks/{todotaskid}")]
        [Authorize(Policy = "ViewModule")]
        public IEnumerable<Step> GetAllByTodoTask(int todotaskid)
        {
            return _todoRepo.Steps.GetAllByExpression(i => i.TodoTaskId == todotaskid);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ViewModule")]
        public Step GetById(int id)
        {
            Step Todo = _todoRepo.Steps.GetById(id);
            return Todo;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = "EditModule")]
        public Step Post([FromBody] Step Todo)
        {
            if (ModelState.IsValid)
            {
                Todo = _todoRepo.Steps.AddNew(Todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Todo Added {Todo}", Todo);
            }
            return Todo;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "EditModule")]
        public Step Put(int id, [FromBody] Step Todo)
        {
            if (ModelState.IsValid)
            {
                Todo = _todoRepo.Steps.Update(Todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Todo Updated {Todo}", Todo);
            }
            return Todo;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EditModule")]
        public bool Delete(int id)
        {
            Step Todo = _todoRepo.Steps.GetById(id);
            if (Todo != null)
            {
                _todoRepo.Steps.Delete(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Todo Deleted {TodoId}", id);
                return true;
            }
            return false;
        }
    }
}