using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using PoisnFang.Todo.Entities;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Oqtane.Models;
using Oqtane.Repository;
using Oqtane.Shared;
using Microsoft.Extensions.Configuration;

namespace PoisnFang.Todo.Repository
{
    public class TodoRepository<TEntity> : IService, ITodoRepository<TEntity> where TEntity : Entity
    {
        private readonly TodoContext _appDb;
        private readonly IConfigurationRoot _config;

        public TodoRepository(TodoContext appDb, IConfigurationRoot config)
        {
            try
            {
                appDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            catch (Exception)
            {
                appDb.InitializeTenant = new Tenant(); ;

                using (var db = new InstallationContext(NormalizeConnectionString(config.GetConnectionString(SettingKeys.ConnectionStringKey))))
                {
                    var master = db.Tenant.FirstOrDefault(i => i.Name == "Master");
                    appDb.InitializeTenant.DBConnectionString = master.DBConnectionString;
                }

                appDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            _appDb = appDb;
        }

        private string NormalizeConnectionString(string connectionString)
        {
            var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString();
            connectionString = connectionString.Replace("|DataDirectory|", dataDirectory);
            return connectionString;
        }

        private DbSet<TEntity> EntityTable => _appDb.Set<TEntity>();

        private string PrimaryKey => _appDb.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(x => x.Name).FirstOrDefault();

        private Func<TEntity, bool> LinqExpression(object Pk)
        {
            var entity = Expression.Parameter(typeof(TEntity));
            Expression keyValue = Expression.Property(entity, PrimaryKey);
            Expression pkValue = Expression.Constant(Pk, keyValue.Type);
            Expression body = Expression.Equal(keyValue, pkValue);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(body, entity);

            return lambda.Compile();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var all = EntityTable.ToList();
            return all;
        }

        public IEnumerable<TEntity> GetAllByExpression(Func<TEntity, bool> expression)
        {
            var all = EntityTable.Where(expression).ToList();

            return all;
        }

        public TEntity GetByExpression(Func<TEntity, bool> expression)
        {
            TEntity entity = EntityTable.FirstOrDefault(expression);

            return entity;
        }

        public TEntity GetById(int id)
        {
            TEntity entity = EntityTable.FirstOrDefault(LinqExpression(id));

            return entity;
        }

        public TEntity AddNew(TEntity entity)
        {
            EntityTable.Add(entity);
            SaveDb();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _appDb.Update(entity);
            SaveDb();
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = EntityTable.FirstOrDefault(LinqExpression(id));
            if (entity != null)
            {
                EntityTable.Remove(entity);
                SaveDb();
                return true;
            }
            else return false;
        }

        public void SaveDb()
        {
            _appDb.SaveChanges();
        }
    }
}