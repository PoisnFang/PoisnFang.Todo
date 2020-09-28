using Microsoft.AspNetCore.Identity;
using Oqtane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PoisnFang.Todo.Entities
{
    public class TodoUser : Entity
    {
        public int AppUserId { get; set; }
        [NotMapped]
        public User AppUser { get; set; }

        public string AspNetUserId { get; set; }
        [NotMapped]
        public IdentityUser AspNetUser { get; set; }
        public string Username { get; set; }
        public string NormalizedUserName { get; set; }
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }

        public ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();
    }
}