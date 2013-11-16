using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
			List<T> list;
			using (Entities)
			{
				IQueryable<T> dbQuery = Entities.CreateObjectSet<T>();
				//Apply eager loading
				foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
					dbQuery = dbQuery.Include(navigationProperty);

				list = dbQuery
					.AsNoTracking()
					.ToList();
			}
			return list;
		}

		public virtual IList<T> GetList(Func<T, bool> where,
			 params Expression<Func<T, object>>[] navigationProperties)
		{
			List<T> list;
			using (Entities)
			{
				IQueryable<T> dbQuery = Entities.CreateObjectSet<T>();

				//Apply eager loading
				foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
					dbQuery = dbQuery.Include<T, object>(navigationProperty);

				list = dbQuery
					.AsNoTracking()
					.Where(where)
					.ToList<T>();
			}
			return list;
		}

		public virtual T GetSingle(Func<T, bool> where,
			 params Expression<Func<T, object>>[] navigationProperties)
		{
			T item = null;
			using (Entities)
			{
				IQueryable<T> dbQuery = Entities.CreateObjectSet<T>();

				//Apply eager loading
				foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
					dbQuery = dbQuery.Include<T, object>(navigationProperty);

				item = dbQuery
					.AsNoTracking() //Don't track any changes for the selected item
					.FirstOrDefault(where); //Apply where clause
			}
			return item;
		}
	}
}

