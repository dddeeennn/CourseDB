using System;
using System.Collections.Generic;
using System.Linq;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
	/// <summary>
	/// event player game viewModel
	/// </summary>
	public class EventVM
	{
        private PlayerRepository _playerRepository { get; set; }
        private GameRepository _gameRepository { get; set; }
	    private EventRepository _eventRepository { get; set; }

		public EventVM()
		{
			Initialize();
		}

		private void Initialize()
		{
            _playerRepository = new PlayerRepository();
            _eventRepository = new EventRepository();
            _gameRepository = new GameRepository();
			Time = DateTime.Today;
			InitEvents();
			InitGames();
			InitPLayers();
		}

		private void InitPLayers()
		{
			Players = new Dictionary<int, string>();
			foreach (var player in _playerRepository.GetAll())
			{
				Players.Add(player.Id, _playerRepository.GetPlayerFullName(player.Id));
			}
		}

		private void InitGames()
		{
			Games = new Dictionary<int, string>();
			foreach (var game in _gameRepository.GetAll())
			{
				Games.Add(game.Id, _gameRepository.GetGameName(game.Id));
			}
		}

		private void InitEvents()
		{
			Events = new Dictionary<int, string>();
			foreach (var e in _eventRepository.GetAll())
			{
				Events.Add(e.Id, _eventRepository.GetEventType(e.Id));
			}
		}

		public EventVM(int id, int eventId, int gameId, int playerId, DateTime time)
			: this()
		{
			Id = id;
			EventId = eventId;
			GameId = gameId;
			PlayerId = playerId;
			Time = time;
		}

		public EventVM(GameEvents ge)
			: this(ge.Id, ge.EventID, ge.GameID, ge.PlayerID, ge.Time)
		{
			SelectedEventId = Events.Keys.ToList().IndexOf(EventId);
			SelectedGameId = Games.Keys.ToList().IndexOf(GameId);
			SelectedPlayerId = Players.Keys.ToList().IndexOf(PlayerId);
		}

        public void UpdatePlayer(int team1Id, int team2Id)
        {
            Players = new Dictionary<int, string>();
            foreach (var player in _playerRepository.GetPlayersForTeam(team1Id))
            {
                Players.Add(player.Id, _playerRepository.GetPlayerFullName(player.Id));
            }

            foreach (var player in _playerRepository.GetPlayersForTeam(team2Id))
            {
                Players.Add(player.Id, _playerRepository.GetPlayerFullName(player.Id));
            }
        }

		public int Id { get; set; }

		public int EventId { get; set; }

		public int GameId { get; set; }

		public int PlayerId { get; set; }

		public DateTime Time { get; set; }

		public int SelectedPlayerId { get; set; }

		public int SelectedEventId { get; set; }

		public int SelectedGameId { get; set; }

		public Dictionary<int, string> Players { get; set; }

		public Dictionary<int, string> Games { get; set; }

		public Dictionary<int, string> Events { get; set; }
	}
}
