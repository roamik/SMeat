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

        public User MadeBy { get; set; }

        public DateTimeOffset MadeTime { get; set; } = DateTimeOffset.Now;

        public int RepliesCount { get; set; }

        public List<Reply> Replies { get; set; }

        public List<BoardLike> Likes { get; set; } = new List<BoardLike>();

        public List<BoardDislike> Dislikes { get; set; } = new List<BoardDislike>();
    }
}
