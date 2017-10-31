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
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public string Status { get; set; }
        // public enum Relationship { get; set; }
        public DateTimeOffset? Birthdate { get; set; }
        // public enum Gender { get; set; }
        public string About { get; set; }

        // [ForeignKey("Location")]
        public int LocationId { get; set; }
        // [ForeignKey("Workplace")]
        public int WorkplaceId { get; set; }
    }
}
