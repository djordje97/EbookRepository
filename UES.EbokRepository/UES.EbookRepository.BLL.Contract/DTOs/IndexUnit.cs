using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.BLL.Contract.DTOs
{
   public    class IndexUnit
    {

        public string Text { get; set; }
        public string Title { get; set; }
        public List<string> Keywords { get; set; }
        public string Filename { get; set; }
        public string FileDate { get; set; }

        public string Author { get; set; }
        public int Category { get; set; }
        public string Language { get; set; }
    }
}
