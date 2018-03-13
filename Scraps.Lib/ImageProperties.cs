using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Scraps.Lib
{
	public class ImageProperties
	{
		private static Regex r = new Regex(":");

		public ImageProperties(string file)
		{
			using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
			using (Image myImage = Image.FromStream(fs, false, false))
			{
				if (!myImage.PropertyIdList.Any(x => x == 36867))
					return;

				PropertyItem propItem = myImage.GetPropertyItem(36867);
				string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);

				TakenDate = DateTime.Parse(dateTaken).ToUniversalTime();
			}
		}

		public DateTime? TakenDate { get; set; }
	}
}
