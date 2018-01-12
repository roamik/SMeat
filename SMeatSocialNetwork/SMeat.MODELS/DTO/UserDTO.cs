using System;
using System.Collections.Generic;
using SMeat.MODELS.Enums;

namespace SMeat.MODELS.DTO {
    public class UserDTO {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        public string Status { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public GenderType GenderType { get; set; }

        public string CustomGenderType { get; set; }

        public DateTimeOffset? Birthdate { get; set; }

        public string About { get; set; }
        
        public string LocationId { get; set; }

        public virtual LocationDTO Location { get; set; }
        
        public string WorkplaceId { get; set; }

        public virtual WorkplaceDTO Workplace { get; set; }
        
        public virtual List<UserGroupChatDTO> UserGroupChats { get; set; }

        public virtual List<ChatDTO> Chats { get; set; }

        public virtual List<UserChatDTO> UserChats { get; set; }

        public virtual List<MessageDTO> Messages { get; set; }

        public virtual List<ContactsDTO> ContactsTo { get; set; }

        public virtual List<ContactsDTO> ContactsFrom { get; set; }
    }
}