using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.BLL.Contract.DTOs
{
  public  class ResultData
    {
        public string Filename { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public string Highlight { get; set; }
    }
}
