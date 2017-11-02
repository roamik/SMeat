using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class Workplace
    {
        public Workplace()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Position { get; set; }

        [ForeignKey("Location")]
        public string LocationId { get; set; }

        public virtual Location Location { get; set; }

        public string CompanyName { get; set; }

        //Navigation properties
        public virtual List<User> Users { get; set; }
    }
}
