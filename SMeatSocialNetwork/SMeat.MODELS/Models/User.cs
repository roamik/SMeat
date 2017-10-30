using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your surname")]
        public string LName { get; set; }
        public string Picture { get; set; } // is this a link to the picture? or a path to the folder? or both depending on a signature?
        public string Status { get; set; }
        // public enum Relationship { get; set; }
        public DateTime Birthdate { get; set; }
        // public enum Gender { get; set; }
        public string About { get; set; }

        // [ForeignKey("Location")]
        public int LocationId { get; set; }
        // [ForeignKey("Workplace")]
        public int WorkplaceId { get; set; }
    }
}
