using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    public partial class GamesVM : UserControl
    {
        private readonly QueryProvider _queryProvider;
        private fscEntities _entities;
        private GameRepository _gameRepository;

        public GamesVM()
        {
            InitializeComponent();
            _entities = EntityProvider.Entities;
            _queryProvider = new QueryProvider();
            _gameRepository = new GameRepository();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            var gamesViewSource = (CollectionViewSource)FindResource("gamesViewSource");
            var gamesVM = new GameVM(_queryProvider.GetQuery<Game>().Select(x => x));
            gamesViewSource.Source = gamesVM.Models;

            var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
            var gameEventsVM = new GameEventVM(_queryProvider.GetQuery<GameEvent>().Select(x => x));
            gamesGameEventsViewSource.Source = gameEventsVM.Models;
        }

        private ObjectQuery<GameEvent> GetGameEventsQuery(int gameId)
        {
            var gamesQuery = _entities.GameEvents.Where(x => x.GameID == gameId);
            return (ObjectQuery<GameEvent>)gamesQuery;
        }

        private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
		    var selectedGame = gamesDataGrid.SelectedItem as GameModel;

            if(selectedGame==null)return;

			var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
			gamesGameEventsViewSource.Source = new GameEventVM(GetGameEventsQuery(selectedGame.Id).Select(x => x)).Models;
		}

        private void gamesDataGrid_MouseDoubleClick_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var model = gamesDataGrid.SelectedItem as GameModel;
            if (model == null) return;

            Content = new GameDetail(_gameRepository.GetGameById(model.Id));
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            Content = new GameDetail();
        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedGame = gamesDataGrid.SelectedItem as GameModel;
            if (selectedGame == null) return;

            _gameRepository.RemoveById(selectedGame.Id);
        }
    }
}
