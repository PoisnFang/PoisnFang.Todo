using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using PoisnFang.Todo.Entities;
using System;
using System.Linq;
using Oqtane.Shared;
using Oqtane.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PoisnFang.Todo.Repository
{
    // change to DbContext for ef core migration
    public class TodoContext : DBContextBase, IService
    {
        private readonly ITenantResolver _tenantResolver;
        private readonly IHttpContextAccessor _accessor;
        private readonly SiteState _siteState;

        public virtual DbSet<TodoList> TodoLists { get; set; }
        public virtual DbSet<TodoTask> TodoTasks { get; set; }
        public virtual DbSet<TodoUser> TodoUsers { get; set; }
        public virtual DbSet<Step> Steps { get; set; }

        // used for ef core migration
        private readonly string _connect = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Oqtane.mdf;Initial Catalog=Oqtane;Integrated Security=SSPI;";

        public Tenant InitializeTenant { get; set; }

        // comment out for ef core migration
        public TodoContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            _tenantResolver = tenantResolver;
            _accessor = accessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Uncomment to do ef core migration
            //InitializeTenant = new Tenant { DBConnectionString = _connect };

            if (InitializeTenant != null)
            {
                optionsBuilder.UseSqlServer(InitializeTenant.DBConnectionString.Replace("|DataDirectory|",
                    AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString()));
            }
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            int userId = 1;
            if (_accessor.HttpContext != null)
            {
                var tenantDb = new TenantDBContext(_tenantResolver, _accessor);

                var todoUsers = ChangeTracker.Entries()
                        .Where(x => x.State == EntityState.Added && x.Entity is TodoUser).Any();
                if (!todoUsers)
                {
                    var currentAppUser = tenantDb.User.AsNoTracking().FirstOrDefault(i => i.Username == _accessor.HttpContext.User.Identity.Name);

                    var currentTodoUser = TodoUsers.AsNoTracking().FirstOrDefault(i => i.AppUserId == currentAppUser.SiteId);
                    if (currentTodoUser != null)
                    {
                        userId = currentTodoUser.Id;
                    }
                    else
                    {
                        userId = 1;
                    }
                    userId = currentTodoUser != null ? currentTodoUser.Id : 1;
                }
            }
            DateTime date = DateTime.UtcNow;

            var modified = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);

            foreach (var item in modified)
            {
                if (item.Entity is Entity entity)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedById = userId;
                        entity.CreatedOn = date;
                    }
                    else
                    {
                        if (entity.IsDeleted && !item.GetDatabaseValues().GetValue<bool>(nameof(Entity.IsDeleted)))
                        {
                            entity.DeletedById = userId;
                            entity.DeletedOn = date;
                        }
                        else if (!entity.IsDeleted && item.GetDatabaseValues().GetValue<bool>(nameof(Entity.IsDeleted)))
                        {
                            entity.DeletedById = null;
                            entity.DeletedOn = null;
                        }
                    }
                    entity.ModifiedById = userId;
                    entity.ModifiedOn = date;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<TodoList>(b =>
            {
                b.ToTable("z_" + nameof(TodoLists));
                b.HasKey(c => c.Id);
                b.HasMany(c => c.TodoTasks)
                    .WithOne(c => c.TodoList)
                    .HasForeignKey(s => s.TodoListId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasQueryFilter(i => !i.IsDeleted);
            });

            mb.Entity<TodoTask>(b =>
            {
                b.ToTable("z_" + nameof(TodoTasks));
                b.HasKey(c => c.Id);
                b.HasMany(c => c.Steps)
                    .WithOne(c => c.TodoTask)
                    .HasForeignKey(s => s.TodoTaskId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasQueryFilter(i => !i.IsDeleted);
            });

            mb.Entity<TodoUser>(b =>
            {
                b.ToTable("z_" + nameof(TodoUsers));
                b.HasKey(c => c.Id);
                b.HasMany(c => c.TodoLists)
                    .WithOne(c => c.TodoUser)
                    .HasForeignKey(s => s.TodoUserId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasQueryFilter(i => !i.IsDeleted);
            });

            mb.Entity<Step>(b =>
            {
                b.ToTable("z_" + nameof(Steps));
                b.HasKey(c => c.Id);
                b.HasQueryFilter(i => !i.IsDeleted);
            });
        }
    }
}