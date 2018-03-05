using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Entities
{
    class BoardReply
    {
        [ForeignKey("Board")]
        public string BoardId { get; set; }

        public virtual Board Board { get; set; }

        [ForeignKey("Reply")]
        public string ReplyId { get; set; }

        public virtual Reply Reply { get; set; }
    }
}
