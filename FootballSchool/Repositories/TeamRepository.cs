using System.Linq;

namespace FootballSchool.Repositories
{

	/// <summary>
	/// repo for teams
	/// </summary>
	public class TeamRepository : RepositoryBase
	{
		/// <summary>
		/// Gets the team by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Teams GetTeamById(int id)
		{
			return Entities.Teams.FirstOrDefault(x => x.Id == id);
		}
	}
}
