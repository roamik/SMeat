using SMeat.MODELS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class UserGroupChat
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }
        
        [ForeignKey("GroupChat")]
        public string GroupChatId { get; set; }

        public virtual GroupChat GroupChat { get; set; }

        public UserChatRole UserRole { get; set; }
    }
}
