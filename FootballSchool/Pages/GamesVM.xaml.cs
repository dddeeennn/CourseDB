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

			var gamesViewSource = (CollectionViewSource)FindResource("gamesViewSource");
			var gamesQuery = GetGamesQuery(entities);
		    var gamesVM = new GameVM(gamesQuery.Select(x => x).ToList());
			gamesViewSource.Source = gamesVM.Models;

			var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
			var gameEventsQuery = GetGameEventsQuery(entities);
		    var gameEventsVM = new GameEventVM(gameEventsQuery.Select(x => x));
            gamesGameEventsViewSource.Source = gameEventsVM.Models;
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
			var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
			var gameEventsQuery = GetGameEventsQuery(((GameModel)gamesDataGrid.SelectedItem).Id);
            var vm = new GameEventVM(gameEventsQuery.Select(x => x).ToList());
            gamesGameEventsViewSource.Source = vm.Models;
		}
	}
}
