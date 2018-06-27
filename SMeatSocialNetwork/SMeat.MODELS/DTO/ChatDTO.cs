using System;
using System.Collections.Generic;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.DTO {
    public class ChatDTO  {
        public string Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual DateTimeOffset DateTime { get; set; }
        public virtual List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
        public virtual List<UserChatDTO> UserChats { get; set; } = new List<UserChatDTO>();
    }
}