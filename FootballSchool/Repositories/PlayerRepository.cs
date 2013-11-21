using System.Collections.Generic;
using System.Linq;

namespace FootballSchool.Repositories
{
    /// <summary>
    /// repository for players
    /// </summary>
    public class PlayerRepository : RepositoryT<Players>
    {
        public PlayerRepository()
        {
            _playerInTeamRepository = new PlayerInTeamRepository();
        }

        private PlayerInTeamRepository _playerInTeamRepository { get; set; }

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
        /// Get full name of the player.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public string GetPlayerFullName(int id)
        {
            var player = GetPlayerById(id);
            return player.LastName + " " + player.Name;
        }

        public List<Players> GetPlayersForTeam(int teamId)
        {
           return _playerInTeamRepository.GetByTeamId(teamId)
               .Select(pit => GetSingle(x => x.Id == pit.PlayerID)).ToList();
        }

    }
}
