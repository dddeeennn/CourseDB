using System.Windows;
using System.Windows.Controls;
using FootballSchool.Kernel.Extensions;
using FootballSchool.Pages.Main;

namespace FootballSchool.Pages.Details
{
	/// <summary>
	/// Interaction logic for GameDetail.xaml
	/// </summary>
	public partial class GameDetail : UserControl
	{
		public GameDetail()
		{
			InitializeComponent();
		}

		public bool IsSaveChanges { get; set; }

		private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
		{
			CloseControl();
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
			this.TryFindParent<UserControl>().Content = new TeamsPlayers();
		}
	}
}
