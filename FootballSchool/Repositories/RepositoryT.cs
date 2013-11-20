using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FootballSchool.Repositories
{
    /// <summary>
    /// gen repo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryT<T> : RepositoryBase where T : class
    {
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Entities.CreateObjectSet<T>();
 
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsNoTracking().ToList();
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Entities.CreateObjectSet<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsNoTracking().Where(where).ToList();
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Entities.CreateObjectSet<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsNoTracking().FirstOrDefault(@where);
        }
    }
}

