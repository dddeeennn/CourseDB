using FootballSchool.Kernel;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// base class for repositories
	/// </summary>
	public abstract class RepositoryBase
	{
		protected fscEntities Entities { get; set; }

		protected RepositoryBase()
		{
		    Entities = EntityProvider.Entities;
		}
	}
}
