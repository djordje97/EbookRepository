using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.Presentation.Controllers
{
    [Route("api/language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageManager _languageManager;

        public LanguageController(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }


        [HttpGet, Authorize]
        public IActionResult GetLanguages()
        {
            var languages = _languageManager.GetAll();
            return Ok(languages);
        }

        [HttpGet("{id}")]
        public IActionResult GetLanguage(int id)
        {
            var language = _languageManager.GetById(id);
            if (language == null)
                return NotFound();
            return Ok(language);
        }

        [HttpPost]
        public IActionResult AddLnguage([FromBody]LanguageDTO languageDto)
        {
            if (languageDto == null)
                return BadRequest();
            var language = _languageManager.Create(languageDto);

            return Created(new Uri("http://localhost:12621/api/language/" + language.LanguageId),language);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, [FromBody] LanguageDTO languageDto)
        {
            if (languageDto == null)
                return BadRequest();
            var languageFromDb = _languageManager.GetById(id);
            if (languageFromDb == null)
                return BadRequest();
            var language = _languageManager.Update(languageDto);
            return Ok(language);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            var languageFromDb = _languageManager.GetById(id);
            if (languageFromDb == null)
                return BadRequest();
            _languageManager.Delete(id);
            return Ok();

        }

    }
}