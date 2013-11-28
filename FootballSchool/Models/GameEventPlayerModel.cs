using System;

namespace FootballSchool.Models
{
	/// <summary>
	/// model for games events and players
	/// </summary>
	public class GameEventPlayerModel
	{
		public GameEventPlayerModel(string game, string player, string et, int id, DateTime time)
		{
			Game = game;
			Player = player;
			Id = id;
			Event = et;
		    Time = time;
		}

		public int Id { get; set; }

		public string Game { get; set; }

		public string Player { get; set; }

		public string Event { get; set; }

        public DateTime Time { get; set; }
	}
}
