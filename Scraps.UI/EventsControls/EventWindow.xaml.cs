using Scraps.Model;
using System.Windows;

namespace Scraps.UI.EventsControls
{
	public partial class EventWindow : Window
	{
		public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(Event), typeof(EventWindow));

		public Event Event
		{
			get => EventControl.Event;
			set => EventControl.Event = value;
		}

		public EventWindow()
		{
			InitializeComponent();
		}

        private void EventControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
