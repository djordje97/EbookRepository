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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
        IMapper _mapper;

        public CategoryController(CategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetAll();
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in categories)
            {
                if (category.CategoryId == 1)
                    continue;
                categoryDtos.Add(_mapper.Map<CategoryDto>(category));

            }
            return Ok(categoryDtos);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoryDtos.Add(_mapper.Map<CategoryDto>(category));

            }
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetOne(id);
            if (category == null)
                return NotFound();
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory([FromBody]CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest();
            var category = _mapper.Map<Category>(categoryDto);
            category = _categoryRepository.Save(category);
            _categoryRepository.Complete();

            return Created(new Uri("http://localhost:12621/api/category/" + category.CategoryId), _mapper.Map<CategoryDto>(category));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest();
            var categoryFromDb = _categoryRepository.GetOne(id);
            if (categoryFromDb == null)
                return BadRequest();
            categoryFromDb.Name = categoryDto.Name;
            categoryFromDb = _categoryRepository.Update(categoryFromDb);
            _categoryRepository.Complete();
            return Ok(_mapper.Map<CategoryDto>(categoryFromDb));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryFromDb = _categoryRepository.GetOne(id);
            if (categoryFromDb == null)
                return BadRequest();
            _categoryRepository.Delete(categoryFromDb);
            _categoryRepository.Complete();
            return Ok();

        }
    }

}