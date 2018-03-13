using Scraps.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Scraps.Lib;

namespace Scraps.UI.PicturesControls
{
	public partial class PicturesControl
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
	}

	public class Scrap
	{
		public Picture Picture { get; set; }
		public string Title => Picture?.FileName;
		public BitmapImage Image { get; set; }

		public Scrap(Picture picture)
		{
			Picture = picture;
			Image = ImageHelper.GetImage(picture.FullName, 200, 200);
		}
	}
}
