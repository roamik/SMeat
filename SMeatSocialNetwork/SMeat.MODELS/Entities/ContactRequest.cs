using System;
using System.ComponentModel.DataAnnotations.Schema;
using SMeat.MODELS.DTO;

namespace SMeat.MODELS.Entities
{
    public class ContactRequest
    {
        public DateTimeOffset? RequestDate { get; set; }

        [ForeignKey("FirstUserRequest")]
        public string FirstUserRequestId { get; set; }

        public virtual User FirstUserRequest { get; set; }

        [ForeignKey("SecondUserRequest")]
        public string SecondUserRequestId { get; set; }

        public virtual User SecondUserRequest { get; set; }
    }
}
