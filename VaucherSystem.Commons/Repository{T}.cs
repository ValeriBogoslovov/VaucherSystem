namespace VaucherSystem.Commons
{
    using Contracts;
    using Data;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private VaucherSystemDbContext context;
        public Repository(VaucherSystemDbContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return this.context.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return this.context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>();
        }

        public void Remove(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate)
        {
            return this.context.Set<T>().Select(predicate);
        }
    }
}
