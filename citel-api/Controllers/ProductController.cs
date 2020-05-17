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
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;  

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }
     
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await productRepository.GetAll();
        }

        [HttpGet("category")]
        public async Task<IEnumerable<Product>> GetAllWithCategory()
        {
            return await productRepository.GetAllWithCategory();
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            var product = await productRepository.GetById(id);            
            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
 
            try
            {
                await productRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            await productRepository.Add(product);
            return CreatedAtAction("Get", new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
 
            await productRepository.Remove(product);
 
            return Ok();
        }        
    }
}
