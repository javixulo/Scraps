using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Scraps.UI.Controls
{
	public partial class TagControl : UserControl
	{
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TagControl));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Brush), typeof(TagControl));

		public Brush Color
		{
			get => (Brush)GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}

		/// <summary>
		/// Put here anything you want :D
		/// </summary>
		public object Object { get; set; }

		public event RoutedEventHandler Deleted;

		public TagControl()
		{
			InitializeComponent();

			SetCurrentValue(ColorProperty, Brushes.ForestGreen);
		}

		private void OnCloseClick(object sender, RoutedEventArgs e)
		{
			Deleted?.Invoke(this, e);
		}
	}

	public class TagsCollection : ObservableCollection<TagControl>
	{
		public event EventHandler OnAdd;

		public new void Add(TagControl item)
		{
			item.Deleted += OnDeletedByUser;

			OnAdd?.Invoke(this, null);

			base.Add(item);
		}

		private void OnDeletedByUser(object sender, RoutedEventArgs routedEventArgs)
		{
			TagControl card = (TagControl)sender;

			Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => Remove(card)));

			card.Deleted -= OnDeletedByUser;
		}
	}
}
