using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;

namespace Scraps.Lib
{
	public static class ImageHelper
	{
		public const int OrientationId = 0x0112;
		
		public static ExifOrientations ImageOrientation(Image img)
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

		public static BitmapImage GetImage(string file, int maxWidth,int maxHeight )
		{
			Image image = Image.FromFile(file);

			var (width, height) = GetNewSize(image, maxWidth, maxHeight);

			image = resizeImage(image, maxWidth, maxHeight);

		//	image = image.GetThumbnailImage((int)width, (int)height, null, IntPtr.Zero);

			Rotate(image);

			var bitMap = new BitmapImage();
			using (MemoryStream memStream2 = new MemoryStream())
			{
				image.Save(memStream2, ImageFormat.Png);
				memStream2.Position = 0;
				bitMap.BeginInit();
				bitMap.CacheOption = BitmapCacheOption.OnLoad;
				bitMap.UriSource = null;
				bitMap.DecodePixelWidth = width;
				bitMap.DecodePixelHeight = height;
				bitMap.StreamSource = memStream2;
				bitMap.EndInit();

			}
			return bitMap;
		}

		public static void Rotate(Image image)
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

		static float imageResolution = 72;

		//set the compression level. higher compression = better quality = bigger images
		static long compressionLevel = 80L;

		public static Image resizeImage(Image image, int width, int height)
		{
			//first we check if the image needs rotating (eg phone held vertical when taking a picture for example)
			foreach (var prop in image.PropertyItems)
			{
				if (prop.Id == 0x0112)
				{
					int orientationValue = image.GetPropertyItem(prop.Id).Value[0];
					RotateFlipType rotateFlipType = getRotateFlipType(orientationValue);
					image.RotateFlip(rotateFlipType);
					break;
				}
			}
			
			//apply the padding to make a square image

			//start the resize with a new image
			Bitmap newImage = new Bitmap(width, height);

			//set the new resolution
			newImage.SetResolution(imageResolution, imageResolution);

			//start the resizing
			using (var graphics = Graphics.FromImage(newImage))
			{
				//set some encoding specs
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				graphics.DrawImage(image, 0, 0, width, height);
			}

			//save the image to a memorystream to apply the compression level
			using (MemoryStream ms = new MemoryStream())
			{
				EncoderParameters encoderParameters = new EncoderParameters(1);
				encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, compressionLevel);

				newImage.Save(ms, getEncoderInfo("image/jpeg"), encoderParameters);

				//save the image as byte array here if you want the return type to be a Byte Array instead of Image
				//byte[] imageAsByteArray = ms.ToArray();
			}

			//return the image
			return newImage;
		}

		public static (int width, int height) GetNewSize(Image image, int width, int height)
		{
			int newWidth, newHeight;

			if (image.Width > width || image.Height > height)
			{
				double ratioX = (double)width / image.Width;
				double ratioY = (double)height / image.Height;
				double ratio = Math.Min(ratioX, ratioY);

				newWidth = (int)(image.Width * ratio);
				newHeight = (int)(image.Height * ratio);
			}
			else
			{
				newWidth = image.Width;
				newHeight = image.Height;
			}

			return (newWidth, newHeight);
		}
		
		//=== get encoder info
		private static ImageCodecInfo getEncoderInfo(string mimeType)
		{
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

			for (int j = 0; j < encoders.Length; ++j)
			{
				if (encoders[j].MimeType.ToLower() == mimeType.ToLower())
				{
					return encoders[j];
				}
			}

			return null;
		}


		//=== determine image rotation
		private static RotateFlipType getRotateFlipType(int rotateValue)
		{
			RotateFlipType flipType = RotateFlipType.RotateNoneFlipNone;

			switch (rotateValue)
			{
				case 1:
					flipType = RotateFlipType.RotateNoneFlipNone;
					break;
				case 2:
					flipType = RotateFlipType.RotateNoneFlipX;
					break;
				case 3:
					flipType = RotateFlipType.Rotate180FlipNone;
					break;
				case 4:
					flipType = RotateFlipType.Rotate180FlipX;
					break;
				case 5:
					flipType = RotateFlipType.Rotate90FlipX;
					break;
				case 6:
					flipType = RotateFlipType.Rotate90FlipNone;
					break;
				case 7:
					flipType = RotateFlipType.Rotate270FlipX;
					break;
				case 8:
					flipType = RotateFlipType.Rotate270FlipNone;
					break;
				default:
					flipType = RotateFlipType.RotateNoneFlipNone;
					break;
			}

			return flipType;
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
}
