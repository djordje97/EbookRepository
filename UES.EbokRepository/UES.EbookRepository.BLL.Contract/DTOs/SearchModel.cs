using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.BLL.Contract.DTOs
{
   public  class SearchModel
    {
        public string Input { get; set; }
        public string FirstField { get; set; }
        public string SecondField { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public string SecondInput { get; set; }
    }
}
