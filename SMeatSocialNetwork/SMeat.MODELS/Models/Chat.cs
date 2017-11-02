using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class Chat
    {
        public Chat()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        //Navigation properties
        public virtual List<Message> Messages { get; set; }

        public virtual List<UserChat> UserChats { get; set; } // users in this chat

    }
}
