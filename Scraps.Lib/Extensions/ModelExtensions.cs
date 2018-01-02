﻿using Scraps.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Scraps.Lib.Extensions
{
	public static class ModelExtensions
	{
		public static BitmapImage GetImage(this Picture picture, int width = 300, int height = 300)
		{
			return ImageHelper.GetImage(picture.FullName, width, height);
		}
	}
}
