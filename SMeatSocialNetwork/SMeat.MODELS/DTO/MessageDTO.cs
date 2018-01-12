using System;
using SMeat.MODELS.Enums;

namespace SMeat.MODELS.DTO {
    public class MessageDTO {

        public string Id { get; set; }

        public DateTimeOffset? DateTime { get; set; }

        public string Text { get; set; }

        public MessageStatus MessageStatus { get; set; }
        
        public string UserId { get; set; }

        public virtual UserDTO User { get; set; }
        
        public string ChatId { get; set; }

        public virtual ChatDTO Chat { get; set; }
        
        public string GroupChatId { get; set; }

        public virtual GroupChatDTO GroupChat { get; set; }
    }
}