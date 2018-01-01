using Scraps.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Scraps.UI.EventsControls
{
	public partial class EventTypesWindow : Window
	{
		public IEnumerable<EventType> SelectedItems => EventTypesControl.eventTypesDataGrid.SelectedItems.OfType<EventType>();

		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<EventType>), typeof(EventTypesWindow));

		public ObservableCollection<EventType> Items
		{
			get => EventTypesControl.Items;
			set => EventTypesControl.Items = value;
		}

		public EventTypesWindow()
		{
			InitializeComponent();
		}

		private void OKClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void CancelClick(object sender, RoutedEventArgs e)
		{
			EventTypesControl.eventTypesDataGrid.UnselectAll();
			Close();
		}
	}
}
