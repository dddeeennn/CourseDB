using System;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace FootballSchool.Kernel
{
	/// <summary>
	/// query provider
	/// </summary>
	public class QueryProvider
	{
		private readonly fscEntities _entities;

		public QueryProvider()
		{
		    _entities = EntityProvider.Entities;
		}

		/// <summary>
		/// Get query.
		/// </summary>
		/// <returns></returns>
		public ObjectQuery<TEntity> GetQuery<TEntity>() where TEntity : class
		{
			return _entities.CreateObjectSet<TEntity>();
		}
	}
}
