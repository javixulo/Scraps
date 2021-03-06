﻿using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Scraps.Model;
using Scraps.UI.PicturesControls;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using Scraps.Lib;
using Scraps.UI.Windows;

namespace Scraps.UI.EventsControls
{
	public partial class EventControl
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
			}

			PicturesControl.Scraps = new ObservableCollection<Scrap>(Event.PictureEvent.Select(x => new Scrap(x.PictureNavigation)));
		}

		private void OnTypesPanelLoaded(object sender, RoutedEventArgs e)
		{
			var types = Event.EventTyped.Select(x => x.TypeNavigation);

			foreach (EventType type in types)
			{
				TypesPanel.AddTag(type.Name, type);
			}
		}

		private void OnAddPicturesClick(object sender, RoutedEventArgs e)
		{
			PicturesWindow window = new PicturesWindow();

			window.Scraps = new ObservableCollection<Scrap>((Application.Current as App).Context.Picture.Local.Except(PicturesControl.Scraps.Select(x => x.Picture)).Select(x => new Scrap(x)));

			window.ShowDialog();

			foreach (var scrap in window.SelectedItems)
			{
				PicturesControl.Scraps.Add(scrap);
			}
		}

		private void OnAddTypeClick(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;
			context.EventType.Load();

			EventTypesWindow window = new EventTypesWindow();
			window.Items = new ObservableCollection<EventType>(context.EventType.Local.Except(TypesPanel.Tags.Select(x => x.Object).OfType<EventType>()));
			window.ShowDialog();

			foreach (EventType type in window.SelectedItems)
			{
				TypesPanel.AddTag(type.Name, type);
			}
		}

		private void OnSaveClick(object sender, RoutedEventArgs e)
		{
			PicManagerContext context = (Application.Current as App).Context;

			foreach (var item in Event.EventTyped)
				context.EventTyped.Remove(item);

			context.SaveChanges();

			foreach (EventType item in TypesPanel.Tags.Select(x => x.Object).OfType<EventType>())
			{
				context.EventTyped.Add(new EventTyped { Event = Event.Id, Type = item.Name });
			}

			foreach (var item in Event.PictureEvent)
				context.PictureEvent.Remove(item);

			context.SaveChanges();

			if (PicturesControl.Scraps != null)
			{
				foreach (Picture item in PicturesControl.Scraps.Select(x => x.Picture))
				{
					context.PictureEvent.Add(new PictureEvent { Event = Event.Id, Picture = item.Id });
				}
			}

			if (Event.Id == default(long))
			{
				// it is a new one
				context = (Application.Current as App).Context;
				context.Event.Add(Event);
			}

			context.SaveChanges();
		}

		private void OnAddIconClick(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog
			{
				DefaultExt = ".png",
				Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All files (*.*)|*.*"
			};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				Event.Icon = File.ReadAllBytes(dlg.FileName);
			}
		}

		private void OnRenameClick(object sender, RoutedEventArgs e)
		{
			RenamingWindow renamingWindow = new  RenamingWindow(Event);
			renamingWindow.ShowDialog();
		}
	}
}
