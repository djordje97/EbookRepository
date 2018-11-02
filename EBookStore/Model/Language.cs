using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Model
{
    public class Language
    {
        public int LanguageId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
