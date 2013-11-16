using System.Data.Objects;

namespace FootballSchool.Kerenl
{
	/// <summary>
	/// query provider
	/// </summary>
	public class QueryProvider
	{
		private readonly fscEntities _entities;

		public QueryProvider()
		{
			_entities = new fscEntities();
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
