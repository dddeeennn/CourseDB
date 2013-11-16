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
	/// Interaction logic for TeamDetail.xaml
	/// </summary>
	public partial class TeamDetail
	{
		private readonly fscEntities _entities;

		private readonly Team _editableTeam;
		private readonly TeamVM _viewModel = new TeamVM();

		public TeamDetail(Team team)
			: this()
		{
			_editableTeam = team;
			_viewModel = new TeamVM(team);
			DataContext = _viewModel;
		}

		public TeamDetail()
		{
            _entities = EntityProvider.Entities;
			InitializeComponent();
			DataContext = _viewModel;
		}

		private void Button_CancelClick(object sender, RoutedEventArgs e)
		{
			CloseControl();
		}

		private void Button_SaveClick(object sender, RoutedEventArgs e)
		{
			if (_editableTeam == null) AddTeam();
			else EditTeam();

			CloseControl();
		}

		/// <summary>
		/// Edit team.
		/// </summary>
		private void EditTeam()
		{
            Map(_editableTeam, _viewModel);
			_entities.Teams.ApplyCurrentValues(_editableTeam);
		}

        private void Map(Team team, TeamVM viewModel)
        {
            team.City = viewModel.City;
            team.Name = viewModel.Name;
            team.CoachID = ((KeyValuePair<int, string>)CmbCoach.SelectedItem).Key;
        }

		/// <summary>
		/// Add new team.
		/// </summary>
		private void AddTeam()
		{
		    var team = new Team();
            Map(team, _viewModel);
			_entities.Teams.AddObject(team);
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
	}
}
