﻿using Scraps.Model;
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
	}
}
