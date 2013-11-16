using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FootballSchool.Kernel;
using FootballSchool.Models;
using FootballSchool.ViewModels;

namespace FootballSchool.Pages.Main
{
	/// <summary>
	/// Interaction logic for GamesVM.xaml
	/// </summary>
	public partial class GamesVM : UserControl
	{
		public GamesVM()
		{
			InitializeComponent();
		    entities = EntityProvider.Entities;
		}

		private fscEntities entities;

		private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
		{
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

			var gamesViewSource = (CollectionViewSource)FindResource("gamesViewSource");
			var gamesVM = new GameVM(GetGamesQuery(entities).Select(x => x));
			gamesViewSource.Source = gamesVM.Models;

			var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
			var gameEventsVM = new GameEventVM(GetGameEventsQuery(entities).Select(x => x));
			gamesGameEventsViewSource.Source = gameEventsVM.Models;
		}

		private ObjectQuery<Game> GetGamesQuery(fscEntities fscEntities)
		{
			var gamesQuery = fscEntities.Games;
			return gamesQuery;
		}

		private ObjectQuery<GameEvent> GetGameEventsQuery(fscEntities fscEntities)
		{
			var gamesQuery = fscEntities.GameEvents;
			return gamesQuery;
		}

		private ObjectQuery<GameEvent> GetGameEventsQuery(int gameId)
		{
			var gamesQuery = entities.GameEvents.Where(x => x.GameID == gameId);
			return (ObjectQuery<GameEvent>)gamesQuery;
		}

		private void gamesDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			var gamesGameEventsViewSource = (CollectionViewSource)FindResource("gamesGameEventsViewSource");
			var gameEventsQuery = GetGameEventsQuery(((GameModel)gamesDataGrid.SelectedItem).Id);
			gamesGameEventsViewSource.Source = new GameEventVM(gameEventsQuery.Select(x => x)).Models;
		}
	}
}
