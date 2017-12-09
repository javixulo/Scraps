using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Scraps.UI.SettingsControls
{
	public partial class SettingsControl
	{
		public SettingsControl()
		{
			InitializeComponent();

			string connectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;

			DBFile.Text = connectionString.Substring(12);

			//IncludedFolders.ItemsSource = ((App)Application.Current).Context.IncludedFolder;

		}

		private void BrowseDBFile(object sender, RoutedEventArgs e)
		{
			var dialog = new Microsoft.Win32.OpenFileDialog();

			dialog.Filter = "DB Files (*.db;*.sqlite)|*.db;*.sqlite|All files (*.*)|*.*";

			bool? result = dialog.ShowDialog();

			if (result.HasValue && result.Value)
			{
				DBFile.Text = dialog.FileName;
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				config.ConnectionStrings.ConnectionStrings[1].ConnectionString = string.Format("Data Source={0}", dialog.FileName);
				config.Save(ConfigurationSaveMode.Modified, true);
				ConfigurationManager.RefreshSection("connectionStrings");
			}
		}

		private void Save(object sender, RoutedEventArgs e)
		{
			try
			{
				((App)Application.Current).Context.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ":" + Environment.NewLine + ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}
	}
}
