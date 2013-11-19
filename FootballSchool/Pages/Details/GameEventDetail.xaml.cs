﻿using System;
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
    /// Interaction logic for GameEventDetail.xaml
    /// </summary>
    public partial class GameEventDetail
    {
        private readonly fscEntities _entities;
        private readonly EventVM _viewModel = new EventVM();
        private readonly GameEvent _editableGE;
        private object _parentUCContent;
        private UserControl _parentUC;

        public GameEventDetail(UserControl parentContent, object content)
        {
            _parentUCContent = content;
            _parentUC = parentContent;
            _entities = EntityProvider.Entities;
            InitializeComponent();
            DataContext = _viewModel;
        }

        public GameEventDetail(GameEvent gameEvent, UserControl parentUCContent, object content)
            : this(parentUCContent,content)
        {
            _editableGE = gameEvent;
            _viewModel = new EventVM(gameEvent);
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

        private void Map(EventVM viewModel, GameEvent entity)
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
            var ge = new GameEvent();
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
            if(_parentUCContent is TeamsPlayers)
            {
                this.TryFindParent<UserControl>().Content = _parentUCContent;
                ((TeamsPlayers)_parentUC).UpdateGameEvents(_viewModel.PlayerId);
            }
            else
            {
                this.TryFindParent<UserControl>().Content = _parentUCContent;
                ((GamesVM)_parentUC).UpdateGameEvents();
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
    }
}
