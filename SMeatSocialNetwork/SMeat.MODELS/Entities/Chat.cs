using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMeat.MODELS.DTO;

namespace SMeat.MODELS.Entities
{
    public class Chat : Entity {
        public Chat()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public override string Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        //Navigation properties
        public virtual List<Message> Messages { get; set; } = new List<Message>();

        public virtual List<UserChat> UserChats { get; set; } = new List<UserChat>();// users in this chat

    }
    
}
