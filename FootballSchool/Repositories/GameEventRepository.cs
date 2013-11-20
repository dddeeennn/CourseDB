using System.Collections.Generic;
using System.Linq;

namespace FootballSchool.Repositories
{
	public class GameEventRepository : RepositoryT<GameEvents>
	{
		public void RemoveByGameId(int gameId)
		{
			var toDeleteList = GetAll(x => x.GameID == gameId);
			foreach (var gameEvent in toDeleteList)
			{
				Entities.GameEvents.DeleteObject(gameEvent);
			}
			Entities.SaveChanges();
		}

		/// <summary>
		/// Get events for game.
		/// </summary>
		/// <param name="gameId">The game id.</param>
		/// <returns></returns>
		public List<GameEvents> GetEventsForGame(int gameId)
		{
			return GetAll().Where(ge => ge.GameID == gameId).ToList();
		}
		public void Remove(int gameEventId)
		{
			Entities.GameEvents.DeleteObject(GetSingle(x => x.Id == gameEventId));
			Entities.SaveChanges();
		}
	}
}
