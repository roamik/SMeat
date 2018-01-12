using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMeat.MODELS.Entities
{
    public class GroupChat : Entity 
    {
        public GroupChat()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public override string Id { get; set; }

        public string Name { get; set; }

        //Navigation properties

        public virtual List<Message> Messages { get; set; }

        public virtual List<UserGroupChat> UserGroupChats { get; set; }

    }
}
