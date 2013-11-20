using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// repository for players
	/// </summary>
	public class PlayerRepository : RepositoryT<Players>
	{
		/// <summary>
		/// Gets the player by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Players GetPlayerById(int id)
		{
			return Entities.Players.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Gets the full name of the player.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public string GetPlayerFullName(int id)
		{
			var player = GetPlayerById(id);
			return player.LastName + " " + player.Name;
		}

	}
}
