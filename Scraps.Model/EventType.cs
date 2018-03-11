using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scraps.Model
{
    public partial class EventType : IEquatable<EventType>
	{
        public EventType()
        {
            EventTyped = new HashSet<EventTyped>();
        }

		[Key]
        public string Name { get; set; }

        public virtual ICollection<EventTyped> EventTyped { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as EventType);
		}

		public bool Equals(EventType other)
		{
			return other != null &&
				   Name == other.Name;
		}

		public override int GetHashCode()
		{
			return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
		}
	}
}
