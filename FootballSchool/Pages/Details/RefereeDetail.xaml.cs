using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FootballSchool.Kernel;
using FootballSchool.Kernel.Extensions;
using FootballSchool.Pages.Main;

namespace FootballSchool.Pages.Details
{
	/// <summary>
	/// Interaction logic for RefereeDetail.xaml
	/// </summary>
	public partial class RefereeDetail
	{
		private Referee _editableReferee;
		private fscEntities _entities;

		public RefereeDetail()
		{
			InitializeComponent();
			_entities = EntityProvider.Entities;
		}

		public RefereeDetail(Referee referee)
			: this()
		{
			_editableReferee = referee;
		}

		private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
		{
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
			if (_editableReferee != null)
			{
				var myCollectionViewSource = (CollectionViewSource)FindResource("refereesViewSource");
				var query = GetRefereeQuery(_editableReferee.Id);
				myCollectionViewSource.Source = query.Execute(MergeOption.OverwriteChanges);
			}
		}

		private ObjectQuery<Referee> GetRefereeQuery(int id)
		{
			var res = from Referee r in _entities.Referee
					  where r.Id == id
					  select r;
			return (ObjectQuery<Referee>)res;
		}

		private void ButtonCancelClick(object sender, RoutedEventArgs e)
		{
			CloseControl();
		}

		private void ButtonSaveClick(object sender, RoutedEventArgs e)
		{
			_entities.Referee.ApplyCurrentValues(_editableReferee);
			CloseControl();
		}

		/// <summary>
		/// Save and refresh.
		/// </summary>
		private void SaveAndRefresh()
		{
			_entities.SaveChanges();
			_entities.Refresh(RefreshMode.StoreWins, _entities.Teams);
		}

		/// <summary>
		/// Closes this control.
		/// </summary>
		private void CloseControl()
		{
			SaveAndRefresh();
			this.TryFindParent<UserControl>().Content = new Referies();
		}
	}
}
