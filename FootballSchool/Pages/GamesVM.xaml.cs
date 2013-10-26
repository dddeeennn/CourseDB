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
    /// Interaction logic for GamesVM.xaml
    /// </summary>
    public partial class GamesVM : UserControl
    {
        public GamesVM()
        {
            InitializeComponent();
            entities = new fscEntities();
        }

        private fscEntities entities;

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var gamesViewSource = ((CollectionViewSource)(FindResource("gamesViewSource")));
            var gamesQuery = GetGamesQuery(entities);
            gamesViewSource.Source = gamesQuery.Execute(MergeOption.AppendOnly);

            var gamesGameEventsViewSource = ((CollectionViewSource)(FindResource("gamesGameEventsViewSource")));
            var gameEventsQuery = GetGameEventsQuery(entities);
            gamesGameEventsViewSource.Source = gameEventsQuery.Execute(MergeOption.AppendOnly);
        }

        private ObjectQuery<Games> GetGamesQuery(fscEntities fscEntities)
        {
            var gamesQuery = fscEntities.Games;
            return gamesQuery;
        }

        private ObjectQuery<GameEvents> GetGameEventsQuery(fscEntities fscEntities)
        {
            var gamesQuery = fscEntities.GameEvents;
            return gamesQuery;
        }

        private ObjectQuery<GameEvents> GetGameEventsQuery(int gameId)
        {
            var gamesQuery = entities.GameEvents.Where(x => x.GameID == gameId);
            return (ObjectQuery<GameEvents>)gamesQuery;
        }

        private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var gamesGameEventsViewSource = ((CollectionViewSource)(FindResource("gamesGameEventsViewSource")));
            var gameEventsQuery = GetGameEventsQuery(((Games)gamesDataGrid.SelectedItem).Id);
            gamesGameEventsViewSource.Source = gameEventsQuery.Execute(MergeOption.OverwriteChanges);
        }
    }
}
