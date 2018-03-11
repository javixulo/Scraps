using Scraps.Lib.Extensions;
using Scraps.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Scraps.UI.PicturesControls
{
	public partial class PicturesControl : UserControl
	{
		public static readonly DependencyProperty ScrapsProperty = DependencyProperty.Register("Scraps", typeof(ObservableCollection<Scrap>), typeof(PicturesControl));

		public ObservableCollection<Scrap> Scraps
		{
			get => (ObservableCollection<Scrap>)GetValue(ScrapsProperty);
			set => SetValue(ScrapsProperty, value);
		}

		public static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(Scrap), typeof(PicturesControl));

		public Scrap SelectedImage
		{
			get => (Scrap)GetValue(SelectedImageProperty);
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

			SetCurrentValue(SelectedImageProperty, ((Scrap)picturesDataGrid.SelectedItem));
		}
	}

	public class Scrap
	{
		public Picture Picture { get; set; }
		public string Title => Picture?.FileName;
		public BitmapImage Image { get; set; }

		public Scrap(Picture picture)
		{
			Picture = picture;
			Image = picture.GetImage(200, 200);
		}
	}
}
