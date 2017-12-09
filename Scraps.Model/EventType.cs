using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class EventType
    {
        public EventType()
        {
            EventTyped = new HashSet<EventTyped>();
        }

        public string Name { get; set; }

        public virtual ICollection<EventTyped> EventTyped { get; set; }
    }
}
