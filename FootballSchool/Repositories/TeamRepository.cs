using System.Collections.Generic;
using System.Linq;

namespace FootballSchool.Repositories
{

	/// <summary>
	/// repo for teams
	/// </summary>
	public class TeamRepository : RepositoryBase
	{
		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Team> GetAll()
		{
			return Entities.Teams;
		}

		/// <summary>
		/// Gets the team by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Team GetTeamById(int id)
		{
			return Entities.Teams.FirstOrDefault(x => x.Id == id);
		}
	}
}
