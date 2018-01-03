using Microsoft.AspNetCore.Identity;
using SMeat.MODELS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMeat.MODELS.Models
{
    public class User : IdentityUser
    {
        public User()
        {
           Id =  Guid.NewGuid().ToString();
        }       

        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        public string Status { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public GenderType GenderType { get; set; }

        public string CustomGenderType { get; set; }

        public DateTimeOffset? Birthdate { get; set; }

        public string About { get; set; }

        [ForeignKey("Location")]
        public string LocationId { get; set; }

        public virtual Location Location { get; set; }

        [ForeignKey("Workplace")]
        public string WorkplaceId { get; set; }

        public virtual Workplace Workplace { get; set; }
        
        //Navigation properties
        public virtual List<UserGroupChat> UserGroupChats { get; set; }

        public virtual List<Chat> Chats { get; set; } // chats where user is an owner

        public virtual List<UserChat> UserChats { get; set; } // chats where user is a user

        public virtual List<Message> Messages { get; set; }

        public virtual List<Contacts> ContactsTo { get; set; }

        public virtual List<Contacts> ContactsFrom { get; set; }


    }
}
