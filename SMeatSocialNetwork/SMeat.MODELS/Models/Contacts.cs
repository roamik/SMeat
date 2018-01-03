using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMeat.MODELS.Models
{
    public class Contacts
    {
        [ForeignKey("FirstUser")]
        [Required]
        public string FirstUserId { get; set; }

        public virtual User FirstUser { get; set; }

        [ForeignKey("SecondUser")]
        public string SecondUserId { get; set; }

        public virtual User SecondUser { get; set; }
    }
}
