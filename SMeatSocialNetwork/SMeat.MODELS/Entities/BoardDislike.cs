using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Entities
{
    public class BoardDislike
    {
        [ForeignKey("Board")]
        [Required]
        public string BoardId { get; set; }

        public virtual Board Board { get; set; }

        [ForeignKey("DislikeFrom")]
        [Required]
        public string DislikeFromId { get; set; }

        public virtual User DislikeFrom { get; set; }
    }
}
