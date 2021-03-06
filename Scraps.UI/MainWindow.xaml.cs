﻿using Scraps.Lib;
using Scraps.Model;
using Scraps.UI.PicturesControls;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;

namespace Scraps.UI
{
	public partial class MainWindow
	{
		private static readonly string[] SearchPatterns = { ".png", ".jpg", ".jpeg" };

		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnPicturesControlLoaded(object sender, RoutedEventArgs e)
		{
			IFileSystem fileSystem = new FileSystem();

			PicManagerContext context = (Application.Current as App).Context;
			context.Picture.Load();

			bool changes = false;

			// look for new pictures in the included folders location
			foreach (IncludedFolder folder in context.IncludedFolder)
			{
				var files = FileHelper.GetFiles(fileSystem, folder.Path, SearchPatterns, context.ExcludedFolder.Select(x => x.Path));

				foreach (string file in files)
				{
					if (!context.Picture.Any(x => x.IsSameFile(file)))
					{
						ImageProperties properties = new ImageProperties(file);

						context.Picture.Add(new Picture
						{
							Folder = Path.GetDirectoryName(file),
							FileName = Path.GetFileName(file),
							Date = properties.TakenDate
						});

						changes = true;
					}
				}
			}

			if (changes) context.SaveChanges();

			PicturesControl.Scraps = new ObservableCollection<Scrap>(context.Picture.Local.Select(x => new Scrap(x)));
		}

		private void OnEventsControlLoaded(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;

			context.EventTyped.Load();
			context.EventType.Load();
			context.Picture.Load();
			context.PictureEvent.Load();

			context.Event.Load();

			EventsControl.Events = context.Event.Local.ToObservableCollection();
		}
	}
}
