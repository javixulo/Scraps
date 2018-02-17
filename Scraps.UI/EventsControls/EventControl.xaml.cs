using Microsoft.Win32;
using Scraps.Model;
using Scraps.UI.PicturesControls;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scraps.UI.EventsControls
{
	public partial class EventControl : UserControl
	{
		public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(Event), typeof(EventControl));
		public PicManagerContext Context = (Application.Current as App).Context;

		public Event Event
		{
			get => (Event)GetValue(EventProperty);
			set => SetValue(EventProperty, value);
		}

		public EventControl()
		{
			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			if (Event == null)
			{
				Event = new Event();
				return;
			}

			PicturesControl.Pictures = new ObservableCollection<Picture>();
			
			foreach(var pictureEvent in Event.PictureEvent.Select(x=>x.PictureNavigation))
			{
				PicturesControl.Pictures.Add(pictureEvent);
			}
			
		}

		private void OnTypesGridLoaded(object sender, RoutedEventArgs e)
		{
			var types = Event.EventTyped.Select(x => x.TypeNavigation);
			
			TypesGrid.SetSelectedTypes(types);
		}

		private void OnAddPicturesClick(object sender, RoutedEventArgs e)
		{
			PicturesWindow window = new PicturesWindow();
			PicturesControl.Pictures = new ObservableCollection<Picture>();
			string imagesBaseDirectory = ConfigurationManager.AppSettings["ImagesBaseDirectory"];

			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = ConfigurationManager.AppSettings["AllowedImageTypes"],
				FilterIndex = 1,
				Multiselect = true
			};

			bool? userClickedOK = openFileDialog.ShowDialog();

			if (userClickedOK == true)
			{
				foreach (string fileName in openFileDialog.FileNames)
				{
					File.Copy(fileName, imagesBaseDirectory + Path.GetFileName(fileName), true);
					Picture picture = new Picture
					{
						FileName = Path.GetFileName(fileName),
						Folder = imagesBaseDirectory
					};
					PicturesControl.Pictures.Add(picture);
					Context.Picture.Add(picture);
				}
			}
		}

		private void OnSaveClick(object sender, RoutedEventArgs e)
		{
			if (Event.Id == default(long))
			{
				Context.Event.Add(Event);
			}

			foreach (var item in Event.EventTyped)
				Context.EventTyped.Remove(item);

			Context.SaveChanges();

			foreach (EventType item in TypesGrid.eventTypesDataGrid.SelectedItems)
			{
				Context.EventTyped.Add(new EventTyped { Event = Event.Id, Type = item.Name });
			}

			foreach (var item in Event.PictureEvent)
				Context.PictureEvent.Remove(item);

			Context.SaveChanges();

			foreach (Picture item in PicturesControl.Pictures)
			{
				Context.PictureEvent.Add(new PictureEvent { Event = Event.Id, Picture = item.Id });
			}

			Context.SaveChanges();

			Window.GetWindow(this).Close();
		}
	}
}
