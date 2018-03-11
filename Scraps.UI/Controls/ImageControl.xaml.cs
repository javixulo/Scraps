using System;
using System.ComponentModel;
using System.Windows;
using Scraps.Lib;

namespace Scraps.UI.Controls
{
	public partial class ImageControl : INotifyPropertyChanged
	{

		public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(string), typeof(ImageControl),
			new FrameworkPropertyMetadata(OnImagePathChanged));

		public string ImagePath
		{
			get => (string)GetValue(ImagePathProperty);
			set => SetValue(ImagePathProperty, value);
		}

		private static void OnImagePathChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			var imageControl = (ImageControl)dependencyObject;

			var bmImg = ImageHelper.GetImage(eventArgs.NewValue.ToString(), Convert.ToInt32(imageControl.Width), Convert.ToInt32(imageControl.Height));

			imageControl.Image.Source = bmImg;
		}


		public ImageControl()
		{
			InitializeComponent();
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}


		#endregion
	}


}
