using System.Collections.Generic;
using System.Data.Objects;
using System.Windows;
using System.Windows.Controls;
using FootballSchool.Kerenl.Extensions;
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
		private TeamVM _viewVm = new TeamVM();

		public TeamDetail(Team team)
			: this()
		{
			_editableTeam = team;
			_viewVm = new TeamVM(team);
			DataContext = _viewVm;
		}

		public TeamDetail()
		{
			_entities = new fscEntities();
			InitializeComponent();
			DataContext = _viewVm;
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
			_editableTeam.City = _viewVm.City;
			_editableTeam.Name = _viewVm.Name;
			_editableTeam.CoachID = _viewVm.CoachId;
			_entities.Teams.ApplyCurrentValues(_editableTeam);
		}

		/// <summary>
		/// Add new team.
		/// </summary>
		private void AddTeam()
		{
			_entities.Teams.AddObject(new Team
			{
				Name = NameTb.Text,
				City = CityTb.Text,
				CoachID = ((KeyValuePair<int, string>)CmbCoach.SelectedItem).Key
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
	}
}
