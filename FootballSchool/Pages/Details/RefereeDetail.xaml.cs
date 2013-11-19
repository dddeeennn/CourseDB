using System.Data.Objects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FootballSchool.Kernel;

namespace FootballSchool.Pages.Details
{
	/// <summary>
	/// Interaction logic for RefereeDetail.xaml
	/// </summary>
	public partial class RefereeDetail
	{
        private Referee _editableReferee;
	    private QueryProvider _queryProvider;
	    private fscEntities _entities;

		public RefereeDetail()
		{
			InitializeComponent();
            _queryProvider = new QueryProvider();
		    _entities = EntityProvider.Entities;
		}

        public RefereeDetail(Referee referee):this()
        {
            _editableReferee = referee;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            if(_editableReferee!=null)
            {
                var myCollectionViewSource = (CollectionViewSource) FindResource("gameEventsViewSource");
                var query = GetRefereeQuery(_editableReferee.Id);
                myCollectionViewSource.Source = _queryProvider.GetQuery<Referee>();
            }
        }

        private ObjectQuery<Referee> GetRefereeQuery(int id)
        {
            var res = _entities.Referees.Where("x=>x.Id=={0}");
            return (ObjectQuery<Referee>)res;
        }
	}
}
