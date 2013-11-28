using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Windows;
using System.Windows.Controls;
using FootballSchool.Kernel;
using FootballSchool.Kernel.Extensions;
using FootballSchool.Models;
using FootballSchool.Pages.Main;
using FootballSchool.Repositories;
using FootballSchool.ViewModels;

namespace FootballSchool.Pages.Details
{
    /// <summary>
    /// Interaction logic for GameEventDetail.xaml
    /// </summary>
    public partial class GameEventDetail
    {
        private readonly fscEntities _entities;
        private readonly EventVM _viewModel = new EventVM();
        private readonly GameEvents _editableGE;
        private object _parentUCContent;
        private UserControl _parentUC;
        private GameRepository _gameRepository;

        public GameEventDetail(UserControl parentContent, object content)
        {
            _parentUCContent = content;
            _parentUC = parentContent;
            _gameRepository = new GameRepository();
            _entities = EntityProvider.Entities;
            InitializeComponent();
            DataContext = _viewModel;
        }

        public GameEventDetail(GameEvents gameEvent,Players player,
            UserControl parentUCContent, object content)
            : this(parentUCContent, content)
        {
            _editableGE = gameEvent;
            _viewModel = new EventVM(gameEvent, player);
            CmbPlayer.IsEnabled = false;
            DataContext = _viewModel;
        }

        public GameEventDetail(GameEvents gameEvent,GameModel game,
           UserControl parentUCContent, object content)
            : this(parentUCContent, content)
        {
            _editableGE = gameEvent;
            _viewModel = new EventVM(gameEvent,game);
            CmbGame.IsEnabled = false;
            DataContext = _viewModel;
        }

        public GameEventDetail(Players selectedPlayer, UserControl parentUCContent, object content)
            : this(parentUCContent, content)
        {
            _viewModel = new EventVM(selectedPlayer);
            CmbPlayer.IsEnabled = false;
            DataContext = _viewModel;
        }

        public GameEventDetail(int gameId, UserControl gamesVM, object content)
            : this(gamesVM, content)
        {
            _viewModel = new EventVM(gameId);
            CmbGame.IsEnabled = false;
            DataContext = _viewModel;
        }

        /// <summary>
        /// Edit game event.
        /// </summary>
        private void EditGameEvent()
        {
            Map(_viewModel, _editableGE);
            _entities.GameEvents.ApplyCurrentValues(_editableGE);
        }

        private void Map(EventVM viewModel, GameEvents entity)
        {
            entity.Time = viewModel.Time;
            viewModel.GameId = ((KeyValuePair<int, string>)CmbGame.SelectedItem).Key;
            viewModel.PlayerId = ((KeyValuePair<int, string>)CmbPlayer.SelectedItem).Key;
            viewModel.EventId = ((KeyValuePair<int, string>)CmbEvent.SelectedItem).Key;
            entity.EventID = viewModel.EventId;
            entity.PlayerID = viewModel.PlayerId;
            entity.GameID = viewModel.GameId;
        }

        /// <summary>
        /// Add new game event.
        /// </summary>
        private void AddGameEvent()
        {
            var ge = new GameEvents();
            Map(_viewModel, ge);
            _entities.GameEvents.AddObject(ge);
            SaveAndRefresh();
        }

        /// <summary>
        /// Closes this control.
        /// </summary>
        private void CloseControl()
        {
            SaveAndRefresh();

            this.TryFindParent<UserControl>().Content = _parentUCContent;

            if (_parentUC is TeamsPlayers)
            {
                ((TeamsPlayers)_parentUC).UpdateGameEvents(_viewModel.PlayerId);
            }

            if (_parentUC is GamesVM)
            {
                ((GamesVM)_parentUC).UpdateGameEvents();
            }

            if (_parentUC is Referies)
            {
                ((Referies)_parentUC).UpdateGameEvents(_viewModel.GameId);
            }
        }

        /// <summary>
        /// Save and refresh.
        /// </summary>
        private void SaveAndRefresh()
        {
            _entities.SaveChanges();
            _entities.Refresh(RefreshMode.StoreWins, _entities.Teams);
            _entities.Refresh(RefreshMode.StoreWins, _entities.GameEvents);
            _entities.Refresh(RefreshMode.StoreWins, _entities.Players);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (_editableGE == null) AddGameEvent();
            else EditGameEvent();

            CloseControl();
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            CloseControl();
        }

        private void CmbGame_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = CmbGame.SelectedItem as KeyValuePair<int, string>?;

            if (selected == null) return;

            var game = _gameRepository.GetGameById(selected.Value.Key);

            _viewModel.UpdatePlayer(game.Team1ID, game.Team2ID);
        }

        private void CmbPlayer_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
