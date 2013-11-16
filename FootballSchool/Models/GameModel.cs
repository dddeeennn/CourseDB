namespace FootballSchool.Models
{
	/// <summary>
	/// model for display game
	/// </summary>
	public class GameModel
	{
		public GameModel(int id, string team1, string team2, string referee, string stadium)
		{
			Team1 = team1;
			Team2 = team2;
			Referee = referee;
			Id = id;
			Stadium = stadium;
		}

		public int Id { get; set; }

		public string Team1 { get; set; }

		public string Team2 { get; set; }

		public string Referee { get; set; }

		public string Stadium { get; set; }
	}
}
