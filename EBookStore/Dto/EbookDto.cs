using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Dto
{
    public class EbookDto
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
