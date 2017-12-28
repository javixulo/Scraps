using Scraps.Model;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Scraps.UI.EventsControls
{
    public partial class EventsControl : UserControl
    {
        public EventsControl()
        {
            InitializeComponent();
        }

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			CollectionViewSource viewSource = ((CollectionViewSource)(FindResource("source")));

			PicManagerContext context = (Application.Current as App).Context;

			context.EventTyped.Load();
			context.EventType.Load();
			context.Picture.Load();
			context.PictureEvent.Load();

			context.Event.Load();

			viewSource.Source = context.Event.Local.ToObservableCollection();
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
	}
}
