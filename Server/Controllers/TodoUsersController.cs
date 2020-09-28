using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using PoisnFang.Todo.Entities;
using PoisnFang.Todo.Repository;
using Oqtane.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace PoisnFang.Todo.Controllers
{
    public class TodoUsersController : TodoControllerBase
    {
        private readonly TenantDBContext _tenantDB;

        public TodoUsersController(ITodoRepoApi todoRepo, ILogManager logger, TenantDBContext tenantDB) : base(todoRepo, logger)
        {
            _tenantDB = tenantDB;
        }

        // GET api/<controller>/current
        [HttpGet("current")]
        [Authorize(Roles = Constants.RegisteredRole)]
        public TodoUser GetCurrentTodoUser()
        {
            var currentUser = _tenantDB.User.AsNoTracking().FirstOrDefault(f => f.Username == User.Identity.Name);

            var currentTodoUser = _todoRepo.TodoUsers.GetByExpression(i => i.AppUserId == currentUser.UserId);

            if (currentTodoUser == null)
            {
                currentTodoUser = CreateNewTodoUser(User);
            }
            return currentTodoUser;
        }

        private TodoUser CreateNewTodoUser(ClaimsPrincipal user)
        {
            var currentUser = _tenantDB.User.AsNoTracking().FirstOrDefault(f => f.Username == user.Identity.Name);
            var identityuser = _tenantDB.Users.AsNoTracking().FirstOrDefault(f => f.UserName == user.Identity.Name);

            var newTodoUser = new TodoUser
            {
                AppUserId = currentUser.UserId,
                AspNetUserId = identityuser.Id,
                Email = identityuser.Email,
                NormalizedEmail = identityuser.NormalizedEmail,
                Username = identityuser.UserName,
                NormalizedUserName = identityuser.NormalizedUserName,
                DisplayName = identityuser.UserName
            };

            newTodoUser = _todoRepo.TodoUsers.AddNew(newTodoUser);
            return newTodoUser;
        }

        // GET: api/<controller>/appusers/1
        [HttpGet("appusers/{appuserid}")]
        [Authorize(Roles = Constants.RegisteredRole)]
        public IEnumerable<TodoUser> GetAllByAppUser(int appuserid)
        {
            return _todoRepo.TodoUsers.GetAllByExpression(i => i.AppUserId == appuserid);
        }

        // GET api/<controller>/dashboards/5/forms/3
        [HttpGet("appusers/{appuserid}/aspnetusers/{aspnetuserid}")]
        [Authorize(Roles = Constants.RegisteredRole)]
        public TodoUser Get(int appuserid, string aspnetuserid)
        {
            return _todoRepo.TodoUsers.GetByExpression(i => i.AppUserId == appuserid && i.AspNetUserId == aspnetuserid);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Constants.RegisteredRole)]
        public TodoUser Get(int id)
        {
            return _todoRepo.TodoUsers.GetById(id);
        }

        // GET api/<controller>/email@email.com
        [HttpGet("emails/{email}")]
        [Authorize(Roles = Constants.RegisteredRole)]
        public TodoUser GetByEmail(string email)
        {
            return _todoRepo.TodoUsers.GetByExpression(i => i.Email == email);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Constants.AdminRole)]
        public TodoUser Post([FromBody] TodoUser TodoUser)
        {
            if (ModelState.IsValid)
            {
                TodoUser = _todoRepo.TodoUsers.AddNew(TodoUser);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "TodoUser Added {TodoUser}", TodoUser);
            }
            return TodoUser;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.AdminRole)]
        public TodoUser Put(int id, [FromBody] TodoUser TodoUser)
        {
            if (ModelState.IsValid)
            {
                TodoUser = _todoRepo.TodoUsers.Update(TodoUser);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "TodoUser Updated {TodoUser}", TodoUser);
            }
            return TodoUser;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.AdminRole)]
        public void Delete(int id)
        {
            _todoRepo.TodoUsers.Delete(id);
            _logger.Log(LogLevel.Information, this, LogFunction.Delete, "TodoUser Deleted {TodoUserId}", id);
        }
    }
}