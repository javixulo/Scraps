using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class Picture
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

        public virtual ICollection<PersonInPicture> PersonInPicture { get; set; }
        public virtual ICollection<PictureEvent> PictureEvent { get; set; }
        public virtual ICollection<PictureGroup> PictureGroup { get; set; }
        public virtual ICollection<PictureTag> PictureTag { get; set; }
    }
}
