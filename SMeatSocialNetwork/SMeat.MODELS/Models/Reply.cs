using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMeat.MODELS.Models
{
    public class Reply
    {

        public Reply()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [ForeignKey("Board")]
        public string BoardId { get; set; }

        public Board Board { get; set; }

        //public int[] ReplyTo { get; set; } // an array of IDs of the replies this reply is sent to (WHAT?)
    }
}
