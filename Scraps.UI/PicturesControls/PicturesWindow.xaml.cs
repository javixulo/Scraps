using Scraps.Model;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;

namespace Scraps.UI.PicturesControls
{
	public partial class PicturesWindow : Window
	{
		public static readonly DependencyProperty PicturesProperty = DependencyProperty.Register("Pictures", typeof(ObservableCollection<Picture>), typeof(PicturesWindow));

		public ObservableCollection<Picture> Pictures
		{
			get => PicturesControl.Pictures;
			set => PicturesControl.Pictures = value;
		}

		public IEnumerable<Picture> SelectedItems { get => PicturesControl.picturesDataGrid.SelectedItems.OfType<Picture>(); }

		public PicturesWindow()
		{
			InitializeComponent();
		}

		private void OKClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void CancelClick(object sender, RoutedEventArgs e)
		{
			PicturesControl.picturesDataGrid.UnselectAll();
			Close();
		}

		private void OnPicturesControlLoaded(object sender, RoutedEventArgs e)
		{
		}
	}
}
