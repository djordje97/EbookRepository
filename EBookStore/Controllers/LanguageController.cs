using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EBookStore.Dto;
using EBookStore.Model;
using EBookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBookStore.Controllers
{
    [Route("api/language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguageController(LanguageRepository languageRepository,IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        [HttpGet, Authorize]
        public IActionResult GetLanguages()
        {
            var languages = _languageRepository.GetAll();
            var languagesDto = new List<LanguageDto>();
            foreach (var language in languages)
            {
                languagesDto.Add(_mapper.Map<LanguageDto>(language));

            }
            return Ok(languagesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetLanguage(int id)
        {
            var language = _languageRepository.GetOne(id);
            if (language == null)
                return NotFound();
            return Ok(_mapper.Map<LanguageDto>(language));
        }

        [HttpPost]
        public IActionResult AddLnguage([FromBody]LanguageDto languageDto)
        {
            if (languageDto == null)
                return BadRequest();
            var language = _mapper.Map<Language>(languageDto);
            language = _languageRepository.Save(language);
            _languageRepository.Complete();

            return Created(new Uri("http://localhost:12621/api/language/"+language.LanguageId),_mapper.Map<LanguageDto>(language));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id ,[FromBody] LanguageDto languageDto)
        {
            if (languageDto == null )
                return BadRequest();
            var languageFromDb = _languageRepository.GetOne(id);
            if(languageFromDb == null)
                return BadRequest();
            languageFromDb.Name = languageDto.Name;
            languageFromDb = _languageRepository.Update(languageFromDb);
            _languageRepository.Complete();
            return Ok(_mapper.Map<LanguageDto>(languageFromDb));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            var languageFromDb = _languageRepository.GetOne(id);
            if (languageFromDb == null)
                return BadRequest();
            _languageRepository.Delete(languageFromDb);
            _languageRepository.Complete();
            return Ok();
         
        }
    }
}