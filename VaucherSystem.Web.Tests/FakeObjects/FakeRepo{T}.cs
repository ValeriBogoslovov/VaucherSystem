namespace VaucherSystem.Web.Tests.FakeObjects
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Commons.Contracts;
    using System.Collections.Generic;

    public class FakeRepo<T> : IRepository<T> where T : class
    {
        private List<T> fakeDb;

        public FakeRepo()
        {
            this.fakeDb = new List<T>();
        }
        public void Add(T entity)
        {
            this.fakeDb.Add(entity);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return this.fakeDb.AsQueryable().FirstOrDefault(predicate);
        }

        public IQueryable<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return this.fakeDb.AsQueryable().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return this.fakeDb.AsQueryable();
        }

        public void Remove(T entity)
        {
            this.fakeDb.Remove(entity);
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate)
        {
            return this.fakeDb.AsQueryable().Select(predicate);
        }
    }
}
