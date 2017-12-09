using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Scraps.Lib
{
	public static class ImageHelper
	{
		public const int OrientationId = 0x0112;


		public static ExifOrientations ImageOrientation(System.Drawing.Image img)
		{
			// Get the index of the orientation property.
			int orientationIndex = Array.IndexOf(img.PropertyIdList, OrientationId);

			// If there is no such property, return Unknown.
			if (orientationIndex < 0) return ExifOrientations.Unknown;

			// Return the orientation value.
			PropertyItem item = img.GetPropertyItem(OrientationId);
			var javi = item.Value[0];
			return (ExifOrientations)item.Value[0];
		}

		public static BitmapImage GetImage(string file)
		{
			Image image = Image.FromFile(file);
			
			Rotate(image);


			var bitMap = new BitmapImage();
			using (MemoryStream memStream2 = new MemoryStream())
			{
				image.Save(memStream2, ImageFormat.Png);
				memStream2.Position = 0;
				bitMap.BeginInit();
				bitMap.CacheOption = BitmapCacheOption.OnLoad;
				bitMap.UriSource = null;
				
				bitMap.StreamSource = memStream2;
				bitMap.EndInit();
				
			}
			return bitMap;
		}

		public static ImageSource GetImage(string file, double height, double width)
		{
			Image image = Image.FromFile(file);

			
			Rotate(image);
			
			var bitMap = new BitmapImage();
			using (MemoryStream memStream2 = new MemoryStream())
			{
				image.Save(memStream2, ImageFormat.Png);
				memStream2.Position = 0;
				bitMap.BeginInit();
				bitMap.CacheOption = BitmapCacheOption.OnLoad;
				bitMap.UriSource = null;
				bitMap.DecodePixelWidth = (int)Math.Round(width);
				bitMap.DecodePixelHeight = (int)Math.Round(height);
				bitMap.StreamSource = memStream2;
				bitMap.EndInit();

			}
			return bitMap;
		}

		private static void Rotate(Image image)
		{
			switch (ImageOrientation(image))
			{
				//case ExifOrientations.TopLeft:
				//	break;
				//case ExifOrientations.TopRight:

				//	gr.ScaleTransform(-1, 1);
				//	break;
				//case ExifOrientations.BottomRight:
				//	gr.RotateTransform(180);
				//	break;
				//case ExifOrientations.BottomLeft:
				//	gr.ScaleTransform(1, -1);
				//	break;
				case ExifOrientations.LeftTop:
					//	bitMap.Rotation = Rotation.Rotate90;
					//gr.RotateTransform(90);
					//gr.ScaleTransform(-1, 1, MatrixOrder.Append);
					break;
				case ExifOrientations.RightTop:
					//	bitMap.Rotation = Rotation.Rotate270;

					//gr.RotateTransform(-90);
					break;
				case ExifOrientations.RightBottom:
					//	bitMap.Rotation = Rotation.Rotate90;

					//gr.RotateTransform(90);
					//gr.ScaleTransform(1, -1, MatrixOrder.Append);
					break;
				case ExifOrientations.LeftBottom:
					//bitMap.Rotation = Rotation.Rotate90;

					image.RotateFlip(RotateFlipType.Rotate270FlipNone);

					//gr.RotateTransform(90);
					break;
			}
		}
	}

	public enum ExifOrientations
	{
		Unknown = 0,
		TopLeft = 1,
		TopRight = 2,
		BottomRight = 3,
		BottomLeft = 4,
		LeftTop = 5,
		RightTop = 6,
		RightBottom = 7,
		LeftBottom = 8,
	}
}
