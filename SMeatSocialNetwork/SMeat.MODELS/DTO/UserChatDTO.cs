using SMeat.MODELS.Enums;

namespace SMeat.MODELS.DTO {
    public class UserChatDTO {
        public virtual string UserId { get; set; }

        public virtual UserDTO User { get; set; }

        public virtual string ChatId { get; set; }

        public virtual ChatDTO Chat { get; set; }

        public virtual UserChatPermitions Permitions { get; set; }

        public UserStatusType Status { get; set; }
    }
}