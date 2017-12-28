using Scraps.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scraps.UI.PicturesControls
{
	public partial class PicturesControl : UserControl
	{
		public static readonly DependencyProperty PicturesProperty = DependencyProperty.Register("Pictures", typeof(ObservableCollection<Picture>), typeof(PicturesControl));

		public ObservableCollection<Picture> Pictures
		{
			get => (ObservableCollection<Picture>)GetValue(PicturesProperty);
			set => SetValue(PicturesProperty, value);
		}

		public static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(string), typeof(PicturesControl));

		public string SelectedImage
		{
			get => (string)GetValue(SelectedImageProperty);
			set => SetValue(SelectedImageProperty, value);
		}

		public PicturesControl()
		{
			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (picturesDataGrid.SelectedItem == null)
				return;

			SetCurrentValue(SelectedImageProperty, ((Picture)picturesDataGrid.SelectedItem).FullName);
		}
	}
}
