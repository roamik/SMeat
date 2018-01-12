using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMeat.MODELS.Entities
{
    public class Board : Entity
    {
        public Board()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public override string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public List<Reply> Replies { get; set; }
    }
}
