using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class Group
    {
        public Group()
        {
            PictureGroup = new HashSet<PictureGroup>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PictureGroup> PictureGroup { get; set; }
    }
}
