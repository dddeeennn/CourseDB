using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FootballSchool.VM;

namespace FootballSchool.Pages
{
    /// <summary>
    /// Interaction logic for TeamsPlayers.xaml
    /// </summary>
    public partial class TeamsPlayers : UserControl
    {
        private fscEntities entities;
        public TeamsPlayers()
        {
            InitializeComponent();
            entities = new fscEntities();
            Resources.Add("Players",new GameEventVM(entities).Players);
            Resources.Add("Events", new GameEventVM(entities).Events);
        }

        private ObjectQuery<Teams> GetTeamsQuery(fscEntities fscEntities)
        {
            var teamsQuery = fscEntities.Teams;
            return teamsQuery;
        }

        private ObjectQuery<Players> GetPlayersQuery(fscEntities fscEntities)
        {
            var playersQuery = fscEntities.Players;
            return playersQuery;
        }

        private ObjectQuery<GameEvents> GetEventsQuery(int playerId)
        {
            var events = (from p in entities.Players
                           join e in entities.GameEvents
                               on p.Id equals e.PlayerID
                           where p.Id == playerId
                           select e);

            return (ObjectQuery<GameEvents>)events;
        }

        private ObjectQuery<Players> GetPlayersQuery(int teamId)
        {
            var players = (from pt in entities.PlayerInTeam
                           join t in entities.Teams
                               on pt.TeamID equals teamId
                           join p in entities.Players
                               on pt.PlayerID equals p.Id
                           select p).Distinct();

            return (ObjectQuery<Players>)players;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var teamsViewSource = ((CollectionViewSource)(FindResource("teamsViewSource")));
            var teamsQuery = GetTeamsQuery(entities);
            teamsViewSource.Source = teamsQuery.Execute(MergeOption.AppendOnly);

            var playersViewSource = ((CollectionViewSource)(FindResource("playersViewSource")));
            var playersQuery = GetPlayersQuery(entities);
            playersViewSource.Source = playersQuery.Execute(MergeOption.AppendOnly);
        }

        private void teamsDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var playersViewSource = ((CollectionViewSource)(FindResource("playersViewSource")));
            var playersQuery = GetPlayersQuery(((Teams)teamsDataGrid.SelectedItem).Id);
            playersViewSource.Source = playersQuery.Execute(MergeOption.OverwriteChanges);
        }

        private void playersDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var gameEventsViewSource = ((CollectionViewSource)(FindResource("gameEventsViewSource")));
            var geQuery = GetEventsQuery(((Players)playersDataGrid.SelectedItem).Id);
            var vm = new GameEventVM(geQuery.Select(x => x).ToList());
            Resources["Players"] =vm.Players;
            Resources["Events"] = vm.Events;
            Resources["Games"] = vm.Games;
            gameEventsViewSource.Source = geQuery.Execute(MergeOption.OverwriteChanges);
        }
    }
}
