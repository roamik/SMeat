using SMeat.MODELS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class UserChat
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Chat")]
        public string ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        public UserChatPermitions Permitions { get; set; } 
    }
}
