using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMeat.MODELS.Entities
{
    public class Reply : Entity 
    {

        public Reply()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public override string Id { get; set; }

        [ForeignKey("Board")]
        public string BoardId { get; set; }

        public Board Board { get; set; }

        public string Text { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public virtual ICollection<ReplyReply> ReplyTo { get; set; } = new List<ReplyReply>();
    }
}
