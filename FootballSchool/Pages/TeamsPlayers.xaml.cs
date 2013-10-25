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
        public TeamsPlayers()
        {
            InitializeComponent();
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

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var fscEntities = new fscEntities();
            var teamsViewSource = ((CollectionViewSource)(FindResource("teamsViewSource")));
            var teamsQuery = GetTeamsQuery(fscEntities);
            teamsViewSource.Source = teamsQuery.Execute(MergeOption.AppendOnly);

            var playersViewSource = ((CollectionViewSource)(FindResource("playersViewSource")));
            var playersQuery = GetPlayersQuery(fscEntities);
            playersViewSource.Source = playersQuery.Execute(MergeOption.AppendOnly);
        }
    }
}
