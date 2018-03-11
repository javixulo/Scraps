using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Scraps.Model;

namespace Scraps.UI.EventsControls
{
	public partial class EventTypesControl : UserControl
	{
		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<EventType>), typeof(EventTypesControl));

		public ObservableCollection<EventType> Items
		{
			get => (ObservableCollection<EventType>)GetValue(ItemsProperty);
			set => SetValue(ItemsProperty, value);
		}

		public EventTypesControl()
		{
			InitializeComponent();
		}

		public void SetSelectedTypes(IEnumerable<EventType> types)
		{
			eventTypesDataGrid.SelectedItems.Clear();

			foreach (var item in types)
			{
				eventTypesDataGrid.SelectedItems.Add(item);
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void OnDelete(object sender, RoutedEventArgs e)
		{
			var selected = eventTypesDataGrid.SelectedValue as EventType;

			if (selected == null)
				return;

			(Application.Current as App).Context.EventType.Remove(selected);
			(Application.Current as App).Context.SaveChanges();
		}

		private void OnAddClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(NameBox.Text))
				return;

			PicManagerContext context = (Application.Current as App).Context;

			if (context.EventType.Any(x => x.Name.Equals(NameBox.Text, StringComparison.InvariantCultureIgnoreCase)))
				return;

			context.EventType.Add(new EventType { Name = NameBox.Text });
			context.SaveChanges();
			NameBox.Text = string.Empty;
		}

	}
}
