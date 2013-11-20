using System.Collections.Generic;
using System.Linq;

namespace FootballSchool.Repositories
{

	/// <summary>
	/// repo for teams
	/// </summary>
	public class TeamRepository : RepositoryT<Teams>
	{
		/// <summary>
		/// Gets the bu player.
		/// </summary>
		/// <param name="playerId">The player id.</param>
		/// <returns></returns>
		public Teams GetByPlayer(int playerId)
		{
			var pitRepo = new PlayerInTeamRepository();

			var teamId = pitRepo.GetByPlayerId(playerId).FirstOrDefault().TeamID;

			return GetSingle(x => x.Id == teamId);
		}
	}
}
