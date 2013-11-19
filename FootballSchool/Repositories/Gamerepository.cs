using System.Linq;

namespace FootballSchool.Repositories
{
    /// <summary>
    /// repo for games
    /// </summary>
    public class GameRepository : RepositoryT<Game>
    {
        private readonly TeamRepository _teamRepository = new TeamRepository();
        private readonly GameEventRepository _gameEventRepository = new GameEventRepository();
        /// <summary>
        /// Gets the game by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Game GetGameById(int id)
        {
            return Entities.Games.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the name of the game.
        /// </summary>
        /// <param name="teamId1">The team id1.</param>
        /// <param name="teamId2">The team id2.</param>
        /// <returns></returns>
        public string GetGameName(int teamId1, int teamId2)
        {
            var team1 = _teamRepository.GetSingle(x => x.Id == teamId1);
            var team2 = _teamRepository.GetSingle(x => x.Id == teamId2);

            return team1.Name + " - " + team2.Name;
        }

        /// <summary>
        /// Get name of the game.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <returns></returns>
        public string GetGameName(int gameId)
        {
            var game = GetGameById(gameId);
            return GetGameName(game.Team1ID, game.Team2ID);
        }

        public void RemoveById(int gameId)
        {
            var game = GetGameById(gameId);
            Entities.Games.DeleteObject(game);
            Entities.SaveChanges();

            _gameEventRepository.RemoveByGameId(gameId);
            Entities.SaveChanges();
        }
    }
}
