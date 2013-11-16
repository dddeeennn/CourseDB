using System.Windows;
using System.Windows.Controls;
using FootballSchool.Kernel;

namespace FootballSchool.Pages.Main
{
    /// <summary>
    /// Interaction logic for TableStats.xaml
    /// </summary>
    public partial class TableStats : UserControl
    {
        public TableStats()
        {
            InitializeComponent();
            entities = EntityProvider.Entities;
        }

        private fscEntities entities;

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
           // dgTableStat.ItemsSource
              //var pt  = from t in entities.Teams
              //          join pt in entities.PlayerInTeam
              //          on t.PlayerInTeam equals pt.Id
              //          select pt;


        }
    }
}
