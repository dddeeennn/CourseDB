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
            refereeGamesViewSource.Source = refereeGamesQuery.Execute(MergeOption.AppendOnly);
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
                                   where r.Id ==refereeId
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
            var refereeGamesViewSource = ((CollectionViewSource)(FindResource("refereeGamesViewSource")));
            if (refereeDataGrid.SelectedItem != null)
            {
                var refereeQuery = GetRefereeQuery(((Referee)refereeDataGrid.SelectedItem).Id);
                refereeGamesViewSource.Source = refereeQuery.Execute(MergeOption.OverwriteChanges);
            }
        }

        private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var gameEventsViewSource = ((CollectionViewSource)(FindResource("gameEventsViewSource")));
            if (gamesDataGrid.SelectedItem != null)
            {
                var refereeGamesQuery = GetRefereeGameQuery(((Games)gamesDataGrid.SelectedItem).Id);
                gameEventsViewSource.Source = refereeGamesQuery.Execute(MergeOption.OverwriteChanges);
            }
        }
    }
}
