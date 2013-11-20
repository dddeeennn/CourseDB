using System.Collections.Generic;
using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// player in team repository
	/// </summary>
	public class PlayerInTeamRepository : RepositoryT<PlayerInTeam>
	{
		public IEnumerable<PlayerInTeam> GetByPlayerId(int playerId)
		{
			return Entities.PlayerInTeam.Where(x => x.PlayerID == playerId).ToList();
		}

		public IEnumerable<PlayerInTeam> GetByTeamId(int teamId)
		{
			return Entities.PlayerInTeam.Where(x => x.TeamID == teamId).ToList();
		}

		public void RemoveByPlayerId(int playerId)
		{
			foreach (var item in GetByPlayerId(playerId))
			{
				Entities.PlayerInTeam.DeleteObject(item);
			}
			Entities.SaveChanges();
		}

		public void RemoveByTeamId(int teamId)
		{
			foreach (var item in GetByTeamId(teamId))
			{
				Entities.PlayerInTeam.DeleteObject(item);
			}
			Entities.SaveChanges();
		}
	}
}
