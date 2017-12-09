using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class PictureTag
    {
        public long Picture { get; set; }
        public string Tag { get; set; }

        public virtual Picture PictureNavigation { get; set; }
        public virtual Tag TagNavigation { get; set; }
    }
}
