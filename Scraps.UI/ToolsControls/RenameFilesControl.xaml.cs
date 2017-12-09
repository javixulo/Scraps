using Scraps.Lib;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scraps.UI.ToolsControls
{
	public partial class RenameFilesControl : UserControl
	{
		public RenameFilesControl()
		{
			InitializeComponent();
		}

		private void BrowseFolder(object sender, RoutedEventArgs e)
		{
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();

				if (result != System.Windows.Forms.DialogResult.OK)
					return;

				Folder.Text = dialog.SelectedPath;
			}
		}

		private void GoRename(object sender, RoutedEventArgs e)
		{
			var folder = new DirectoryInfo(Folder.Text);

			var fileNames = folder.GetFiles().Select(x => x.Name).ToList();

			var renamedFileNames = FileHelper.GetFileNames(fileNames, Prefix.Text);

			if (string.IsNullOrEmpty(Folder.Text) || string.IsNullOrEmpty(Prefix.Text))
			{
				MessageBox.Show("Need folder and prefeix", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}

			if (fileNames.Any(x => renamedFileNames.Keys.Contains(x)))
			{
				MessageBox.Show("Filename already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
				return;
			}

			var confirmation = MessageBox.Show("Are you sure you want to continue?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

			if (confirmation != MessageBoxResult.OK)
				return;

			foreach (var newFileName in renamedFileNames)
			{
				File.Move(Path.Combine(Folder.Text, newFileName.Key), Path.Combine(Folder.Text, newFileName.Value));
			}
		}
	}
}
