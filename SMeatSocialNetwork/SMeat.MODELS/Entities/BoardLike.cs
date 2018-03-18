using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.Entities
{
    public class BoardLike
    {
        [ForeignKey("Board")]
        [Required]
        public string BoardId { get; set; }

        public virtual Board Board { get; set; }

        [ForeignKey("LikeFrom")]
        [Required]
        public string LikeFromId { get; set; }

        public virtual User LikeFrom { get; set; }
    }
}
