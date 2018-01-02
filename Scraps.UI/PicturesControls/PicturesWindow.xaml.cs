using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;

namespace Scraps.UI.PicturesControls
{
	public partial class PicturesWindow : Window
	{
		public static readonly DependencyProperty PicturesProperty = DependencyProperty.Register("Scraps", typeof(ObservableCollection<Scrap>), typeof(PicturesWindow));

		public ObservableCollection<Scrap> Scraps
		{
			get => PicturesControl.Scraps;
			set => PicturesControl.Scraps = value;
		}

		public IEnumerable<Scrap> SelectedItems { get => PicturesControl.picturesDataGrid.SelectedItems.OfType<Scrap>(); }

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
