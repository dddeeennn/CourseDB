namespace FootballSchool.Models
{
	/// <summary>
	/// model for display game
	/// </summary>
	public class GameModel
	{
		public GameModel(int id, string team1, string team2, string referee, string stadium, bool type)
		{
			Team1 = team1;
			Team2 = team2;
			Referee = referee;
			Id = id;
			Stadium = stadium;
		    Type = type;
		}

		public int Id { get; set; }

		public string Team1 { get; set; }

		public string Team2 { get; set; }

		public string Referee { get; set; }

		public string Stadium { get; set; }

	    public bool Type { get; set; }
	}
}
