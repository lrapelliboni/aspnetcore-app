using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using citelapi.Models;
using citelapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace citelapi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;  

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _logger = logger;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await categoryRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            var category = await categoryRepository.GetById(id);            
            return category;
        }

        [HttpGet("{id}/products")]
        public async Task<Category> GetWithProducts(int id)
        {
            var category = await categoryRepository.GetByIdWithProducts(id);
            return category;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
 
            try
            {
                await categoryRepository.Update(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
 
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            await categoryRepository.Add(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
 
            await categoryRepository.Remove(category);

            return Ok();
        }        
    }
}
