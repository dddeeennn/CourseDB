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
        public EventVM()
        {
            Initialize();
        }

        private void Initialize()
        {
            Time = DateTime.Today;
            InitEvents();
            InitGames();
            InitPLayers();
        }

        private void InitPLayers()
        {
            var playerRepo = new PlayerRepository();
            Players = new Dictionary<int, string>();
            foreach (var player in playerRepo.GetAll())
            {
                Players.Add(player.Id, playerRepo.GetPlayerFullName(player.Id));
            }
        }

        private void InitGames()
        {
            var gameRepo = new GameRepository();
            Games = new Dictionary<int, string>();
            foreach (var game in gameRepo.GetAll())
            {
                Games.Add(game.Id, gameRepo.GetGameName(game.Id));
            }
        }

        private void InitEvents()
        {
            var eventRepo = new EventRepository();
            Events = new Dictionary<int, string>();
            foreach (var e in eventRepo.GetAll())
            {
                Events.Add(e.Id, eventRepo.GetEventType(e.Id));
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

        public EventVM(GameEvent ge)
            : this(ge.Id, ge.EventID, ge.GameID, ge.PlayerID, ge.Time)
        {
            SelectedEventId = Events.Keys.ToList().IndexOf(EventId);
            SelectedGameId = Games.Keys.ToList().IndexOf(GameId);
            SelectedPlayerId = Players.Keys.ToList().IndexOf(PlayerId);
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
