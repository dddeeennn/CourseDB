using System.Collections.Generic;
using System.Data.Objects;
using System.Windows;
using System.Windows.Controls;
using FootballSchool.Kernel;
using FootballSchool.Kernel.Extensions;
using FootballSchool.Pages.Main;
using FootballSchool.ViewModels;

namespace FootballSchool.Pages.Details
{
	/// <summary>
	/// Interaction logic for GameDetail.xaml
	/// </summary>
	public partial class GameDetail
	{
		private Games _editableGame;
		private readonly fscEntities _entities;
		private GameVM _viewModel = new GameVM();

		private object _parentUCContent;
		private UserControl _parentUC;

		public GameDetail(UserControl parentContent, object content)
		{
			_parentUCContent = content;
			_parentUC = parentContent;
			_entities = EntityProvider.Entities;
			InitializeComponent();
			DataContext = _viewModel;
		}

		public GameDetail(Games game, UserControl parentUCContent, object content)
			: this(parentUCContent, content)
		{
			_editableGame = game;
			_viewModel = new GameVM(game);
			DataContext = _viewModel;
		}

		private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
		{
			if (_editableGame == null) AddGame();
			else EditGame();

			CloseControl();
		}

		private void EditGame()
		{
			Map(_editableGame, _viewModel);
			_entities.Games.ApplyCurrentValues(_editableGame);
		}

		private void AddGame()
		{
			var game = new Games();
			Map(game, _viewModel);
			_entities.Games.AddObject(game);

		}

		private void Map(Games game, GameVM viewModel)
		{
			viewModel.RefereeId = ((KeyValuePair<int, string>)CmbReferee.SelectedItem).Key;
			viewModel.Team1Id = ((KeyValuePair<int, string>)CmbTeam1.SelectedItem).Key;
			viewModel.Team2Id = ((KeyValuePair<int, string>)CmbTeam2.SelectedItem).Key;
			game.RefereeID = viewModel.RefereeId;
			game.Team1ID = viewModel.Team1Id;
			game.Team2ID = viewModel.Team2Id;
			game.Stadium = viewModel.Stadium;
			game.Type = viewModel.Type;
		}

		private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
		{
			CloseControl();
		}

		/// <summary>
		/// Closes this control.
		/// </summary>
		private void CloseControl()
		{
			_entities.SaveChanges();
			_entities.Refresh(RefreshMode.StoreWins, _entities.Games);

			this.TryFindParent<UserControl>().Content = _parentUCContent;

			if (_parentUC is GamesVM)
			{
				((GamesVM)_parentUC).UpdateGame();
			}

			if (_parentUC is Referies)
			{
				((Referies)_parentUC).UpdateGames();
			}
		}
	}
}
