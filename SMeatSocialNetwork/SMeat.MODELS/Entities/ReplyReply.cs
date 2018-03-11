using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Entities
{
    public class ReplyReply
    {
        [ForeignKey("Reply")]
        [Required]
        public string ReplyId { get; set; }

        public virtual Reply Reply { get; set; }

        [ForeignKey("ReplyTo")]
        [Required]
        public string ReplyToId { get; set; }

        public virtual Reply ReplyTo { get; set; }
    }
}
