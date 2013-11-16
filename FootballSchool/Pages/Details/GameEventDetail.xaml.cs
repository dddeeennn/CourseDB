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

namespace FootballSchool.Pages.Details
{
	/// <summary>
	/// Interaction logic for GameEventDetail.xaml
	/// </summary>
	public partial class GameEventDetail : UserControl
	{
		private GameEvent gameEvent;

		public GameEventDetail()
		{
			InitializeComponent();
		}

		public GameEventDetail(GameEvent gameEvent)
		{
			// TODO: Complete member initialization
			this.gameEvent = gameEvent;
		}

		
	}
}
