using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS.BindingModels
{
    public class PageReturnModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
