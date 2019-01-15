using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Lucene.Model
{
    public class ResultData
    {
        public string Filename { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public string Highlight { get; set; }
    }
}
