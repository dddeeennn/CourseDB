using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FootballSchool.Models;
using FootballSchool.ViewModels;

namespace FootballSchool.Pages
{
    /// <summary>
    /// Interaction logic for Referies.xaml
    /// </summary>
    public partial class Referies : UserControl
    {
        public Referies()
        {
            InitializeComponent();
            entities = new fscEntities();
        }

        private fscEntities entities;

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var refereeViewSource = ((CollectionViewSource)(FindResource("refereeViewSource")));
            var refereeQuery = GetRefereeQuery(entities);
            refereeViewSource.Source = refereeQuery.Execute(MergeOption.AppendOnly);

            var refereeGamesViewSource = ((CollectionViewSource)(FindResource("refereeGamesViewSource")));
            var refereeGamesQuery = GetRefereeGamesQuery(entities);
            var gamesVM = new GameVM(refereeGamesQuery.Select(x => x));
            refereeGamesViewSource.Source = gamesVM.Models;
        }

        private ObjectQuery<Referee> GetRefereeQuery(fscEntities fscEntities)
        {
            var refereeQuery = fscEntities.Referee;
            return refereeQuery;
        }

        private ObjectQuery<Games> GetRefereeQuery(int refereeId)
        {
            var refereeQuery = from r in entities.Referee
                               join g in entities.Games
                                   on r.Id equals g.RefereeID
                               where r.Id == refereeId
                               select g;
            return (ObjectQuery<Games>)refereeQuery;
        }

        private ObjectQuery<Games> GetRefereeGamesQuery(fscEntities fscEntities)
        {
            var refereeGamesQuery = entities.Games;
            return refereeGamesQuery;
        }

        private ObjectQuery<GameEvents> GetRefereeGameQuery(int gameId)
        {
            var refereeGamesQuery = from g in entities.Games
                                    join ge in entities.GameEvents
                                        on g.Id equals ge.GameID
                                    where ge.GameID == gameId
                                    select ge;
            return (ObjectQuery<GameEvents>)refereeGamesQuery;
        }

        private void refereeDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var refereeGamesViewSource = (CollectionViewSource)FindResource("refereeGamesViewSource");
            if (refereeDataGrid.SelectedItem == null) return;
            var refereeQuery = GetRefereeQuery(((Referee)refereeDataGrid.SelectedItem).Id);
            var gamesVM = new GameVM(refereeQuery.Select(x => x));
            refereeGamesViewSource.Source = gamesVM.Models;
        }

        private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var gameEventsViewSource = ((CollectionViewSource)(FindResource("gameEventsViewSource")));
            if (gamesDataGrid.SelectedItem != null)
            {
                var refereeGamesQuery = GetRefereeGameQuery(((GameModel)gamesDataGrid.SelectedItem).Id);
                var gameEventVM = new GameEventVM(refereeGamesQuery.Select(x => x));
                gameEventsViewSource.Source = gameEventVM.Models;
            }
        }
    }
}
