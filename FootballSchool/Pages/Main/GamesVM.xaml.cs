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
    /// Interaction logic for GamesVM.xaml
    /// </summary>
    public partial class GamesVM
    {
        private readonly QueryProvider _queryProvider;
        private readonly GameEventRepository _gameEventRepository;
        private readonly fscEntities _entities;
        private readonly GameRepository _gameRepository;

        public GamesVM()
        {
            InitializeComponent();
            _entities = EntityProvider.Entities;
            _queryProvider = new QueryProvider();
            _gameRepository = new GameRepository();
            _gameEventRepository = new GameEventRepository();
        }

        public void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            UpdateGame();

            UpdateGameEvents();
        }

        public void UpdateGame()
        {
            var gamesViewSource = (CollectionViewSource)FindResource("gamesViewSource");
            var gamesVM = new GameVM(_queryProvider.GetQuery<Games>().Select(x => x));
            gamesViewSource.Source = gamesVM.Models;
        }

        public void UpdateGameEvents()
        {
            var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
            var gameEventsVM = new GameEventVM(_queryProvider.GetQuery<GameEvents>().Select(x => x));
            gamesGameEventsViewSource.Source = gameEventsVM.Models;
        }

        private ObjectQuery<GameEvents> GetGameEventsQuery(int gameId)
        {
            var gamesQuery = _entities.GameEvents.Where(x => x.GameID == gameId);
            return (ObjectQuery<GameEvents>)gamesQuery;
        }

        private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;

            if (selectedGame == null) return;

            var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
            gamesGameEventsViewSource.Source = new GameEventVM(GetGameEventsQuery(selectedGame.Id).Select(x => x)).Models;
        }

        private void gamesDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var model = gamesDataGrid.SelectedItem as GameModel;
            if (model == null) return;

            Content = new GameDetail(_gameRepository.GetGameById(model.Id), this, Content);
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            Content = new GameDetail(this, Content);
        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;
            if (selectedGame == null) return;

            _gameRepository.RemoveById(selectedGame.Id);
        }

        private void gameEventsDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;
            if (selectedGame == null) return;

            var model = gameEventsDataGrid.SelectedItem as GameEventPlayerModel;
            if (model == null) return;

            Content = new GameEventDetail(_gameEventRepository.GetSingle(x => x.Id == model.Id), selectedGame, this, Content);
        }

        private void MenuGEAdd_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;
            Content = selectedGame == null ?
                new GameEventDetail(this, Content) : new GameEventDetail(selectedGame.Id, this, Content);
        }

        private void MenuGERemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedGEP = gamesDataGrid.SelectedItem as GameEventPlayerModel;
            if (selectedGEP == null) return;

            _gameEventRepository.Remove(selectedGEP.Id);
        }


    }
}
