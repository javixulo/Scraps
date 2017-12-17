using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Scraps.Lib;
using Scraps.Model;

namespace Scraps.UI.Controls
{
	public partial class FileListControl : INotifyPropertyChanged
	{
		private static readonly string[] SearchPatterns = { ".png", ".jpg", ".jpeg" };
		
		public static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(string), typeof(FileListControl));

		public string SelectedImage
		{
			get => (string) GetValue(SelectedImageProperty);
			set => SetValue(SelectedImageProperty, value);
		}

		public FileListControl()
		{
			InitializeComponent();

			var viewSource = (CollectionViewSource)FindResource("source");
			var list = new List<FileDetails>();
			FileList.SelectionChanged += FileListOnSelectionChanged;

			IFileSystem fileSystem = new FileSystem();

			PicManagerContext picManagerContext = (Application.Current as App).Context;


			foreach (IncludedFolder folder in picManagerContext.IncludedFolder)
			{
				var files = FileHelper.GetFiles(fileSystem, folder.Path, SearchPatterns, picManagerContext.ExcludedFolder.Select(x => x.Path));

				list.AddRange(files.Select(file => new FileDetails(file)));
			}

			viewSource.Source = list;
		}

		private void FileListOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
		{
			//	SelectedImage = ((FileDetails)FileList.SelectedItem).FullName;
			if (FileList.SelectedItem == null)
				return;

			SetCurrentValue(SelectedImageProperty, ((FileDetails)FileList.SelectedItem).FullName);

		//	NotifyPropertyChanged(nameof(SelectedImage));
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		
		#endregion
	}

	public class FileDetails
	{
		public string FullName { get; set; }

		public string FileName => System.IO.Path.GetFileName(FullName);
		public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(FullName);
		public string Folder => System.IO.Path.GetDirectoryName(FullName);
		public string FullPath => System.IO.Path.GetFullPath(FullName);
		public string Extension => System.IO.Path.GetExtension(FullName);

		public FileDetails(string fileName)
		{
			FullName = fileName;
		}
	}
}
