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

namespace EBookStore.Controllers
{
    [Route("api/ebook")]
    [ApiController]
    public class EbookController : ControllerBase
    {

        private readonly EbookRepository _ebookRepository;

        IMapper _mapper;
        public EbookController(EbookRepository ebookRepository, IMapper mapper)
        {
            _ebookRepository = ebookRepository;
            _mapper = mapper;


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
        public IActionResult AddEbook([FromBody]EbookDto ebookDto)
        {
            if (ebookDto == null)
                return BadRequest();
            var ebook = _mapper.Map<Ebook>(ebookDto);
            ebook = _ebookRepository.Save(ebook);
            _ebookRepository.Complete();

            return Created(new Uri("http://localhost:12621/api/ebook/" + ebook.UserId), _mapper.Map<EbookDto>(ebook));
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public IActionResult GetBooksData()
        {
            try
            {
                var file = Request.Form.Files[0];
                var filePath = Path.Combine(ConfigurationManager.TempDir, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                FileInfo fileInfo = new FileInfo(file.FileName);
                var indexUnit = Indexer.GetIndexUnit(fileInfo);
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

        [HttpDelete("{username}")]
        public IActionResult DeleteEbook(int id)
        {
            var ebookFromDb = _ebookRepository.GetOne(id);
            if (ebookFromDb == null)
                return BadRequest();
            _ebookRepository.Delete(ebookFromDb);
            _ebookRepository.Complete();
            return Ok();

        }
    }
}