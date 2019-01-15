using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EBookStore.Dto;
using EBookStore.Model;
using EBookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EBookStore.Lucene;
using System.IO;
using EBookStore.Configuration;
using EBookStore.Lucene.Model;
using Microsoft.AspNetCore.Authorization;

namespace EBookStore.Controllers
{
    [Route("api/ebook")]
    [ApiController]
    public class EbookController : ControllerBase
    {

        private readonly EbookRepository _ebookRepository;

        IMapper _mapper;
        private readonly CategoryRepository _categoryRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly UserRepository _userRepository;
        public EbookController(EbookRepository ebookRepository, IMapper mapper,CategoryRepository categoryRepository,LanguageRepository languageRepository,UserRepository userRepository)
        {
            _ebookRepository = ebookRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _languageRepository = languageRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetEbooks()
        {
            var ebooks = _ebookRepository.GetAll();
            var ebookDtos = new List<EbookDto>();
            foreach (var ebook in ebooks)
            {
                ebookDtos.Add(_mapper.Map<EbookDto>(ebook));

            }
            return Ok(ebookDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetEbook(int id)
        {
            var ebook = _ebookRepository.GetOne(id);
            if (ebook == null)
                return NotFound();
            return Ok(_mapper.Map<EbookDto>(ebook));
        }

        [HttpPost]
        public IActionResult AddEbook(IndexUnit indexUnit)
        {
            if (indexUnit == null)
                return BadRequest();
            var categoryId = int.Parse(indexUnit.Category);
            var languageId = int.Parse(indexUnit.Language);
            var language = _languageRepository.GetOne(languageId);
            var category=_categoryRepository.GetOne(categoryId);
            var keywords = string.Empty;
            var username = User.Identity.Name;
            var user = _userRepository.GetByUsername(username);
            foreach (var item in indexUnit.Keywords)
            {
                keywords += item + " ";

            }

            Ebook ebook = new Ebook()
            {
                Title = indexUnit.Title,
                MIME = "application/json",
                Author = indexUnit.Author,
                Filename = indexUnit.Filename,
                Category = category,
                Language = language,
                PublicationYear=int.Parse(indexUnit.FileDate),
                Keywords=keywords,
                User=user
            };
            ebook = _ebookRepository.Save(ebook);
            _ebookRepository.Complete();
            Indexer.index(indexUnit);
            return Created(new Uri("http://localhost:12621/api/ebook/" + ebook.EbookId), _mapper.Map<EbookDto>(ebook));
        }

        [HttpGet("download/{filename}")]
        [Authorize]
        public IActionResult DownloadBook(string filename)
        {
            var book = _ebookRepository.GetEbookByFilename(filename);
            string path = Path.Combine(ConfigurationManager.FileDir, book.Filename);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);


            return File(fileBytes, "application/pdf", book.Filename);
        }


        [HttpPost("search")]
        [AllowAnonymous]
        public IActionResult Search(SearchModel searchModel)
        {
            var books = _ebookRepository.Search(searchModel);

            return Ok(books);


        }
        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var books = _ebookRepository.GetAll();
            var booksWithCategory = new List<EbookDto>();
            foreach (var book in books)
            {
                if(book.CategoryId == categoryId)
                {
                    booksWithCategory.Add(_mapper.Map<EbookDto>(book));
                }
            }


            return Ok(booksWithCategory);
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

                var indexUnit = Indexer.GetIndexUnit(file.FileName);
                indexUnit.Filename = file.Name;
                
                return Ok(indexUnit);

            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.Message);
                return BadRequest();
            }
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdateEbook(int id, [FromBody] EbookDto ebookDto)
        {
            if (ebookDto == null)
                return BadRequest();
            var ebookFromDb = _ebookRepository.GetOne(id);
            if (ebookFromDb == null)
                return BadRequest();
            ebookFromDb.Title = ebookDto.Title;
            ebookFromDb.Author = ebookFromDb.Author;
            ebookFromDb.Keywords = ebookDto.Keywords;
            ebookFromDb.MIME = ebookDto.MIME;
            ebookFromDb = _ebookRepository.Update(ebookFromDb);
            _ebookRepository.Complete();
            return Ok(_mapper.Map<EbookDto>(ebookFromDb));
        }



        [HttpDelete("{filename}")]
        public IActionResult DeleteEbook(string filename)
        {
            var ebookFromDb = _ebookRepository.GetEbookByFilename(filename);
            if (ebookFromDb == null)
                return BadRequest();
            _ebookRepository.DeleteIndexDocument(ebookFromDb.Filename);
            _ebookRepository.Delete(ebookFromDb);
            _ebookRepository.Complete();
            return Ok();

        }
    }
}