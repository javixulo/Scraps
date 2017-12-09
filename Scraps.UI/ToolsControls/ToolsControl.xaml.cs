using Scraps.Lib;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Scraps.UI.ToolsControls
{
	public partial class ToolsControl : UserControl
	{
		public ToolsControl()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			//	CollectionViewSource viewSource = ((CollectionViewSource)(FindResource("source")));

			//	MainWindow.Context.Subject.Load();

			//	viewSource.Source = MainWindow.Context.Subject.Local;
		}

		
	}
}
