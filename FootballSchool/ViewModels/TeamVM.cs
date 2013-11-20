using System.Collections.Generic;
using System.Linq;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
    /// <summary>
    /// model for display team
    /// </summary>
    public class TeamVM
    {
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
            SelectedCoachId = Coaches.Keys.ToList().IndexOf(CoachId);
        }

        public TeamVM(Teams team)
            : this(team.Id, team.Name, team.City, team.CoachID)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int CoachId { get; set; }

        public int SelectedCoachId { get; set; }

        public Dictionary<int, string> Coaches { get; set; }

        private void Initialize()
        {
            var coachRepository = new CoachRepository();
            Coaches = new Dictionary<int, string>();
            foreach (var coach in coachRepository.GetAll())
            {
                Coaches.Add(coach.Id, coachRepository.GetFullName(coach.Id));
            }
        }
    }
}
