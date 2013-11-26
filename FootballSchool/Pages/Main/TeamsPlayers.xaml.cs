using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FootballSchool.Kernel;
using FootballSchool.Models;
using FootballSchool.Pages.Details;
using FootballSchool.Repositories;
using FootballSchool.ViewModels;

namespace FootballSchool.Pages.Main
{
    /// <summary>
    /// Interaction logic for TeamsPlayers.xaml
    /// </summary>
    public partial class TeamsPlayers
    {
        private readonly QueryProvider _queryProvider;
        private readonly fscEntities _entities;
        private readonly PlayerInTeamRepository _playerInTeamRepository;
        private readonly GameEventRepository _gameEventRepository;

        public TeamsPlayers()
        {
            InitializeComponent();
            _queryProvider = new QueryProvider();
            _entities = EntityProvider.Entities;
            _gameEventRepository = new GameEventRepository();
            _playerInTeamRepository = new PlayerInTeamRepository();
        }

        private ObjectQuery<GameEvents> GetEventsQuery(int playerId)
        {
            var events = from p in _entities.Players
                         join e in _entities.GameEvents
                         on p.Id equals e.PlayerID
                         where p.Id == playerId
                         select e;

            return (ObjectQuery<GameEvents>)events;
        }

        private ObjectQuery<Players> GetPlayersQuery(int teamId)
        {
            var players = (from pt in _entities.PlayerInTeam
                           join t in _entities.Teams
                               on pt.TeamID equals teamId
                           join p in _entities.Players
                               on pt.PlayerID equals p.Id
                           select p).Distinct();

            return (ObjectQuery<Players>)players;
        }

        public void UpdateGameEvents(int playerId)
        {
            var gameEventsViewSource = (CollectionViewSource)FindResource("gameEventsViewSource");
            var geQuery = GetEventsQuery(playerId);
            var vm = new GameEventVM(geQuery.Select(x => x).ToList());
            gameEventsViewSource.Source = vm.Models;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var teamsViewSource = (CollectionViewSource)FindResource("teamsViewSource");
            var teamsQuery = _queryProvider.GetQuery<Teams>();
            teamsViewSource.Source = teamsQuery.Execute(MergeOption.AppendOnly);

            var playersViewSource = (CollectionViewSource)FindResource("playersViewSource");
            var playersQuery = _queryProvider.GetQuery<Players>();
            playersViewSource.Source = playersQuery.Execute(MergeOption.AppendOnly);

        }

        private void teamsDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedTeam = teamsDataGrid.SelectedItem as Teams;
            if (selectedTeam == null) return;

            var playersViewSource = (CollectionViewSource)FindResource("playersViewSource");
            var playersQuery = GetPlayersQuery(selectedTeam.Id);
            playersViewSource.Source = playersQuery.Execute(MergeOption.OverwriteChanges);
        }

        private void playersDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedPlayer = playersDataGrid.SelectedItem as Players;
            if (selectedPlayer == null) return;

            var gameEventsViewSource = (CollectionViewSource)FindResource("gameEventsViewSource");
            var geQuery = GetEventsQuery(selectedPlayer.Id);
            var vm = new GameEventVM(geQuery.Select(x => x).ToList());
            gameEventsViewSource.Source = vm.Models;
        }

        private void TeamsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Content = new TeamDetail((Teams)teamsDataGrid.SelectedItem);
        }

        private void PlayersDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Content = new PlayerDetail((Players)playersDataGrid.SelectedItem);
        }

        private void GameEventsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var model = gameEventsDataGrid.SelectedItem as GameEventPlayerModel;
            if (model == null) return;

            Content = new GameEventDetail(_gameEventRepository.GetSingle(x => x.Id == model.Id), this, this.Content);
        }

        private void MenuAddTeam_OnClick(object sender, RoutedEventArgs e)
        {
            Content = new TeamDetail();
        }

        private void MenuAddGameEvent_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedPlayer = playersDataGrid.SelectedItem as Players;
            Content = selectedPlayer == null ?
                new GameEventDetail(this, Content) : new GameEventDetail(selectedPlayer, this, Content);
        }

        private void MenuAddPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedTeam = teamsDataGrid.SelectedItem as Teams;

            Content = selectedTeam == null ? new PlayerDetail() : new PlayerDetail(selectedTeam);
        }

        private void MenuRemovePlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedPlayer = playersDataGrid.SelectedItem as Players;
            if (selectedPlayer == null) return;

            _playerInTeamRepository.RemoveByPlayerId(selectedPlayer.Id);
            DeleteObject(selectedPlayer);
        }

        private void MenuRemoveGameEvent_OnClick(object sender, RoutedEventArgs e)
        {
            var model = gameEventsDataGrid.SelectedItem as GameEventPlayerModel;
            if (model == null) return;

            var ge = _gameEventRepository.GetSingle(x => x.Id == model.Id);

            DeleteObject(ge);
            UpdateGameEvents(ge.PlayerID);
        }

        private void MenuRemoveTeam_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedTeam = teamsDataGrid.SelectedItem as Teams;
            if (selectedTeam == null) return;

            _playerInTeamRepository.RemoveByTeamId(selectedTeam.Id);
            DeleteObject(selectedTeam);
        }

        private void DeleteObject(object toDelete)
        {
            _entities.DeleteObject(toDelete);
            _entities.SaveChanges();
        }
    }
}
