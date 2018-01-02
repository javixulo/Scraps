using Scraps.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Scraps.UI.EventsControls
{
    public partial class EventsControl : UserControl
    {
		public static readonly DependencyProperty EventsProperty = DependencyProperty.Register("Events", typeof(ObservableCollection<Event>), typeof(EventsControl));

		public ObservableCollection<Event> Events
		{
			get => (ObservableCollection<Event>)GetValue(EventsProperty);
			set => SetValue(EventsProperty, value);
		}

		public EventsControl()
        {
            InitializeComponent();
        }

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
		}

		private void OnDelete(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Are you sure?", "Scraps", MessageBoxButton.OKCancel) != MessageBoxResult.Yes)
				return;

			var selected = eventsDataGrid.SelectedValue as Event;

			if (selected == null)
				return;

			(Application.Current as App).Context.Event.Remove(selected);
			(Application.Current as App).Context.SaveChanges();
		}

		private void OnAddClick(object sender, RoutedEventArgs e)
		{
			EventWindow window = new EventWindow();
			window.ShowDialog();
		}

		private void EventDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Event item = eventsDataGrid.SelectedValue as Event;

			if (item == null)
				return;

			EventWindow window = new EventWindow { Event = item };
			window.ShowDialog();
		}

		private void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
			{
				EventDoubleClick(sender, e);
			}
		}
	}
}
