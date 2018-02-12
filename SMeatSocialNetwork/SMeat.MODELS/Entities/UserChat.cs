using System.ComponentModel.DataAnnotations.Schema;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Enums;

namespace SMeat.MODELS.Entities
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

        public UserStatusType Status { get; set; }
    }
}
