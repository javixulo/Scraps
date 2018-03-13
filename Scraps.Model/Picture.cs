using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Scraps.Model
{
	public partial class Picture : IEquatable<Picture>
	{
		public Picture()
		{
			PersonInPicture = new HashSet<PersonInPicture>();
			PictureEvent = new HashSet<PictureEvent>();
			PictureGroup = new HashSet<PictureGroup>();
			PictureTag = new HashSet<PictureTag>();
		}

		public long Id { get; set; }
		public string Folder { get; set; }
		public string FileName { get; set; }
		public long? Location { get; set; }
		public DateTime? Date { get; set; }

		[NotMapped]
		public string FullName => Path.Combine(Folder, FileName);

		public virtual ICollection<PersonInPicture> PersonInPicture { get; set; }
		public virtual ICollection<PictureEvent> PictureEvent { get; set; }
		public virtual ICollection<PictureGroup> PictureGroup { get; set; }
		public virtual ICollection<PictureTag> PictureTag { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as Picture);
		}

		public bool Equals(Picture other)
		{
			return other != null &&
				   Id == other.Id;
		}

		public override int GetHashCode()
		{
			return 2108858624 + Id.GetHashCode();
		}

		public bool IsSameFile(string file)
		{
			return FullName.Equals(file, StringComparison.InvariantCultureIgnoreCase);
		}

	}
}
