using Scraps.Model;
using Scraps.UI.Controls;
using Scraps.UI.PicturesControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scraps.UI.EventsControls
{
	public partial class EventControl : UserControl
	{
		public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(Event), typeof(EventControl));

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
			PicManagerContext context = (Application.Current as App).Context;

			var types = Event.EventTyped.Select(x => x.TypeNavigation);
			
			TypesGrid.SetSelectedTypes(types);
		}

		private void OnAddPicturesClick(object sender, RoutedEventArgs e)
		{
			PicturesWindow window = new PicturesWindow();
			
			window.Pictures = new ObservableCollection<Picture>((Application.Current as App).Context.Picture.Local.Except(PicturesControl.Pictures));

			window.ShowDialog();

			foreach (Picture picture in window.SelectedItems)
			{
				PicturesControl.Pictures.Add(picture);
			}
		}

		private void OnSaveClick(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;

			foreach (var item in Event.EventTyped)
				context.EventTyped.Remove(item);

			context.SaveChanges();

			foreach (EventType item in TypesGrid.eventTypesDataGrid.SelectedItems)
			{
				context.EventTyped.Add(new EventTyped { Event = Event.Id, Type = item.Name });
			}


			foreach (var item in Event.PictureEvent)
				context.PictureEvent.Remove(item);

			context.SaveChanges();

			foreach (Picture item in PicturesControl.Pictures)
			{
				context.PictureEvent.Add(new PictureEvent { Event = Event.Id, Picture = item.Id });
			}

			if (Event.Id == default(long))
			{
				// it is a new one
				context = (Application.Current as App).Context;
				context.Event.Add(Event);
			}

			context.SaveChanges();
		}
	}
}
