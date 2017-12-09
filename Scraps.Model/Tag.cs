using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class Tag
    {
        public Tag()
        {
            PictureTag = new HashSet<PictureTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<PictureTag> PictureTag { get; set; }
    }
}
