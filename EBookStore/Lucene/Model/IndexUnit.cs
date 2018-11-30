using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Lucene.Model
{
    public class IndexUnit
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public List<string> Keywords { get; set; }
        public string Filename { get; set; }
        public string FileDate { get; set; }

        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }

    }
}
