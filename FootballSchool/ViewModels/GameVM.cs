using System.Collections.Generic;
using System.Linq;
using FootballSchool.Models;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
    public class GameVM
    {
        private readonly RefereeRepository _refereeRepository;
        private readonly TeamRepository _teamRepository;

        public GameVM()
        {
            Models = new List<GameModel>();
            _teamRepository = new TeamRepository();
            _refereeRepository = new RefereeRepository();
        }

        public GameVM(IEnumerable<Games> games)
            : this()
        {
            foreach (var model in games.Select(GetModel))
            {
                Models.Add(model);
            }
        }

        public List<GameModel> Models { get; set; }

        private GameModel GetModel(Games g)
        {
            var refereeName = _refereeRepository.GetRefereeFullName(g.RefereeID);
            var team1 = _teamRepository.GetTeamById(g.Team1ID);
            var team2 = _teamRepository.GetTeamById(g.Team2ID);
            return new GameModel(g.Id, team1.Name, team2.Name, refereeName, g.Stadium);
        }

    }
}
