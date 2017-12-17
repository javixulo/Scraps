using Scraps.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public ObservableCollection<EventType> EventTypes { get; set; }
		
		public EventControl()
		{
			InitializeComponent();

			TypesGrid.Loaded += OnTypesGridLoaded;

			
		}

		private void OnSaveClick(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;

			var selected = TypesGrid.eventTypesDataGrid.SelectedItems;

			//foreach (var item in Event.EventTyped)
			//{
			//	context.EventTyped.Remove(item);
			//}

			//Event.EventTyped.Clear();

			foreach (var item in Event.EventTyped)
				context.EventTyped.Remove(item);

			context.SaveChanges();
			
			foreach(EventType item in selected)
			{
				//if ( !context.EventTyped.Any(x=> x.Event == Event.Id && x.Type.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)))
					context.EventTyped.Add(new EventTyped { Event = Event.Id, Type = item.Name });
			}

			//foreach(var item in context.EventType.Where(x=> !selected.Contains(x)))
			//{
			//	var remove = context.EventTyped.FirstOrDefault(x=> x.Event == Event.Id && x.Type.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)))
			//		context.EventTyped.Remove(remove);
			//}
			
			if ( Event.Id == default(long))
			{
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

			EventTypes = new ObservableCollection<EventType>(types);

			TypesGrid.SetSelectedTypes(EventTypes);
		}
	}
}
