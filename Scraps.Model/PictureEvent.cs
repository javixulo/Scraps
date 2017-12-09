using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class PictureEvent
    {
        public long Picture { get; set; }
        public long Event { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual Picture PictureNavigation { get; set; }
    }
}
