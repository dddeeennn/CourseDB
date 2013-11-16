using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FootballSchool.Kernel;
using FootballSchool.Pages.Details;
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

        public TeamsPlayers()
        {
            InitializeComponent();
            _queryProvider = new QueryProvider();
            _entities = EntityProvider.Entities;
        }

        private ObjectQuery<GameEvent> GetEventsQuery(int playerId)
        {
            var events = from p in _entities.Players
                         join e in _entities.GameEvents
                         on p.Id equals e.PlayerID
                         where p.Id == playerId
                         select e;

            return (ObjectQuery<GameEvent>)events;
        }

        private ObjectQuery<Player> GetPlayersQuery(int teamId)
        {
            var players = (from pt in _entities.PlayerInTeams
                           join t in _entities.Teams
                               on pt.TeamID equals teamId
                           join p in _entities.Players
                               on pt.PlayerID equals p.Id
                           select p).Distinct();

            return (ObjectQuery<Player>)players;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var teamsViewSource = (CollectionViewSource)FindResource("teamsViewSource");
            var teamsQuery = _queryProvider.GetQuery<Team>();
            teamsViewSource.Source = teamsQuery.Execute(MergeOption.AppendOnly);

            var playersViewSource = (CollectionViewSource)FindResource("playersViewSource");
            var playersQuery = _queryProvider.GetQuery<Player>();
            playersViewSource.Source = playersQuery.Execute(MergeOption.AppendOnly);
        }

        private void teamsDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedTeam = teamsDataGrid.SelectedItem as Team;
            if (selectedTeam == null) return;

            var playersViewSource = (CollectionViewSource)FindResource("playersViewSource");
            var playersQuery = GetPlayersQuery(selectedTeam.Id);
            playersViewSource.Source = playersQuery.Execute(MergeOption.OverwriteChanges);
        }

        private void playersDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedPlayer = playersDataGrid.SelectedItem as Player;
            if (selectedPlayer == null) return;

            var gameEventsViewSource = (CollectionViewSource)FindResource("gameEventsViewSource");
            var geQuery = GetEventsQuery(selectedPlayer.Id);
            var vm = new GameEventVM(geQuery.Select(x => x).ToList());
            gameEventsViewSource.Source = vm.Models;
        }

        private void TeamsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Content = new TeamDetail((Team)teamsDataGrid.SelectedItem);
        }

        private void PlayersDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Content = new PlayerDetail((Player)playersDataGrid.SelectedItem);
        }

        private void GameEventsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Content = new GameEventDetail((GameEvent)gameEventsDataGrid.SelectedItem);
        }

        private void MenuAddTeam_OnClick(object sender, RoutedEventArgs e)
        {
            Content = new TeamDetail();
        }

        private void MenuAddGameEvent_OnClick(object sender, RoutedEventArgs e)
        {
            Content = new GameEventDetail();
        }

        private void MenuAddPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            Content = new PlayerDetail();
        }

        private void MenuRemovePlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedPlayer = playersDataGrid.SelectedItem as Player;
            if (selectedPlayer == null) return;

            DeleteObject(selectedPlayer);
        }

        private void MenuRemoveGameEvent_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedGameEvent = gameEventsDataGrid.SelectedItem as GameEvent;
            if (selectedGameEvent == null) return;

            DeleteObject(selectedGameEvent);
        }

        private void MenuRemoveTeam_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedTeam = teamsDataGrid.SelectedItem as Team;
            if (selectedTeam == null) return;

            DeleteObject(selectedTeam);
        }

        private void DeleteObject(object toDelete)
        {
            _entities.DeleteObject(toDelete);
            _entities.SaveChanges();
        }
    }
}
