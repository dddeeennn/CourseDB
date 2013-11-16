using System.Collections.Generic;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
	/// <summary>
	/// player view model
	/// </summary>
	public class PlayerVM
	{
		public PlayerVM(Player player)
			: this(player.Id, player.LastName, player.Name, player.MiddleName,
				player.Passport, player.IsMain, player.PositionID)
		{
			TeamId = new RepositoryT<PlayerInTeam>().GetSingle(x => x.PlayerID == player.Id).TeamID;
		}

		public PlayerVM(int id, string ln, string n, string mn,
			string passport, bool isMain, int posId)
			: this()
		{
			Id = id;
			LastName = ln;
			Name = n;
			MiddleName = mn;
			Passport = passport;
			IsMain = isMain;
			PositionId = posId;
		}

		public PlayerVM()
		{
			Initialize();
		}

		public int Id { get; set; }

		public string LastName { get; set; }

		public string Name { get; set; }

		public string MiddleName { get; set; }

		public string Passport { get; set; }

		public bool IsMain { get; set; }

		public int PositionId { get; set; }

		public int TeamId { get; set; }

		public Dictionary<int, string> Positions { get; set; }

		public Dictionary<int, string> Teams { get; set; }

		private void Initialize()
		{
			InitTeams();
			InitPositions();
		}

		/// <summary>
		/// Inits the positions.
		/// </summary>
		private void InitPositions()
		{
			var positionRepo = new RepositoryT<PlayerPosition>();
			Positions = new Dictionary<int, string>();
			foreach (var position in positionRepo.GetAll())
			{
				Positions.Add(position.Id, position.Position);
			}
		}

		/// <summary>
		/// Inits the teams.
		/// </summary>
		private void InitTeams()
		{
			var teamRepo = new RepositoryT<Team>();
			Teams = new Dictionary<int, string>();
			foreach (var team in teamRepo.GetAll())
			{
				Teams.Add(team.Id, team.Name);
			}
		}
	}
}
