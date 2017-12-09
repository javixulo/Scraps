using System;
using System.Collections.Generic;

namespace Scraps.Model
{
    public partial class Person
    {
        public Person()
        {
            PersonInPicture = new HashSet<PersonInPicture>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PersonInPicture> PersonInPicture { get; set; }
    }
}
