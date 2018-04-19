namespace VaucherSystem.Commons.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
        where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        T Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindMany(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate);

    }
}
