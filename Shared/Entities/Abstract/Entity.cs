using Oqtane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PoisnFang.Todo.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }

        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }

        public int? DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        public TodoUser CreatedByDMSUser { get; set; }
        [NotMapped]
        public TodoUser ModifiedByDMSUser { get; set; }
        [NotMapped]
        public TodoUser DeletedByDMSUser { get; set; }
    }
}