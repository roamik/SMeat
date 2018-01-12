using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMeat.MODELS.DTO;

namespace SMeat.MODELS.Entities
{
    public class Location : Entity 
    {
        public Location()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public override string Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

        //Navigation properties

        public virtual List<User> Users { get; set; }

        public virtual List<Workplace> Workplaces { get; set; }
    }
}
