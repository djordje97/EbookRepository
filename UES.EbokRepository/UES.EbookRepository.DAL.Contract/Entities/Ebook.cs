using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.DAL.Contract.Entities
{
   public class Ebook
    {
        public int EbookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public int PublicationYear { get; set; }
        public string Filename { get; set; }
        public string MIME { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}
