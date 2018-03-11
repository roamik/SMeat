using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS.BindingModels
{
    public class ReplyCreateBindingModel
    {
        public string Text { get; set; }
        public string BoardId { get; set; }

        public string[] ReplyId { get; set; }
    }
}
