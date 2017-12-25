using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMeat.MODELS.Models
{
    public class Board
    {

        public Board()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public List<Reply> Replies { get; set; }
    }
}
