using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class PersonInPicture
    {
        public long Person { get; set; }
        public long Picture { get; set; }

        public virtual Person PersonNavigation { get; set; }
        public virtual Picture PictureNavigation { get; set; }
    }
}
