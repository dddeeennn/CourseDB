using System.Collections.Generic;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
	/// <summary>
	/// model for display team
	/// </summary>
	public class TeamVM
	{
		private CoachRepository _coachRepository;

		public TeamVM()
		{
			Initialize();
		}

		public TeamVM(int id, string name, string city, int coachId)
			: this()
		{
			Id = id;
			Name = name;
			City = city;
			CoachId = coachId;
		}

		public TeamVM(Team team) : this(team.Id, team.Name, team.City, team.CoachID)
		{
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string City { get; set; }

		public int CoachId { get; set; }

		public Dictionary<int, string> Coaches { get; set; }

		private void Initialize()
		{
			_coachRepository = new CoachRepository();
			Coaches = new Dictionary<int, string>();
			foreach (var coach in _coachRepository.GetAll())
			{
				Coaches.Add(coach.Id, _coachRepository.GetFullName(coach.Id));
			}
		}
	}
}
