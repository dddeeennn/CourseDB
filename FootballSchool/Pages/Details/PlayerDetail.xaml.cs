using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballSchool.Kernel;
using FootballSchool.Kernel.Extensions;
using FootballSchool.Pages.Main;
using FootballSchool.ViewModels;

namespace FootballSchool.Pages.Details
{
    /// <summary>
    /// Interaction logic for PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetail
    {
        private readonly fscEntities _entities;
        private readonly PlayerVM _viewModel = new PlayerVM();
        private readonly Player _editablePlayer;

        public PlayerDetail()
        {
            _entities = EntityProvider.Entities;
            InitializeComponent();
            DataContext = _viewModel;
        }

        public PlayerDetail(Player player)
            : this()
        {
            _editablePlayer = player;
            _viewModel = new PlayerVM(player);
            DataContext = _viewModel;
        }

        /// <summary>
        /// Edit player.
        /// </summary>
        private void EditPlayer()
        {
            Map(_viewModel, _editablePlayer);
            _entities.Players.ApplyCurrentValues(_editablePlayer);
            
            var playerInTeam = _entities.PlayerInTeams.First(x => x.PlayerID == _editablePlayer.Id);
            playerInTeam.TeamID = _viewModel.TeamId;
            _entities.PlayerInTeams.ApplyCurrentValues(playerInTeam);
        }

        private void Map(PlayerVM viewModel, Player entity)
        {
            entity.LastName = viewModel.LastName;
            entity.Name = viewModel.Name;
            entity.MiddleName = viewModel.MiddleName;
            entity.Passport = viewModel.Passport;
            entity.IsMain = viewModel.IsMain;
            viewModel.TeamId = ((KeyValuePair<int, string>)CmbTeam.SelectedItem).Key;
            viewModel.PositionId = ((KeyValuePair<int, string>)CmbPosition.SelectedItem).Key;
            entity.PositionID = viewModel.PositionId;
        }

        /// <summary>
        /// Add new player.
        /// </summary>
        private void AddPlayer()
        {
            var player = new Player();
            Map(_viewModel, player);
            _entities.Players.AddObject(player);
            SaveAndRefresh();

            player = _entities.Players.First(x => x.Passport == _viewModel.Passport);
            _entities.AddToPlayerInTeams(new PlayerInTeam
            {
                PlayerID = player.Id,
                TeamID = _viewModel.TeamId
            });
        }

        /// <summary>
        /// Closes this control.
        /// </summary>
        private void CloseControl()
        {
            SaveAndRefresh();
            this.TryFindParent<UserControl>().Content = new TeamsPlayers();
        }

        /// <summary>
        /// Save and refresh.
        /// </summary>
        private void SaveAndRefresh()
        {
            _entities.SaveChanges();
            _entities.Refresh(RefreshMode.StoreWins, _entities.Teams);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (_editablePlayer == null) AddPlayer();
            else EditPlayer();

            CloseControl();
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            CloseControl();
        }
    }
}
