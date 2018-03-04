using SMeat.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS.DTO
{
    class BoardDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public List<Reply> Replies { get; set; }
    }
}
