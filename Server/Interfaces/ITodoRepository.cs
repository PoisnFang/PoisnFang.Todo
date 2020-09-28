using System;
using System.Collections.Generic;
using PoisnFang.Todo.Entities;

namespace PoisnFang.Todo.Repository
{
    public interface ITodoRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllByExpression(Func<TEntity, bool> expression);

        TEntity GetByExpression(Func<TEntity, bool> expression);

        TEntity GetById(int id);

        TEntity AddNew(TEntity model);

        TEntity Update(TEntity model);

        bool Delete(int id);

        void SaveDb();
    }
}