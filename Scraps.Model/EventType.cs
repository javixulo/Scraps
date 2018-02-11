using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scraps.Model
{
    public partial class EventType
    {
        public EventType()
        {
            EventTyped = new HashSet<EventTyped>();
        }

		[Key]
        public string Name { get; set; }

        public virtual ICollection<EventTyped> EventTyped { get; set; }
    }
}
