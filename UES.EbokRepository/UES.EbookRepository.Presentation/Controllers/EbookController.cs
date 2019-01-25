using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.BLL.Extensions;

namespace UES.EbookRepository.Presentation.Controllers
{
    [Route("api/ebook")]
    [ApiController]
    public class EbookController : ControllerBase
    {
        private readonly IEbookManager _ebookManager;
        private readonly ILanguageManager _languageManager;
        private readonly IIndexManager _indexManager;

        public EbookController(IEbookManager ebookManager, ILanguageManager languageManager, IIndexManager indexManager)
        {
            _ebookManager = ebookManager;
            _languageManager = languageManager;
            _indexManager = indexManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetEbooks()
        {
            var ebooks = _ebookManager.GetAll();
            return Ok(ebooks);
        }

        [HttpGet("{id}")]
        public IActionResult GetEbook(int id)
        {
            var ebook = _ebookManager.GetById(id);
            if (ebook == null)
                return NotFound();
            IndexUnit indexUnit = new IndexUnit()
            {
                Title = ebook.Title,
                Category = ebook.CategoryId,
                Author = ebook.Author,
                FileDate = ebook.PublicationYear.ToString(),
                Filename = ebook.Filename,
                Language = _languageManager.GetById(ebook.LanguageId).Name,
                Keywords = ebook.Keywords.Trim().Split(" ").ToList()
            };
            return Ok(indexUnit);
        }

        [HttpPost]
        public IActionResult AddEbook(IndexUnit indexUnit)
        {
            if (indexUnit == null)
                return BadRequest();

            var ebook = _ebookManager.Create(indexUnit);
            return Created(new Uri("http://localhost:12621/api/ebook/" + ebook.EbookId), ebook);
        }

        [HttpGet("download/{filename}")]
        [Authorize]
        public IActionResult DownloadBook(string filename)
        {
            var book = _ebookManager.GetByFilename(filename);
            string path = Path.Combine(ConfigurationManager.FileDir, book.Filename);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);


            return File(fileBytes, "application/pdf", book.Filename);
        }


        [HttpPost("search")]
        [AllowAnonymous]
        public IActionResult Search(SearchModel searchModel)
        {
            var books = _ebookManager.Search(searchModel);

            return Ok(books);


        }
        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var books = _ebookManager.GetByCategory(categoryId);


            return Ok(books);
        }
        [HttpPost("upload"), DisableRequestSizeLimit]
        public IActionResult GetBooksData()
        {
            try
            {
                var file = Request.Form.Files[0];
                var filePath = Path.Combine(ConfigurationManager.FileDir, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Close();
                }

                var indexUnit = _indexManager.GetIndexUnit(file.FileName);
                indexUnit.Filename = file.Name;

                return Ok(indexUnit);

            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.Message);
                return BadRequest();
            }
        }


        [HttpPut("{filename}")]
        public IActionResult UpdateEbook(string filename, [FromBody] IndexUnit indexUnit)
        {
            if (indexUnit == null)
                return BadRequest();

            var ebookFromDb = _ebookManager.Update(indexUnit);
            return Ok(ebookFromDb);
        }



        [HttpDelete("{filename}")]
        public IActionResult DeleteEbook(string filename)
        {
            var ebookFromDb = _ebookManager.GetByFilename(filename);
            if (ebookFromDb == null)
                return BadRequest();
            _ebookManager.Delete(ebookFromDb.EbookId);
            return Ok();

        }
    }
}