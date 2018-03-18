using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using Scraps.Model.Other;

namespace Scraps.UI.SettingsControls
{
	public partial class SettingsControl : INotifyPropertyChanged
	{
		private ScrapsSettings _settings;

		public ScrapsSettings Settings
		{
			get => _settings;
			set
			{
				_settings = value;
				RaisePropertyChanged(nameof(Settings));
			}
		}

		public SettingsControl()
		{
			InitializeComponent();

			Settings = new ScrapsSettings(((App) Application.Current).Context.Settings);

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
				config.ConnectionStrings.ConnectionStrings[1].ConnectionString = $"Data Source={dialog.FileName}";
				config.Save(ConfigurationSaveMode.Modified, true);
				ConfigurationManager.RefreshSection("connectionStrings");
			}
		}

		private void BrowseRootFolderPath(object sender, RoutedEventArgs e)
		{
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();

				if (result != System.Windows.Forms.DialogResult.OK)
					return;

				_settings.RootFoler = dialog.SelectedPath;
				RaisePropertyChanged(nameof(Settings));
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

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Raised when a property on this object has a new value.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">The property that has a new value.</param>
		protected virtual void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

		#endregion

	
	}
}
