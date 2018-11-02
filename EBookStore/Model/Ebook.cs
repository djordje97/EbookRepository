using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Model
{
    public class Ebook
    {
        public int EbookId { get; set; }
        [Required]
        [StringLength(80)]
        public string Title { get; set; }
        [StringLength(120)]
        public string Author { get; set; }
        [StringLength(120)]
        public string Keywords { get; set; }
        public int PublicationYear { get; set; }
        [Required]
        [StringLength(200)]
        public string Filename { get; set; }
        [StringLength(100)]
        public string MIME { get; set; }

        public  virtual User Use { get; set; }
        public int UserId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}
