using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class EventTyped
    {
        public long Event { get; set; }
        public string Type { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual EventType TypeNavigation { get; set; }
    }
}
