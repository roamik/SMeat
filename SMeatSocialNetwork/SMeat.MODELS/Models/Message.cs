using SMeat.MODELS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class Message
    {
        public Message()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public DateTimeOffset? DateTime { get; set; }

        public string Text { get; set; }

        public MessageStatus MessageStatus { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Chat")]
        public string ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        [ForeignKey("GroupChat")]
        public string GroupChatId { get; set; }

        public virtual GroupChat GroupChat { get; set; }
    }
}
