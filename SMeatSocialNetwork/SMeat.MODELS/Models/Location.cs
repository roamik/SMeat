using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class Location
    {
        public Location()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

        //Navigation properties

        public virtual List<User> Users { get; set; }

        public virtual List<Workplace> Workplaces { get; set; }
    }
}
