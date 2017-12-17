using Scraps.Model;
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

		private void OnSaveClick(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;
			
			foreach (var item in Event.EventTyped)
				context.EventTyped.Remove(item);

			context.SaveChanges();
			
			foreach(EventType item in TypesGrid.eventTypesDataGrid.SelectedItems)
			{
				context.EventTyped.Add(new EventTyped { Event = Event.Id, Type = item.Name });
			}
			
			if ( Event.Id == default(long))
			{
				// it is a new one
				context = (Application.Current as App).Context;
				context.Event.Add(Event);
			}

			context.SaveChanges();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			if (Event == null)
			{
				Event = new Event();
				return;
			}
		}

		private void OnTypesGridLoaded(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;

			var types = Event.EventTyped.Select(x => x.TypeNavigation);
			
			TypesGrid.SetSelectedTypes(types);
		}
	}
}
