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
    }
}
