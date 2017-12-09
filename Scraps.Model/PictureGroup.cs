using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class PictureGroup
    {
        public long Picture { get; set; }
        public long Group { get; set; }

        public virtual Group GroupNavigation { get; set; }
        public virtual Picture PictureNavigation { get; set; }
    }
}
