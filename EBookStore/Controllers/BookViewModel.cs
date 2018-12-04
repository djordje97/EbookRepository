using EBookStore.Lucene.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Controllers
{
    public class BookViewModel
    {
        public IFormFile file { get; set; }
        public IndexUnit IndexUnit { get; set; }
    }
}
