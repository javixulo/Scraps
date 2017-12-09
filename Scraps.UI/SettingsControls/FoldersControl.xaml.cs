using Scraps.Model;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Scraps.UI.SettingsControls
{
	public partial class FoldersControl
	{
		public FoldersControl()
		{
			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			CollectionViewSource includedSource = (CollectionViewSource)FindResource("includedSource");
			CollectionViewSource excludedSource = (CollectionViewSource)FindResource("excludedSource");
			includedSource.Source = ((App)Application.Current).Context.IncludedFolder.ToList();
			excludedSource.Source = ((App)Application.Current).Context.ExcludedFolder.ToList();
		}

		private void AddIncluded(object sender, RoutedEventArgs e)
		{
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();

				if (result != System.Windows.Forms.DialogResult.OK)
					return;

				((App)Application.Current).Context.IncludedFolder.Add(new IncludedFolder { Path = dialog.SelectedPath });
				((App)Application.Current).Context.SaveChanges();

				OnLoaded(this, null);
			}
		}

		private void AddExcluded(object sender, RoutedEventArgs e)
		{
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();

				if (result != System.Windows.Forms.DialogResult.OK)
					return;

				((App)Application.Current).Context.ExcludedFolder.Add(new ExcludedFolder { Path = dialog.SelectedPath });
				((App)Application.Current).Context.SaveChanges();

				OnLoaded(this, null);
			}
		}

		private void RemoveIncluded(object sender, RoutedEventArgs e)
		{
			var selected = IncludedFolders.SelectedItem as IncludedFolder;
			if (selected == null)
				return;

			((App)Application.Current).Context.IncludedFolder.Remove(selected);
			((App)Application.Current).Context.SaveChanges();

			OnLoaded(this, null);
		}

		private void RemoveExcluded(object sender, RoutedEventArgs e)
		{
			var selected = ExcludedFolders.SelectedItem as ExcludedFolder;
			if (selected == null)
				return;

			((App)Application.Current).Context.ExcludedFolder.Remove(selected);
			((App)Application.Current).Context.SaveChanges();

			OnLoaded(this, null);
		}
	}
}
