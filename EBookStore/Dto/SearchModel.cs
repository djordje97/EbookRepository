using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Dto
{
    public class SearchModel
    {
        public string Input { get; set; }
        public string FirstField { get; set; }
        public string SecondField { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }

    }
}
