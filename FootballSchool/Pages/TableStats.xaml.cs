using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballSchool.Pages
{
    /// <summary>
    /// Interaction logic for TableStats.xaml
    /// </summary>
    public partial class TableStats : UserControl
    {
        public TableStats()
        {
            InitializeComponent();
            entities = new fscEntities();
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
