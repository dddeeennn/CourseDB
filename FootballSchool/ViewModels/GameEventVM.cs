using System.Collections.Generic;
using System.Linq;
using FootballSchool.Models;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
	/// <summary>
	/// GameEventViewModel
	/// </summary>
	public class GameEventVM
	{
		private readonly PlayerRepository _playerRepository;
		private readonly GameRepository _gameRepository;
		private readonly EventRepository _eventRepository;

		public GameEventVM()
		{
			Models = new List<GameEventPlayerModel>();
			_eventRepository = new EventRepository();
			_gameRepository = new GameRepository();
			_playerRepository = new PlayerRepository();
		}

		public GameEventVM(IEnumerable<GameEvent> gameEvents)
			: this()
		{
			foreach (var model in gameEvents.Select(ge => GetModel(ge)))
			{
				Models.Add(model);
			}
		}

		public List<GameEventPlayerModel> Models { get; set; }

		private GameEventPlayerModel GetModel(GameEvent ge)
		{
			var gameName = _gameRepository.GetGameName(ge.GameID);
			var playerName = _playerRepository.GetPlayerFullName(ge.PlayerID);
			var eventType = _eventRepository.GetEvenType(ge.EventID);

			return new GameEventPlayerModel(gameName, playerName, eventType, ge.Id);
		}
	}
}
