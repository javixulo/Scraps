using Scraps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

			if (Event == null)
				Event = new Event();
		}

		private void OnSaveClick(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;

			if ( Event.Id == default(long))
			{
				context = (Application.Current as App).Context;
				context.Event.Add(Event);
			}

			context.SaveChanges();
		}
	}
}
