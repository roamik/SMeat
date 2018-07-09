using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS.BindingModels
{
    public class ChatCreateBindingModel
    {
        public string Text { get; set; }

        public string Picture { get; set; }

        public string[] UserIds { get; set; }
    }
}
