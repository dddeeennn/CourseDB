using System.Collections.Generic;
using System.Linq;
using FootballSchool.Models;
using FootballSchool.Repositories;

namespace FootballSchool.ViewModels
{
    /// <summary>
    /// viewModel for game
    /// </summary>
    public class GameVM
    {
        private readonly RefereeRepository _refereeRepository;
        private readonly TeamRepository _teamRepository;

        public GameVM()
        {
            Models = new List<GameModel>();
            _teamRepository = new TeamRepository();
            _refereeRepository = new RefereeRepository();
            Initialize();
        }

        private void Initialize()
        {
            InitializeTeam();
            InitializeReferee();
        }

        private void InitializeReferee()
        {
            Referees = new Dictionary<int, string>();
            foreach (var referee in _refereeRepository.GetAll())
            {
                Referees.Add(referee.Id, _refereeRepository.GetFullName(referee.Id));
            }
        }

        private void InitializeTeam()
        {
            Teams = new Dictionary<int, string>();
            foreach (var team in _teamRepository.GetAll())
            {
                Teams.Add(team.Id, team.Name);
            }
        }

        public GameVM(IEnumerable<Game> games)
            : this()
        {
            foreach (var model in games.Select(GetModel))
            {
                Models.Add(model);
            }
        }

        public GameVM(Game game)
            : this(game.Id, game.Team1ID, game.Team2ID, game.RefereeID, game.Stadium, game.Type)
        {
            SelectedRefereeId = Referees.Keys.ToList().IndexOf(RefereeId);
            SelectedTeam1Id = Teams.Keys.ToList().IndexOf(Team1Id);
            SelectedTeam2Id = Teams.Keys.ToList().IndexOf(Team2Id);
        }

        public GameVM(int id, int team1Id, int team2Id, int refereeId, string stadium, bool type)
            : this()
        {
            Id = id;
            Team1Id = team1Id;
            Team2Id = team2Id;
            RefereeId = refereeId;
            Stadium = stadium;
            Type = type;
        }

        public int Id { get; set; }

        public Dictionary<int, string> Teams { get; set; }

        public int Team1Id { get; set; }

        public int SelectedTeam1Id { get; set; }

        public int Team2Id { get; set; }

        public int SelectedTeam2Id { get; set; }

        public Dictionary<int, string> Referees { get; set; }

        public int RefereeId { get; set; }

        public int SelectedRefereeId { get; set; }

        public string Stadium { get; set; }

        public bool Type { get; set; }

        public List<GameModel> Models { get; set; }

        private GameModel GetModel(Game g)
        {
            var refereeName = _refereeRepository.GetFullName(g.RefereeID);
            var team1 = _teamRepository.GetSingle(x => x.Id == g.Team1ID);
            var team2 = _teamRepository.GetSingle(x => x.Id == g.Team2ID);
            return new GameModel(g.Id, team1.Name, team2.Name, refereeName, g.Stadium, g.Type);
        }

    }
}
