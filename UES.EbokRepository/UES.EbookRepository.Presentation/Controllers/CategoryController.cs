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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCategories()
        {
            var categories = _categoryManager.GetAll().Where(category =>category.CategoryId != 1);
            return Ok(categories);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryManager.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryManager.GetById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory([FromBody]CategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest();
            var category = _categoryManager.Create(categoryDto);

            return Created(new Uri("http://localhost:12621/api/category/" + category.CategoryId), category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest();
            var categoryFromDb = _categoryManager.GetById(id);
            if (categoryFromDb == null)
                return BadRequest();
            var category = _categoryManager.Update(categoryDto);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryFromDb = _categoryManager.GetById(id);
            if (categoryFromDb == null)
                return BadRequest();
            _categoryManager.Delete(id);
            return Ok();

        }
    }
}