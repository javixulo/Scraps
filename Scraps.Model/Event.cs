using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scraps.Model
{
    public partial class Event
    {
        public Event()
        {
            EventTyped = new HashSet<EventTyped>();
            PictureEvent = new HashSet<PictureEvent>();
        }

		[Key]
		public long Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long? Location { get; set; }
        public byte[] Icon { get; set; }

		public virtual ICollection<EventTyped> EventTyped { get; set; }
        public virtual ICollection<PictureEvent> PictureEvent { get; set; }


    }
}
