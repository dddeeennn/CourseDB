using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSchool.VM
{
    class GameEventVM
    {
        public List<KeyValuePair<int, string>> Games { get; set; }
        private fscEntities entities;
        public List<KeyValuePair<int, string>> Events { get; set; }

        public List<KeyValuePair<int, string>> Players { get; set; }

        public GameEventVM(List<KeyValuePair<int, string>> games, List<KeyValuePair<int, string>> events, List<KeyValuePair<int, string>> players)
        {
            Games = games;
            Events = events;
            Players = players;
        }

        public GameEventVM()
        {
            Games = new List<KeyValuePair<int, string>>();
            Events = new List<KeyValuePair<int, string>>();
            Players = new List<KeyValuePair<int, string>>();
            entities = new fscEntities();
        }

        public GameEventVM(IEnumerable<GameEvents> ge)
            : this()
        {
            foreach (var gameEvent in ge)
            {
                Players.Add(new KeyValuePair<int, string>(gameEvent.PlayerID, GetPlayerName(gameEvent.PlayerID)));
                Events.Add(new KeyValuePair<int, string>(gameEvent.EventID, GetEventType(gameEvent.EventID)));
                Games.Add(new KeyValuePair<int, string>(gameEvent.GameID, GetGameName(gameEvent.GameID)));
            }
        }

        private string GetGameName(int gameId)
        {
            var game = entities.Games.First(x => x.Id == gameId);
            return GetTeamName(game.Team1ID) + " - " + GetTeamName(game.Team2ID);
        }

        private string GetTeamName(int teamId)
        {
            return entities.Teams.First(x => x.Id == teamId).Name;
        }

        private string GetEventType(int eventId)
        {
            return entities.Events.First(x => x.Id == eventId).Type;
        }

        private string GetPlayerName(int playerId)
        {
            var player = entities.Players.First(x => x.Id == playerId);
            return player.LastName + " " + player.Name;
        }

        public GameEventVM(fscEntities entities)
            : this()
        {
            foreach (var e in entities.Events)
            {
                Events.Add(new KeyValuePair<int, string>(e.Id, e.Type));
            }

            foreach (var p in entities.Players)
            {
                Players.Add(new KeyValuePair<int, string>(p.Id, p.LastName + " " + p.Name));
            }
        }
    }
}
