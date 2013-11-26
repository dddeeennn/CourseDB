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
    /// Interaction logic for Referies.xaml
    /// </summary>
    public partial class Referies
    {
        private QueryProvider _queryProvider;
        private GameRepository _gameRepository;
        private GameEventRepository _gameEventRepository;
        private RefereeRepository _refereeRepository;
        private fscEntities entities;

        public Referies()
        {
            InitializeComponent();
            entities = EntityProvider.Entities;
            _queryProvider = new QueryProvider();
            _gameRepository = new GameRepository();
            _gameEventRepository = new GameEventRepository();
            _refereeRepository = new RefereeRepository();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var refereeViewSource = (CollectionViewSource)FindResource("refereeViewSource");
            refereeViewSource.Source = _queryProvider.GetQuery<Referee>().Execute(MergeOption.AppendOnly);

            UpdateGames();
        }

        public void UpdateGames()
        {
            var refereeGamesViewSource = (CollectionViewSource)FindResource("refereeGamesViewSource");
            var gamesVM = new GameVM(_queryProvider.GetQuery<Games>().Select(x => x));
            refereeGamesViewSource.Source = gamesVM.Models;
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
            var selectedReferee = refereeDataGrid.SelectedItem as Referee;
            if (selectedReferee == null) return;

            var refereeGamesViewSource = (CollectionViewSource)FindResource("refereeGamesViewSource");
            var gamesVM = new GameVM(GetRefereeQuery(selectedReferee.Id).Select(x => x));
            refereeGamesViewSource.Source = gamesVM.Models;
        }

        private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;
            if (selectedGame == null) return;

            UpdateGameEvents(selectedGame.Id);
        }

        private void MenuRefereeAdd_Click(object sender, RoutedEventArgs e)
        {
            Content = new RefereeDetail();
        }

        private void MenuRefereeRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedReferee = refereeDataGrid.SelectedItem as Referee;
            if (selectedReferee == null) return;

            _refereeRepository.Remove(selectedReferee.Id);
        }

        private void refereeDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var model = refereeDataGrid.SelectedItem as Referee;
            if (model == null) return;

            Content = new RefereeDetail(_refereeRepository.GetRefereeById(model.Id));
        }

        private void MenuAddGame_Click(object sender, RoutedEventArgs e)
        {
            var model = refereeDataGrid.SelectedItem as Referee;
            Content = model == null ? new GameDetail(this, Content) : new GameDetail(model, this, Content);
        }

        private void MenuRemoveGame_Click(object sender, RoutedEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;
            if (selectedGame == null) return;

            _gameRepository.RemoveById(selectedGame.Id);
        }

        private void gamesDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var model = gamesDataGrid.SelectedItem as GameModel;
            if (model == null) return;

            Content = new GameDetail(_gameRepository.GetGameById(model.Id), this, Content);
        }

        private void gameEventsDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var model = gameEventsDataGrid.SelectedItem as GameEventPlayerModel;
            if (model == null) return;

            Content = new GameEventDetail(_gameEventRepository.GetSingle(x => x.Id == model.Id), this, Content);
        }

        private void MenuAddGameEvent_OnClick(object sender, RoutedEventArgs e)
        {
            var model = gamesDataGrid.SelectedItem as GameModel;
            Content = model == null ? new GameEventDetail(this, Content) : new GameEventDetail(model.Id, this, Content);
        }

        private void MenuRemoveGameEvent_OnClick(object sender, RoutedEventArgs e)
        {
            var model = gameEventsDataGrid.SelectedItem as GameEventPlayerModel;
            if (model == null) return;

            _gameEventRepository.Remove(model.Id);
            UpdateGameEvents();
        }

        public void UpdateGameEvents(int gameId)
        {
            var gameEventsViewSource = (CollectionViewSource)FindResource("refereeGamesGameEventsViewSource");
            var gameEventVM = new GameEventVM(GetRefereeGameQuery(gameId).Select(x => x));
            gameEventsViewSource.Source = gameEventVM.Models;
        }

        public void UpdateGameEvents()
        {
            var gameEventsViewSource = (CollectionViewSource)FindResource("refereeGamesGameEventsViewSource");
            var gameEventVM = new GameEventVM(_queryProvider.GetQuery<GameEvents>().Select(x => x));
            gameEventsViewSource.Source = gameEventVM.Models;
        }
    }
}
