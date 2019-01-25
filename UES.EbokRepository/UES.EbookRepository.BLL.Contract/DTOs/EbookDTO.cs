using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.BLL.Contract.DTOs
{
    public class EbookDTO
    {
        public int EbookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public int PublicationYear { get; set; }
        public string Filename { get; set; }
        public string MIME { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
    }
}
